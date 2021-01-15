Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.IO.Ports
Imports System.Threading
Imports iMeeting.My.Resources

Namespace iMeeting
    Public Class ReservationController
        Inherits BaseController

        Private db As New ApplicationDbContext

        <HttpPost()> _
        Public Function Autocomplete(term As String) As ActionResult
            Dim results = (From e In db.RESERVATION
                Where (e.PresidentSeance.ToLower.Contains(term.ToLower))
                Select e.PresidentSeance).Distinct

            'Dim count = results.Count

            Return Json(results, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()> _
        Public Function RequiredActionCount() As ActionResult
            Dim nbre = 0

            ' Si ce n'est pas le SCC, limiter à l'utilisateur en cours
            If Not User.IsInRole("SCC") Then
                Dim currentUserId As String = User.Identity.GetUserId
                nbre = (From e In db.RESERVATION
                        Where e.UserId = currentUserId And (e.ETAT = EnumReservation.Initial Or e.CurrentState = EnumCurrentState.CanceledPending Or e.CurrentState = EnumCurrentState.ReportedPending)
                        Select e.ID_RESERVATION).Count
            Else
                nbre = (From e In db.RESERVATION
                Where e.ETAT = EnumReservation.Initial Or e.CurrentState = EnumCurrentState.CanceledPending Or e.CurrentState = EnumCurrentState.ReportedPending
                Select e.ID_RESERVATION).Count
            End If

            'Dim count = results.Count

            Return Json(nbre, JsonRequestBehavior.AllowGet)
        End Function

        Private Function getCollisions(id_lieu As Long, heure_deb As DateTime, heure_fin As DateTime, Optional id_reserv As Long = -1) As List(Of RESERVATION)
            Dim collisions = From e In db.RESERVATION
                             Where (e.ID_RESERVATION <> id_reserv Or id_reserv = -1) And
                             e.ID_LIEU = id_lieu And
                             ((e.HEURE_DEBUT <= heure_deb And heure_deb < e.HEURE_FIN) Or
                              (e.HEURE_DEBUT < heure_fin And heure_fin <= e.HEURE_FIN) Or
                              (heure_deb <= e.HEURE_DEBUT And e.HEURE_DEBUT < heure_fin) Or
                              (heure_deb < e.HEURE_FIN And e.HEURE_FIN <= heure_fin))
                            Select e

            Return collisions.ToList
        End Function

        <AllowAnonymous>
        Function Calendar(id_lieu As Long?, date_deb As Date?, id_reserv As Long?) As ActionResult
            ViewBag.activeMenu = "oper-1"
            'Return View()
            Dim model As New ReservationViewModel
            If id_reserv.HasValue Then
                Dim reserv = db.RESERVATION.Find(id_reserv)
                If IsNothing(reserv) Then
                    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                End If
                model.LoadEntity(reserv)
            Else
                If id_lieu.HasValue Then
                    model.ID_LIEU = id_lieu
                End If
                If date_deb.HasValue Then
                    model.DATE_DEBUT = date_deb
                End If
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", model.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", model.ThemeId)
            Return View(model)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Public Function Calendar(reservationVM As ReservationViewModel) As ActionResult
            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservationVM.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservationVM.ThemeId)
            ' la réservation
            Dim reserv As RESERVATION = reservationVM.getReservation(db)
            If ModelState.IsValid Then
                Dim lst = getCollisions(reserv.ID_LIEU, reserv.HEURE_DEBUT, reserv.HEURE_FIN, reserv.ID_RESERVATION)
                If lst.Count = 0 Then
                    If reserv.ID_RESERVATION <= 0 Then
                        reserv.UserId = User.Identity.GetUserId()

                        ' Pour le SCC, c'est la validation directe
                        If User.IsInRole("SCC") Then
                            reserv.ETAT = EnumReservation.Confirmed
                        End If

                        db.RESERVATION.Add(reserv)
                    Else
                        reserv.ETAT = EnumReservation.Confirmed ' Etat initial

                        db.Entry(reserv).State = EntityState.Modified
                    End If

                    Try
                        db.SaveChanges()

                        ' Envoyer Mail et SMS
                        If reserv.ETAT = EnumReservation.Confirmed Then
                            SendMailAndSms(reserv, db)
                        End If

                        Return RedirectToAction("Calendar", New With {.id_lieu = reserv.ID_LIEU, .date_deb = reserv.DATE_DEBUT})
                    Catch ex As Exception
                        Util.getError(ex, ModelState)
                    End Try
                Else
                    For Each r In lst
                        ModelState.AddModelError("", String.Format("{0} : {1} {2} {3} - {4}", Resource.rese_collision, r.Objet, r.HEURE_DEBUT.ToShortDateString, r.HEURE_DEBUT.ToShortTimeString, r.HEURE_FIN.ToShortTimeString))
                    Next
                End If
            End If
            Return View("Create", reservationVM)
        End Function

        <HttpPost>
        Public Function Annulation(id As Long) As JsonResult
            Dim msg = "" 'String.Format("Annulation Id = {0}", id) '
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                msg = "Reservation not found"
            End If
            If reservation.UserId <> User.Identity.GetUserId Then
                msg = My.Resources.Resource.rese_your_are_not_owner
            Else
                If reservation.ETAT = EnumReservation.Initial Then
                    reservation.CurrentState = EnumCurrentState.Canceled
                ElseIf reservation.ETAT = EnumReservation.Confirmed Then
                    reservation.CurrentState = EnumCurrentState.CanceledPending
                End If

                db.Entry(reservation).State = EntityState.Modified
                Try
                    db.SaveChanges()
                    msg = "ok"
                Catch ex As Exception
                    msg = Util.getError(ex)
                End Try
            End If

            Return Json(msg)
        End Function

        Public Function UpdateEvent(id As Long, NewEventStart As DateTime, NewEventEnd As DateTime) As JsonResult
            Dim msg = "" 'String.Format("Id = {0}, début = {1} et fin = {2}", id, NewEventStart.ToString, NewEventEnd.ToString)

            Dim reservation As RESERVATION = db.RESERVATION.Find(id)

            If reservation Is Nothing Then
                Return Json("Event not found")
            End If

            Dim lst = getCollisions(reservation.ID_LIEU, NewEventStart, NewEventEnd, reservation.ID_RESERVATION)
            If lst.Count = 0 Then

                reservation.DATE_DEBUT = NewEventStart
                reservation.DATE_FIN = NewEventEnd
                reservation.HEURE_DEBUT = reservation.DATE_DEBUT
                reservation.HEURE_FIN = reservation.DATE_FIN

                db.Entry(reservation).State = EntityState.Modified
                Try
                    db.SaveChanges()
                    msg = "ok"
                Catch ex As Exception
                    msg = Util.getError(ex)
                End Try
            Else
                For Each r In lst
                    msg &= String.Format("{0} : {1} {2} {3} - {4}", Resource.rese_collision, r.Objet, r.HEURE_DEBUT.ToShortDateString, r.HEURE_DEBUT.ToShortTimeString, r.HEURE_FIN.ToShortTimeString) & vbCrLf
                Next
            End If
            Return Json(msg)
        End Function

        Public Function SaveEvent(Title As String, NewEventDate As String, NewEventTime As DateTime, NewEventDuration As String) As Boolean
            Return True ' DiaryEvent.CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration)
        End Function

        <AllowAnonymous>
        Public Function GetDiarySummary(start As DateTime, [end] As DateTime, id_lieu As Integer) As JsonResult
            'Dim ApptListForDate = ReservationEvent.LoadReservationsSummaryInDateRange(start, [end], id_lieu)
            'Dim eventList = From e In ApptListForDate
            '                Select New With {
            '                     .id = e.ID_Reservation,
            '                     .title = e.Title,
            '                     .start = e.StartDateString,
            '                     .[end] = e.EndDateString,
            '                     .someKey = e.ID_Lieu,
            '                     .allDay = False
            '                }
            'Dim rows = eventList.ToArray()
            'Return Json(rows, JsonRequestBehavior.AllowGet)
            Return GetDiaryEvents(start, [end], id_lieu, "")
        End Function

        <AllowAnonymous>
        Public Function GetDiaryEvents(start As DateTime, [end] As DateTime, id_lieu As Integer, vue As String) As JsonResult
            'If vue = "month" Then Return GetDiarySummary(start, [end], id_lieu)
            Dim ReservationListForDate = ReservationEvent.LoadAllReservationsInDateRange(start, [end], id_lieu)
            Dim eventList = From e In ReservationListForDate
                            Select New With {
                                 .id = e.ID_Reservation,
                                 .title = e.Objet,
                                 .start = e.StartDateString,
                                 .[end] = e.EndDateString,
                                 .color = e.StatusColor,
                                 .className = e.ClassName,
                                 .someKey = e.ID_Lieu,
                                 .url = Url.Action("Calendar", New With {.id_reserv = e.ID_Reservation}),
                                 .motif = e.Objet,
                                 .president = e.PresidentSeance,
                                 .State = e.State,
                                 .StateName = e.StateName,
                                 .currentState = e.CurrentState,
                                 .currentStateName = e.CurrentStateName,
                                 .currentStateDisplayName = e.currentStateDisplayName,
                                 .lieu = e.Lieu,
                                 .userId = e.UserId,
                                 .allDay = False
                            }
            Dim rows = eventList.ToArray()
            Return Json(rows, JsonRequestBehavior.AllowGet)
        End Function

        ' GET: /RESERVATION/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, tab As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder
            Dim tabNumber = If(tab, 1)
            ViewBag.activeTab = tabNumber

            ViewBag.activeMenu = "oper-2"

            ViewBag.IdLieuSortParm = If(String.IsNullOrEmpty(sortOrder), "IdLieu_desc", "")
            ViewBag.UseridSortParm = If(sortOrder = "Userid", "Userid_desc", "Userid")
            ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
            ViewBag.MotifSortParm = If(sortOrder = "Motif", "Motif_desc", "Motif")
            ViewBag.DateDebutSortParm = If(sortOrder = "DateDebut", "DateDebut_desc", "DateDebut")
            ViewBag.DateFinSortParm = If(sortOrder = "DateFin", "DateFin_desc", "DateFin")
            ViewBag.EtatSortParm = If(sortOrder = "Etat", "Etat_desc", "Etat")
            ViewBag.HeureDebutSortParm = If(sortOrder = "HeureDebut", "HeureDebut_desc", "HeureDebut")
            ViewBag.HeureFinSortParm = If(sortOrder = "HeureFin", "HeureFin_desc", "HeureFin")
            ViewBag.NbrePersSortParm = If(sortOrder = "NbrePers", "NbrePers_desc", "NbrePers")
            ViewBag.DetailsSortParm = If(sortOrder = "Details", "Details_desc", "Details")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.RESERVATION.Include(Function(l) l.LIEUX)

            If tabNumber = 1 Then
                entities = entities.Where(Function(e) e.ETAT = EnumReservation.Initial Or e.CurrentState = EnumCurrentState.CanceledPending Or e.CurrentState = EnumCurrentState.ReportedPending)
            End If

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.PresidentSeance.ToUpper.Contains(searchString.ToUpper) Or e.Objet.ToUpper.Contains(searchString.ToUpper) Or e.LIEUX.LIBELLE.ToUpper.Contains(searchString.ToUpper))
            End If

            ' Si ce n'est pas le SCC, limiter à l'utilisateur en cours
            If Not User.IsInRole("SCC") Then
                Dim currentUserId As String = User.Identity.GetUserId
                entities = entities.Where(Function(e) e.UserId = currentUserId)
            End If

            ViewBag.EnregCount = entities.Count

            Select Case sortOrder

                Case "IdLieu"
                    entities = entities.OrderBy(Function(e) e.LIEUX.LIBELLE)
                Case "IdLieu_desc"
                    entities = entities.OrderByDescending(Function(e) e.LIEUX.LIBELLE)

                Case "Userid"
                    entities = entities.OrderBy(Function(e) e.UserId)
                Case "Userid_desc"
                    entities = entities.OrderByDescending(Function(e) e.UserId)

                Case "Libelle"
                    entities = entities.OrderBy(Function(e) e.TelPresiSce)
                Case "Libelle_desc"
                    entities = entities.OrderByDescending(Function(e) e.TelPresiSce)

                Case "Motif"
                    entities = entities.OrderBy(Function(e) e.Objet)
                Case "Motif_desc"
                    entities = entities.OrderByDescending(Function(e) e.Objet)

                Case "DateDebut"
                    entities = entities.OrderBy(Function(e) e.DATE_DEBUT)
                Case "DateDebut_desc"
                    entities = entities.OrderByDescending(Function(e) e.DATE_DEBUT)

                Case "DateFin"
                    entities = entities.OrderBy(Function(e) e.DATE_FIN)
                Case "DateFin_desc"
                    entities = entities.OrderByDescending(Function(e) e.DATE_FIN)

                Case "HeureDebut"
                    entities = entities.OrderBy(Function(e) e.HEURE_DEBUT)
                Case "HeureDebut_desc"
                    entities = entities.OrderByDescending(Function(e) e.HEURE_DEBUT)

                Case "HeureFin"
                    entities = entities.OrderBy(Function(e) e.HEURE_FIN)
                Case "HeureFin_desc"
                    entities = entities.OrderByDescending(Function(e) e.HEURE_FIN)

                Case "Etat"
                    entities = entities.OrderBy(Function(e) e.CurrentState)
                Case "Etat_desc"
                    entities = entities.OrderByDescending(Function(e) e.CurrentState)

                Case "NbrePers"
                    entities = entities.OrderBy(Function(e) e.NBRE_PERS)
                Case "NbrePers_desc"
                    entities = entities.OrderByDescending(Function(e) e.NBRE_PERS)

                Case "Details"
                    entities = entities.OrderBy(Function(e) e.DETAILS)
                Case "Details_desc"
                    entities = entities.OrderByDescending(Function(e) e.DETAILS)

                Case Else
                    entities = entities.OrderByDescending(Function(e) e.DATE_DEBUT)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: /Reservation/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                Return HttpNotFound()
            End If
            Return View(reservation)
        End Function

        ' GET: /Reservation/Create
        Function Create(StartTime As DateTime?, EndTime As DateTime?) As ActionResult
            ViewBag.activeMenu = "oper-1"
            Dim model As New ReservationViewModel
            If StartTime.HasValue Then
                model.DATE_DEBUT = StartTime.Value.Date
                model.HEURE_DEBUT = StartTime.Value
            End If
            If EndTime.HasValue Then
                model.DATE_FIN = EndTime.Value.Date
                model.HEURE_FIN = EndTime.Value
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", model.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", model.ThemeId)
            Return View(model)
        End Function

        ' POST: /Reservation/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal reservationVM As ReservationViewModel) As ActionResult
            If reservationVM.HEURE_DEBUT = reservationVM.HEURE_DEBUT.Date Then
                ModelState.AddModelError("HEURE_DEBUT", "Veuillez entrer une heure valide")
            End If
            If reservationVM.HEURE_FIN = reservationVM.HEURE_FIN.Date Then
                ModelState.AddModelError("HEURE_FIN", "Veuillez entrer une heure valide")
            End If
            If reservationVM.HEURE_DEBUT = reservationVM.HEURE_FIN Then
                ModelState.AddModelError("HEURE_FIN", "Veuillez entrer une heure fin valide")
            End If
            ' la réservation
            Dim reservation As RESERVATION = reservationVM.getReservation(db)
            If ModelState.IsValid Then
                Dim lst = getCollisions(reservation.ID_LIEU, reservation.HEURE_DEBUT, reservation.HEURE_FIN, reservation.ID_RESERVATION)
                If lst.Count = 0 Then
                    reservation.UserId = User.Identity.GetUserId()
                    reservation.DATE_CREATION = Now

                    ' Pour le SCC, c'est la validation directe
                    If User.IsInRole("SCC") Then
                        reservation.ETAT = EnumReservation.Confirmed
                    Else
                        reservation.ETAT = EnumReservation.Initial ' Etat initial
                    End If

                    db.RESERVATION.Add(reservation)
                    Try
                        db.SaveChanges()

                        ' Envoyer Mail et SMS
                        If reservation.ETAT = EnumReservation.Confirmed Then
                            SendMailAndSms(reservation, db)
                        End If

                        Return RedirectToAction("Calendar", New With {.id_lieu = reservation.ID_LIEU, .date_deb = reservation.DATE_DEBUT})
                    Catch ex As Exception
                        Util.getError(ex, ModelState)
                    End Try
                Else
                    For Each r In lst
                        ModelState.AddModelError("", String.Format("{0} : {1} {2} {3} - {4}", Resource.rese_collision, r.Objet, r.HEURE_DEBUT.ToShortDateString, r.HEURE_DEBUT.ToShortTimeString, r.HEURE_FIN.ToShortTimeString))
                    Next
                End If
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservation.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservation.ThemeId)
                Return View(reservationVM)
        End Function

        ' GET: /Reservation/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                Return HttpNotFound()
            End If
            Dim model As New ReservationViewModel
            model.LoadEntity(reservation)

            ' Positionner l'etat
            If model.Etat = EnumReservation.Confirmed And
                            model.CurrentState <> EnumCurrentState.CanceledPending And
                            model.CurrentState <> EnumCurrentState.ReportedPending Then
                model.ManualState = True
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservation.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservation.ThemeId)
            Return View(model)
        End Function

        ' POST: /Reservation/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(reservationVM As ReservationViewModel) As ActionResult
            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservationVM.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservationVM.ThemeId)
            ' la réservation
            Dim reserv As RESERVATION = reservationVM.getReservation(db)
            If ModelState.IsValid Then
                Dim lst = getCollisions(reserv.ID_LIEU, reserv.HEURE_DEBUT, reserv.HEURE_FIN, reserv.ID_RESERVATION)
                If lst.Count = 0 Then
                    If reserv.ID_RESERVATION <= 0 Then
                        reserv.UserId = User.Identity.GetUserId()

                        db.RESERVATION.Add(reserv)
                    Else
                        ' Validation auto des statuts, 
                        ' en fait, si le user Poste, c'est qu'il a cliqué sur le bouton "Valider"
                        ' Seul le scc peut le faire
                        If User.IsInRole("SCC") AndAlso Not reservationVM.ManualState Then
                            If reserv.CurrentState = EnumCurrentState.CanceledPending Then
                                'actionName = "Valider Annulation"
                                reserv.CurrentState = EnumCurrentState.Canceled
                                reserv.ETAT = EnumReservation.Confirmed
                            ElseIf reserv.CurrentState = EnumCurrentState.ReportedPending Then
                                'actionName = "Valider Report"
                                reserv.CurrentState = EnumCurrentState.Reported
                                reserv.ETAT = EnumReservation.Confirmed
                            ElseIf reserv.ETAT <> EnumReservation.Confirmed Then
                                'actionName = "Valider"
                                reserv.ETAT = EnumReservation.Confirmed
                            End If
                        End If

                        db.Entry(reserv).State = EntityState.Modified
                    End If

                    Try
                        db.SaveChanges()

                        ' Envoyer Mail et SMS
                        If reserv.ETAT = EnumReservation.Confirmed Then
                            SendMailAndSms(reserv, db)
                        End If

                        Return RedirectToAction("Index")
                    Catch ex As Exception
                        Util.getError(ex, ModelState)
                    End Try
                Else
                    For Each r In lst
                        ModelState.AddModelError("", String.Format("{0} : {1} {2} {3} - {4}", Resource.rese_collision, r.Objet, r.HEURE_DEBUT.ToShortDateString, r.HEURE_DEBUT.ToShortTimeString, r.HEURE_FIN.ToShortTimeString))
                    Next
                End If
            End If
                Return View(reservationVM)
        End Function

        ' GET: /Reservation/Edit/5
        Function Reporter(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                Return HttpNotFound()
            End If
            If reservation.UserId <> User.Identity.GetUserId Then
                ModelState.AddModelError("", My.Resources.Resource.rese_your_are_not_owner)
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservation.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservation.ThemeId)
            Return View(reservation)
        End Function

        ' POST: /Reservation/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Reporter(ByVal reservation As RESERVATION) As ActionResult
            If reservation.UserId <> User.Identity.GetUserId Then
                ModelState.AddModelError("", My.Resources.Resource.rese_your_are_not_owner)
            End If

            If ModelState.IsValid Then
                ' Récuperer une copie de l'objet en question
                Dim date_debut = reservation.DATE_DEBUT
                reservation.DATE_DEBUT = date_debut.Add(reservation.HEURE_DEBUT.TimeOfDay)
                reservation.DATE_FIN = date_debut.Add(reservation.HEURE_FIN.TimeOfDay)
                reservation.HEURE_DEBUT = reservation.DATE_DEBUT
                reservation.HEURE_FIN = reservation.DATE_FIN

                Dim lst = getCollisions(reservation.ID_LIEU, reservation.HEURE_DEBUT, reservation.HEURE_FIN, reservation.ID_RESERVATION)
                If lst.Count = 0 Then
                    ' Si l'objet n'est pas validé, on le modifie
                    If reservation.ETAT = EnumReservation.Initial Then

                        db.Entry(reservation).State = EntityState.Modified
                    ElseIf reservation.ETAT = EnumReservation.Confirmed Then
                        Dim old_reserv = db.RESERVATION.Find(reservation.ID_RESERVATION)

                        old_reserv.CurrentState = EnumCurrentState.ReportedPending
                        old_reserv.NextReservation = reservation
                        db.Entry(old_reserv).State = EntityState.Modified

                        reservation.ETAT = EnumReservation.Initial
                        reservation.CurrentState = EnumCurrentState.Initial
                        db.RESERVATION.Add(reservation)
                    End If

                    Try
                        db.SaveChanges()

                        Return RedirectToAction("Calendar", New With {.id_lieu = reservation.ID_LIEU, .date_deb = reservation.DATE_DEBUT})
                    Catch ex As Exception
                        Util.getError(ex, ModelState)
                    End Try
                Else
                    For Each r In lst
                        ModelState.AddModelError("", String.Format("{0} : {1} {2} {3} - {4}", Resource.rese_collision, r.Objet, r.HEURE_DEBUT.ToShortDateString, r.HEURE_DEBUT.ToShortTimeString, r.HEURE_FIN.ToShortTimeString))
                    Next
                End If
            End If

            ViewBag.LIEUX = New SelectList(db.LIEUX.Where(Function(e) e.ID_LIEU > 0 And e.Etat = 0).OrderBy(Function(e) e.LIBELLE), "ID_LIEU", "LIBELLE", reservation.ID_LIEU)
            ViewBag.Themes = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "Id", "Libelle", reservation.ThemeId)
                Return View(reservation)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function UpdateStatus(ByVal id? As Long, CurrentState As EnumCurrentState) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                Return HttpNotFound()
            End If
            Try
                reservation.CurrentState = CurrentState

                db.Entry(reservation).State = EntityState.Modified
                db.SaveChanges()

            Catch ex As Exception
                ViewBag.ErrorMsg = ex.Message
                ModelState.AddModelError("", ex.Message)
            End Try

            Return RedirectToAction("Calendar", New With {.id_lieu = reservation.ID_LIEU, .date_deb = reservation.DATE_DEBUT})
        End Function

        ' GET: /Reservation/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            If IsNothing(reservation) Then
                Return HttpNotFound()
            End If
            Return View(reservation)
        End Function

        ' POST: /Reservation/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim reservation As RESERVATION = db.RESERVATION.Find(id)
            db.RESERVATION.Remove(reservation)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Public Sub New()
            ViewBag.activeMenu = "oper-2"
        End Sub

        Private Sub SendSms(num As String, msg As String)
            Using serialport1 = New SerialPort With {
                                                        .PortName = ConfigurationManager.AppSettings("SMSComPort"),
                                                        .BaudRate = 9600,
                                                        .Parity = Parity.None,
                                                        .DataBits = 8,
                                                        .StopBits = StopBits.One,
                                                        .Handshake = Handshake.RequestToSend,
                                                        .DtrEnable = True,
                                                        .NewLine = vbCrLf,
                                                        .ReadBufferSize = 10000,
                                                        .ReadTimeout = 1000,
                                                        .WriteBufferSize = 10000,
                                                        .WriteTimeout = 10000,
                                                        .RtsEnable = True,
                                                        .Encoding = System.Text.Encoding.UTF8
                                                    }


                Dim quote As String = """"

                Try
                    serialport1.Open()
                    If (serialport1.IsOpen) Then
                        Trace.WriteLine("ouverture du port")

                        'Thread.Sleep(1000)
                        serialport1.WriteLine("AT") ' verifi si le modem est ok
                        'Thread.Sleep(1000)
                        'tb_console.Text &= "resultat execution cmd AT= " + SerialPort1.ReadExisting.ToString
                        serialport1.WriteLine("AT+CMGF=1" & vbCrLf) 'changer le mode d'envoie de donné (on passe en mode texe)
                        'Thread.Sleep(1000)
                        'tb_console.Text &= "resultat execution cmd AT+CMGF=1= " + SerialPort1.ReadExisting.ToString
                        serialport1.WriteLine("AT+CMGS=" & quote & num & quote & vbCrLf) ' on indique le munero du destinataire
                        'Thread.Sleep(1000)
                        'MessageBox.Show("resultat execution cmd AT+CMGS= " + SerialPort1.ReadExisting.ToString)
                        serialport1.WriteLine(msg & vbCrLf & Chr(26)) 'on envoie le sms
                        'Thread.Sleep(1000)
                        'tb_console.Text &= "resultat execution last cmd = " + SerialPort1.ReadExisting.ToString
                        Trace.WriteLine(serialport1.ReadExisting.ToString)
                        serialport1.WriteLine("AT+CMGF=0" & vbCrLf) 'changer le mode d'envoie de donné (on repasse en mode pdu)
                        'Thread.Sleep(1000)


                        serialport1.Close()
                    Else
                        Trace.WriteLine("impossible d'ouvrir le port")

                    End If
                Catch ex As Exception
                    Trace.WriteLine(ex.Message)
                End Try
            End Using
        End Sub

        Private Sub SendMailAndSms(reserv As RESERVATION, db As ApplicationDbContext)
            Dim lib_lieu = (From e In db.LIEUX
                           Where e.ID_LIEU = reserv.ID_LIEU
                           Select e.LIBELLE).FirstOrDefault
            Dim sms As New StringBuilder

            sms.AppendFormat("SPM alert {0}. ", reserv.Objet)
            sms.AppendFormat("Date {0} à {1}. ", reserv.DATE_DEBUT.ToLongDateString, reserv.HEURE_DEBUT.ToShortTimeString)
            sms.AppendFormat("Lieu {0}. ", lib_lieu)
            sms.AppendFormat("Président de séance {0}. ", reserv.PresidentSeance)
            sms.Append("Merci.")

            'Dim ANSI As Encoding = Encoding.GetEncoding("utf-8")
            Dim msg_ansi = RemoveAccent(sms.ToString).ToUpper ' ANSI.GetString(ANSI.GetBytes(sms.ToString))

            For Each parti In reserv.PARTICIPANT
                If Not String.IsNullOrWhiteSpace(parti.TELEPHONE) Then
                    System.Threading.Thread.Sleep(2000)
                    SendSms(parti.TELEPHONE, msg_ansi)

                    System.Threading.Thread.Sleep(2000)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Fonction de conversion de chaîne accentué en chaîne sans accent
        ''' </summary>
        ''' <param name="chaine">La chaine à convertir</param>
        ''' <returns>string</returns>
        Private Function RemoveAccent(chaine As String) As String
            ' Déclaration de variables
            Dim accent As String = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç"
            Dim sansAccent As String = "AAAAAAaaaaaaOOOOOOooooooEEEEeeeeIIIIiiiiUUUUuuuuyNnCc"

            ' Pour chaque accent
            For i As Integer = 0 To accent.Length - 1
                ' Remplacement de l'accent par son équivalent sans accent dans la chaîne de caractères
                chaine = chaine.Replace(accent(i), sansAccent(i))
            Next

            ' Retour du résultat
            Return chaine
        End Function

    End Class
End Namespace
