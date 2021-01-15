Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports PagedList

Namespace iMeeting
    Public Class ParticipantController
        Inherits BaseController

        Private db As New ApplicationDbContext

        <HttpPost()> _
        Public Function Autocomplete(term As String) As ActionResult
            Dim results = (From e In db.PARTICIPANT
                Where (e.EMAIL.ToLower.Contains(term.ToLower) Or e.NOM.ToLower.Contains(term.ToLower))
                Select email = e.EMAIL, nom = e.NOM, prenom = e.PRENOM, fonction = e.FONCTION,
                        label = e.NOM & " " & e.PRENOM,
                        tel = e.TELEPHONE).Distinct

            'Dim count = results.Count

            Return Json(results, JsonRequestBehavior.AllowGet)
        End Function

        ' GET: /Participant/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            ViewBag.NomSortParm = If(String.IsNullOrEmpty(sortOrder), "Nom_desc", "")
            ViewBag.PrenomSortParm = If(sortOrder = "Prenom", "Prenom_desc", "Prenom")
            ViewBag.FonctionSortParm = If(sortOrder = "Fonction", "Fonction_desc", "Fonction")
            ViewBag.EmailSortParm = If(sortOrder = "Email", "Email_desc", "Email")
            ViewBag.TelephoneSortParm = If(sortOrder = "Telephone", "Telephone_desc", "Telephone")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.PARTICIPANT

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.NOM.ToUpper.Contains(searchString.ToUpper) Or e.PRENOM.ToUpper.Contains(searchString.ToUpper) Or
                                              e.FONCTION.ToUpper.Contains(searchString.ToUpper) Or e.TELEPHONE.ToUpper.Contains(searchString.ToUpper) Or
                                              e.EMAIL.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder

                Case "Nom"
                    entities = entities.OrderBy(Function(e) e.NOM)
                Case "Nom_desc"
                    entities = entities.OrderByDescending(Function(e) e.NOM)

                Case "Prenom"
                    entities = entities.OrderBy(Function(e) e.PRENOM)
                Case "Prenom_desc"
                    entities = entities.OrderByDescending(Function(e) e.PRENOM)

                Case "Fonction"
                    entities = entities.OrderBy(Function(e) e.FONCTION)
                Case "Fonction_desc"
                    entities = entities.OrderByDescending(Function(e) e.FONCTION)

                Case "Email"
                    entities = entities.OrderBy(Function(e) e.EMAIL)
                Case "Email_desc"
                    entities = entities.OrderByDescending(Function(e) e.EMAIL)

                Case "Telephone"
                    entities = entities.OrderBy(Function(e) e.TELEPHONE)
                Case "Telephone_desc"
                    entities = entities.OrderByDescending(Function(e) e.TELEPHONE)

                Case Else
                    entities = entities.OrderBy(Function(e) e.NOM)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function


        ' GET: /Participant/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As PARTICIPANT = db.PARTICIPANT.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' GET: /Participant/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: /Participant/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include := "ID_PARTICIPANT,NOM,PRENOM,FONCTION,EMAIL,TELEPHONE")> ByVal participant As PARTICIPANT) As ActionResult
            If ModelState.IsValid Then
                db.PARTICIPANT.Add(participant)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If 
            Return View(participant)
        End Function

        ' GET: /Participant/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As PARTICIPANT = db.PARTICIPANT.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' POST: /Participant/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include := "ID_PARTICIPANT,NOM,PRENOM,FONCTION,EMAIL,TELEPHONE")> ByVal participant As PARTICIPANT) As ActionResult
            If ModelState.IsValid Then
                db.Entry(participant).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(participant)
        End Function

        ' GET: /Participant/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As PARTICIPANT = db.PARTICIPANT.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' POST: /Participant/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim participant As PARTICIPANT = db.PARTICIPANT.Find(id)
            db.PARTICIPANT.Remove(participant)
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
            ViewBag.activeMenu = "oper-3"
        End Sub
    End Class
End Namespace
