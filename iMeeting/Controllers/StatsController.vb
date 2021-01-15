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
Imports System.Net.Mail

Public Class StatsController
    Inherits BaseController

    ' GET: /Stats
    Function Index() As ActionResult
        ViewBag.activeMenu = "stats-1"
        Dim db As New ApplicationDbContext

        Dim pieData = From e In db.RESERVATION
                       Where e.ETAT = EnumReservation.Confirmed
                       Group By key = e.LIEUX.LIBELLE
                       Into Reserv = Group, Count()
                       Select key, y = Reserv.Count

        ViewBag.pieData = pieData.ToList

        Dim bar1Data = From e In db.RESERVATION, u In db.Users
                       Where e.UserId = u.Id And e.LIEUX.TYPE_LIEU = 0 And e.ETAT = EnumReservation.Confirmed
                       Group By label = u.SERVICE.LIBELLE
                       Into Reserv = Group, Count()
                       Select label, value = Reserv.Count

        ViewBag.bar1Data = bar1Data.ToList

        Dim bar2Data = From e In db.RESERVATION, u In db.Users
                       Where e.UserId = u.Id And e.LIEUX.TYPE_LIEU = 1 And e.ETAT = EnumReservation.Confirmed
                       Group By label = u.SERVICE.LIBELLE
                       Into Reserv = Group, Count()
                       Select label, value = Reserv.Count

        ViewBag.bar2Data = bar2Data.ToList

        Return View()
    End Function

  
    Function Statspartheme(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, id_theme As Long?, date_debut As String, date_fin As String, date_debut_iso As DateTime?, date_fin_iso As DateTime?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.activeMenu = "stats-2_3"
        Dim db = New ApplicationDbContext
        ViewBag.THEME = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "ID", "LIBELLE")
        ViewBag.IdLieuSortParm = If(String.IsNullOrEmpty(sortOrder), "IdLieu_desc", "")
        ViewBag.UseridSortParm = If(sortOrder = "Userid", "Userid_desc", "Userid")
        ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
        ViewBag.MotifSortParm = If(sortOrder = "Motif", "Motif_desc", "Motif")
        ViewBag.DateDebutSortParm = If(sortOrder = "DateDebut", "DateDebut_desc", "DateDebut")
        ViewBag.DateFinSortParm = If(sortOrder = "DateFin", "DateFin_desc", "DateFin")
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
                       Where e.ETAT = EnumReservation.Confirmed
                       Order By e.ID_LIEU, e.HEURE_DEBUT

        ViewBag.date_debut = date_debut
        ViewBag.date_fin = date_fin
        ViewBag.id_theme = id_theme

        ' Pour effacer les champs après un post
        ' http://stackoverflow.com/questions/18661401/how-to-use-modelstate-to-clear-my-form-after-i-post-my-form-data-my-view-model-i
        ModelState.Remove("date_debut")
        ModelState.Remove("date_fin")

        If id_theme.HasValue Then
            entities = entities.Where(Function(e) e.ThemeId = id_theme.Value)
        End If
        If Not String.IsNullOrWhiteSpace(date_debut) Then
            entities = entities.Where(Function(e) e.DATE_DEBUT >= date_debut_iso.Value)
            ViewBag.date_debut_iso = date_debut_iso
        End If
        If Not String.IsNullOrWhiteSpace(date_fin) Then
            entities = entities.Where(Function(e) e.DATE_FIN <= date_fin_iso.Value)
            ViewBag.date_fin_iso = date_fin_iso
        End If
        If Not String.IsNullOrEmpty(searchString) Then
            entities = entities.Where(Function(e) e.TelPresiSce.ToUpper.Contains(searchString.ToUpper) Or e.Objet.ToUpper.Contains(searchString.ToUpper))
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

            Case "NbrePers"
                entities = entities.OrderBy(Function(e) e.NBRE_PERS)
            Case "NbrePers_desc"
                entities = entities.OrderByDescending(Function(e) e.NBRE_PERS)

            Case "Details"
                entities = entities.OrderBy(Function(e) e.DETAILS)
            Case "Details_desc"
                entities = entities.OrderByDescending(Function(e) e.DETAILS)
            Case Else
                entities = entities.OrderByDescending(Function(e) e.HEURE_DEBUT)
        End Select

        Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
        Dim pageNumber As Integer = If(page, 1)

        Return View(entities.ToPagedList(pageNumber, pageSize))
    End Function

  
    Function Statsparsal(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, respo As Long?, date_debut As String, date_fin As String, date_debut_iso As DateTime?, date_fin_iso As DateTime?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.activeMenu = "stats-2_2"
        Dim db = New ApplicationDbContext
        ViewBag.LIEUX = New SelectList(db.LIEUX, "ID_LIEU", "LIBELLE")
        ViewBag.IdLieuSortParm = If(String.IsNullOrEmpty(sortOrder), "IdLieu_desc", "")
        ViewBag.UseridSortParm = If(sortOrder = "Userid", "Userid_desc", "Userid")
        ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
        ViewBag.MotifSortParm = If(sortOrder = "Motif", "Motif_desc", "Motif")
        ViewBag.DateDebutSortParm = If(sortOrder = "DateDebut", "DateDebut_desc", "DateDebut")
        ViewBag.DateFinSortParm = If(sortOrder = "DateFin", "DateFin_desc", "DateFin")
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
                       Where e.ETAT = EnumReservation.Confirmed
                       Order By e.ID_LIEU, e.HEURE_DEBUT

        ViewBag.date_debut = date_debut
        ViewBag.date_fin = date_fin
        ViewBag.respo = respo

        ' Pour effacer les champs après un post
        ' http://stackoverflow.com/questions/18661401/how-to-use-modelstate-to-clear-my-form-after-i-post-my-form-data-my-view-model-i
        ModelState.Remove("date_debut")
        ModelState.Remove("date_fin")

        If respo.HasValue Then
            entities = entities.Where(Function(e) e.ID_LIEU = respo.Value)
        End If
        If Not String.IsNullOrWhiteSpace(date_debut) Then
            entities = entities.Where(Function(e) e.DATE_DEBUT >= date_debut_iso.Value)
            ViewBag.date_debut_iso = date_debut_iso
        End If
        If Not String.IsNullOrWhiteSpace(date_fin) Then
            entities = entities.Where(Function(e) e.DATE_FIN <= date_fin_iso.Value)
            ViewBag.date_fin_iso = date_fin_iso
        End If
        If Not String.IsNullOrEmpty(searchString) Then
            entities = entities.Where(Function(e) e.TelPresiSce.ToUpper.Contains(searchString.ToUpper) Or e.Objet.ToUpper.Contains(searchString.ToUpper))
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

            Case "NbrePers"
                entities = entities.OrderBy(Function(e) e.NBRE_PERS)
            Case "NbrePers_desc"
                entities = entities.OrderByDescending(Function(e) e.NBRE_PERS)

            Case "Details"
                entities = entities.OrderBy(Function(e) e.DETAILS)
            Case "Details_desc"
                entities = entities.OrderByDescending(Function(e) e.DETAILS)
            Case Else
                entities = entities.OrderByDescending(Function(e) e.HEURE_DEBUT)
        End Select

        Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
        Dim pageNumber As Integer = If(page, 1)

        Return View(entities.ToPagedList(pageNumber, pageSize))
    End Function

    Function Statsparrespo(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, respo As String, date_debut As String, date_fin As String, date_debut_iso As DateTime?, date_fin_iso As DateTime?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.activeMenu = "stats-2_1"
        Dim db = New ApplicationDbContext
        ViewBag.THEME = New SelectList(db.Theme.OrderBy(Function(e) e.Libelle), "ID", "LIBELLE")
        ViewBag.IdLieuSortParm = If(String.IsNullOrEmpty(sortOrder), "IdLieu_desc", "")
        ViewBag.UseridSortParm = If(sortOrder = "Userid", "Userid_desc", "Userid")
        ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
        ViewBag.MotifSortParm = If(sortOrder = "Motif", "Motif_desc", "Motif")
        ViewBag.DateDebutSortParm = If(sortOrder = "DateDebut", "DateDebut_desc", "DateDebut")
        ViewBag.DateFinSortParm = If(sortOrder = "DateFin", "DateFin_desc", "DateFin")
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
                       Where e.ETAT = EnumReservation.Confirmed
                       Order By e.ID_LIEU, e.HEURE_DEBUT

        ViewBag.date_debut = date_debut
        ViewBag.date_fin = date_fin
        ViewBag.id_theme = respo

        ' Pour effacer les champs après un post
        ' http://stackoverflow.com/questions/18661401/how-to-use-modelstate-to-clear-my-form-after-i-post-my-form-data-my-view-model-i
        ModelState.Remove("date_debut")
        ModelState.Remove("date_fin")

        If Not String.IsNullOrWhiteSpace(respo) Then
            entities = entities.Where(Function(e) e.PresidentSeance = respo)
        End If
        If Not String.IsNullOrWhiteSpace(date_debut) Then
            entities = entities.Where(Function(e) e.DATE_DEBUT >= date_debut_iso.Value)
            ViewBag.date_debut_iso = date_debut_iso
        End If
        If Not String.IsNullOrWhiteSpace(date_fin) Then
            entities = entities.Where(Function(e) e.DATE_FIN <= date_fin_iso.Value)
            ViewBag.date_fin_iso = date_fin_iso
        End If
        If Not String.IsNullOrEmpty(searchString) Then
            entities = entities.Where(Function(e) e.TelPresiSce.ToUpper.Contains(searchString.ToUpper) Or e.Objet.ToUpper.Contains(searchString.ToUpper))
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

            Case "NbrePers"
                entities = entities.OrderBy(Function(e) e.NBRE_PERS)
            Case "NbrePers_desc"
                entities = entities.OrderByDescending(Function(e) e.NBRE_PERS)

            Case "Details"
                entities = entities.OrderBy(Function(e) e.DETAILS)
            Case "Details_desc"
                entities = entities.OrderByDescending(Function(e) e.DETAILS)
            Case Else
                entities = entities.OrderByDescending(Function(e) e.HEURE_DEBUT)
        End Select

        Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
        Dim pageNumber As Integer = If(page, 1)

        Return View(entities.ToPagedList(pageNumber, pageSize))
    End Function
End Class