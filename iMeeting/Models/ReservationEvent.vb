Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Globalization ' << dont forget to add this for converting dates to localtime
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class ReservationEvent

    Public ID_Reservation As Integer
    Public Title As String
    Public ID_Lieu As Integer
    Public StartDateString As String
    Public EndDateString As String
    Public StatusString As String
    Public StatusColor As String
    Public ClassName As String

    Property Objet As String

    Property PresidentSeance As String

    Property CurrentState As Integer
    Property CurrentStateName As String
    Property Lieu As String
    Property State As EnumReservation

    Property UserId As String

    Property StateName As String

    Property currentStateDisplayName As String

    Public Shared Function getDisplayEnum(model As EnumCurrentState) As String
        Select Case model
            Case EnumCurrentState.Canceled
                Return Resource.rese_cs_canceled
            Case EnumCurrentState.CanceledPending
                Return Resource.rese_cs_canceled_pending
            Case EnumCurrentState.EnCours
                Return Resource.rese_cs_encours
            Case EnumCurrentState.EnRetard
                Return Resource.rese_cs_enretard
            Case EnumCurrentState.Reported
                Return Resource.rese_cs_reported
            Case EnumCurrentState.ReportedPending
                Return Resource.rese_cs_reportedpending
            Case EnumCurrentState.Terminee
                Return Resource.rese_cs_ended
            Case Else
                Return Resource.rese_cs_initial
        End Select
    End Function

    Public Shared Function LoadAllReservationsInDateRange(start As DateTime, [end] As DateTime, id_lieu As Integer) As List(Of ReservationEvent)
        Dim fromDate = start
        Dim toDate = [end]
        Using ent As New ApplicationDbContext()
            Dim rslt = From e In ent.RESERVATION.Include("LIEUX")
                       Where e.DATE_DEBUT >= fromDate And e.DATE_FIN <= toDate And
                            e.ID_LIEU = id_lieu
                       Select e

            Dim result As New List(Of ReservationEvent)()
            For Each item As RESERVATION In rslt
                Dim rec As New ReservationEvent()
                Dim CanAddItem As Boolean = True
                rec.ID_Reservation = item.ID_RESERVATION
                rec.ID_Lieu = item.ID_LIEU
                rec.StartDateString = item.HEURE_DEBUT.ToString("s") ' "s" is a preset format that outputs as: "2009-02-27T12:12:22" 
                rec.EndDateString = item.HEURE_FIN.ToString("s")
                rec.Title = item.Objet
                rec.Objet = item.Objet
                rec.PresidentSeance = item.PresidentSeance
                rec.CurrentState = item.CurrentState
                rec.CurrentStateName = item.CurrentState.ToString
                rec.currentStateDisplayName = getDisplayEnum(item.CurrentState) '.ToString
                rec.Lieu = item.LIEUX.LIBELLE
                ' Eléments passés ?
                If item.HEURE_DEBUT.Date < Now.Date Then
                    ' On n'affiche pas les éléments passés non validés
                    If item.ETAT = EnumReservation.Initial Then
                        CanAddItem = False
                    End If
                    item.ETAT = EnumReservation.Passed
                End If

                ' Si la réservation est validée ou a un état courant significatif, prendre son CurrentState Code, sinon, on affiche son State
                If item.ETAT = EnumReservation.Confirmed Or item.CurrentState <> EnumCurrentState.Initial Then
                    rec.StatusString = Enums.GetName(Of EnumCurrentState)(item.CurrentState)
                    rec.StatusColor = Enums.GetEnumDescription(Of EnumCurrentState)(rec.StatusString)
                Else
                    rec.StatusString = Enums.GetName(Of EnumReservation)(item.ETAT)
                    rec.StatusColor = Enums.GetEnumDescription(Of EnumReservation)(rec.StatusString)
                End If
                Dim ColorCode As String = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"))
                rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1)
                rec.StatusColor = ColorCode
                rec.State = item.ETAT
                rec.StateName = item.ETAT.ToString
                rec.UserId = item.UserId
                If CanAddItem Then
                    result.Add(rec)
                End If
            Next

            Return result
        End Using

    End Function

    Public Shared Function LoadReservationsSummaryInDateRange(start As DateTime, [end] As DateTime, id_lieu As Integer) As List(Of ReservationEvent)
        Dim fromDate = start 'ConvertFromUnixTimestamp(start)
        Dim toDate = [end] ' ConvertFromUnixTimestamp([end])
        Using ent As New ApplicationDbContext()
            Dim rslt = ent.RESERVATION.Where(Function(s) s.DATE_DEBUT >= fromDate AndAlso s.DATE_FIN <= toDate).GroupBy(Function(s) s.DATE_DEBUT.Date).[Select](Function(x) New With { _
                 .DateTimeScheduled = x.Key, _
                 .Count = x.Count() _
            })

            Dim result As New List(Of ReservationEvent)()
            Dim i As Integer = 0
            For Each item As Object In rslt
                Dim rec As New ReservationEvent()
                rec.ID_Reservation = i
                'we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                rec.ID_Lieu = -1
                Dim StringDate As String = String.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled)
                rec.StartDateString = StringDate & Convert.ToString("T00:00:00")
                'ISO 8601 format
                rec.EndDateString = StringDate & Convert.ToString("T23:59:59")
                rec.Title = "Booked: " + item.Count.ToString()
                result.Add(rec)
                i += 1
            Next

            Return result
        End Using

    End Function

    Public Shared Sub UpdateDiaryEvent(id As Integer, NewEventStart As String, NewEventEnd As String)
        ' EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
        Using ent As New ApplicationDbContext()
            Dim rec = ent.RESERVATION.FirstOrDefault(Function(s) s.ID_RESERVATION = id)
            If rec IsNot Nothing Then
                Dim DateTimeStart As DateTime = DateTime.Parse(NewEventStart, Nothing, DateTimeStyles.RoundtripKind).ToLocalTime()
                ' and convert offset to localtime
                rec.HEURE_DEBUT = DateTimeStart
                If Not [String].IsNullOrEmpty(NewEventEnd) Then
                    'Dim span As TimeSpan = DateTime.Parse(NewEventEnd, Nothing, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart
                    'rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes)
                    Dim DateEndStart As DateTime = DateTime.Parse(NewEventStart, Nothing, DateTimeStyles.RoundtripKind).ToLocalTime()
                    rec.HEURE_FIN = DateEndStart
                End If
                ent.SaveChanges()
            End If
        End Using

    End Sub


    Private Shared Function ConvertFromUnixTimestamp(timestamp As Double) As DateTime
        Dim origin = New DateTime(1970, 1, 1, 0, 0, 0, _
            0)
        Return origin.AddSeconds(timestamp)
    End Function


    Public Shared Function CreateNewEvent(Title As String, NewEventDate As String, NewEventTime As String, NewEventDuration As String) As Boolean
        Using ent As New ApplicationDbContext()
            Try
                Dim rec As New RESERVATION()
                rec.TelPresiSce = Title
                'rec.DateTimeScheduled = DateTime.ParseExact(Convert.ToString(NewEventDate & Convert.ToString(" ")) & NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                'rec.AppointmentLength = Int32.Parse(NewEventDuration)
                rec.DATE_DEBUT = DateTime.ParseExact(Convert.ToString(NewEventDate & Convert.ToString(" ")) & NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                rec.HEURE_DEBUT = DateTime.ParseExact(Convert.ToString(NewEventDate & Convert.ToString(" ")) & NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                rec.DATE_FIN = rec.DATE_DEBUT
                rec.HEURE_FIN = rec.HEURE_DEBUT.AddMinutes(Int32.Parse(NewEventDuration))
                rec.ETAT = EnumReservation.Initial
                ent.RESERVATION.Add(rec)
                ent.SaveChanges()
            Catch generatedExceptionName As Exception
                Return False
            End Try
            Return True
        End Using
    End Function



End Class
