Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail

Public Class Report
    Inherits System.Web.UI.Page

    Public Function getPeriode(date_deb As String, date_fin As String) As String
        If Not String.IsNullOrWhiteSpace(date_deb) And Not String.IsNullOrWhiteSpace(date_fin) Then
            Return String.Format("Du {0}   Au   {1}", CDate(date_deb).ToShortDateString, CDate(date_fin).ToShortDateString)
        ElseIf Not String.IsNullOrWhiteSpace(date_deb) Then
            Return String.Format("A partir   du   {0}", CDate(date_deb).ToShortDateString)
        ElseIf Not String.IsNullOrWhiteSpace(date_fin) Then
            Return String.Format("Avant le   {0}", CDate(date_fin).ToShortDateString)
        Else
            Return ""
        End If
    End Function

    Public Function dsReservation(ByVal id As String, date_deb As String, date_fin As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = " SELECT * FROM v_Reservation Where (Etat = 2) "
        If Not String.IsNullOrWhiteSpace(id) Then
            cmd &= " AND (ID_LIEU = @id)"
        End If
        If Not String.IsNullOrWhiteSpace(date_deb) Then
            cmd &= " AND (DATE_DEBUT >= @date_deb)"
        End If
        If Not String.IsNullOrWhiteSpace(date_fin) Then
            cmd &= " AND (DATE_FIN <= @date_fin)"
        End If

        Using myConnection As New SqlConnection(Util.ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@id", id)
                If Not String.IsNullOrWhiteSpace(date_deb) Then
                    macmd.Parameters.AddWithValue("@date_deb", CDate(date_deb))
                End If
                If Not String.IsNullOrWhiteSpace(date_fin) Then
                    macmd.Parameters.AddWithValue("@date_fin", CDate(date_fin))
                End If
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id As String = Request("id")
            Dim date_debut = Request("date_debut")
            Dim date_fin = Request("date_fin")
            Dim report_type = Request("type")
            showReport(id, date_debut, date_fin)
            If report_type = "mail" Then
                SendPlanningByMail(id, date_debut, date_fin)
            End If
        End If
    End Sub

    Private Sub showReport(id As String, date_debut As String, date_fin As String)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/"), "Reservation.rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim rp0 As ReportParameter = New ReportParameter("periode", getPeriode(date_debut, date_fin))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {rp0})
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", dsReservation(id, date_debut, date_fin)))
    End Sub

    Public Function SendPlanningByMail(id As String, date_debut As String, date_fin As String) As Boolean
        Dim lr = New LocalReport

        lr.ReportPath = Path.Combine(Server.MapPath("~/Report/"), "Reservation.rdlc")
        lr.DataSources.Clear()
        Dim rp0 As ReportParameter = New ReportParameter("periode", getPeriode(date_debut, date_fin))
        lr.SetParameters(New ReportParameter() {rp0})
        lr.DataSources.Add(New ReportDataSource("DataSet1", dsReservation(id, date_debut, date_fin)))

        Dim warnings As Warning() = {}
        Dim streamids As String() = {}
        Dim mimeType As String = ""
        Dim encoding As String = ""
        Dim extension As String = ""

        Dim bytes As Byte() = lr.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)

        Dim s As New MemoryStream(bytes)
        s.Seek(0, SeekOrigin.Begin)

        Dim a As New Attachment(s, "Planning.pdf")

        ' from, to, subject, body
        Dim message As New MailMessage() '"tsoyejr@gmail.com", "tsoyejr@yahoo.fr", "Sogir : Planning présionnel", "Bien vouloir trouver en pièces jointes les planning prévisionnel des réunions.")

        message.From = New MailAddress(ConfigurationManager.AppSettings("MailFrom"))
        message.Subject = "Sogir : Planning présionnel"
        message.Body = "Bien vouloir trouver en pièces jointes les planning prévisionnel des réunions."

        ' Liste des destinataires
        Using db As New ApplicationDbContext
            Dim users_mail = (From e In db.Users
                        Select e.EMAIL).ToList

            For Each email In users_mail
                If Not String.IsNullOrWhiteSpace(email) Then
                    Try
                        message.To.Add(email)
                    Catch ex As Exception
                        'Throw New Exception(ex.Message & " Mail : " & email)
                    End Try
                End If
            Next
        End Using

        message.Attachments.Add(a)

        Dim client As New SmtpClient()

        Try
            client.Send(message)
        Catch ex As Exception
            lblMail.Text = "Erreur : " & ex.Message
        End Try

        lblMail.Visible = True
        Return True
    End Function
End Class