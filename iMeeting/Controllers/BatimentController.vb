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
    Public Class BatimentController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: /BATIMENT/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            ViewBag.LibelleSortParm = If(String.IsNullOrEmpty(sortOrder), "Libelle_desc", "")
            ViewBag.PositionSortParm = If(sortOrder = "Position", "Position_desc", "Position")
            ViewBag.NbrePieceSortParm = If(sortOrder = "NbrePiece", "NbrePiece_desc", "NbrePiece")
            ViewBag.DetailsSortParm = If(sortOrder = "Details", "Details_desc", "Details")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.BATIMENT Where e.ID_BAT > 0

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.LIBELLE.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder

                Case "Libelle"
                    entities = entities.OrderBy(Function(e) e.LIBELLE)
                Case "Libelle_desc"
                    entities = entities.OrderByDescending(Function(e) e.LIBELLE)

                Case "Position"
                    entities = entities.OrderBy(Function(e) e.POSITION)
                Case "Position_desc"
                    entities = entities.OrderByDescending(Function(e) e.POSITION)

                Case "NbrePiece"
                    entities = entities.OrderBy(Function(e) e.NBRE_PIECE)
                Case "NbrePiece_desc"
                    entities = entities.OrderByDescending(Function(e) e.NBRE_PIECE)

                Case "Details"
                    entities = entities.OrderBy(Function(e) e.DETAILS)
                Case "Details_desc"
                    entities = entities.OrderByDescending(Function(e) e.DETAILS)

                Case Else
                    entities = entities.OrderBy(Function(e) e.LIBELLE)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim batiment As BATIMENT = db.BATIMENT.Find(id)
            If IsNothing(batiment) Then
                Return HttpNotFound()
            End If
            Return View(batiment)
        End Function

        ' GET: /Batiment/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: /Batiment/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ID_BAT,LIBELLE,POSITION,NBRE_PIECE,DETAILS")> ByVal batiment As BATIMENT) As ActionResult
            If ModelState.IsValid Then
                batiment.DATE_CREATION = Now
                db.BATIMENT.Add(batiment)
                Try
                    'db.SaveChanges(ModelState)
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As Exception
                End Try
            End If
            Return View(batiment)
        End Function

        ' GET: /Batiment/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim batiment As BATIMENT = db.BATIMENT.Find(id)
            If IsNothing(batiment) Then
                Return HttpNotFound()
            End If
            Return View(batiment)
        End Function

        ' POST: /Batiment/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID_BAT,LIBELLE,POSITION,NBRE_PIECE,DETAILS,DATE_CREATION")> ByVal batiment As BATIMENT) As ActionResult
            If ModelState.IsValid Then
                db.Entry(batiment).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(batiment)
        End Function

        ' GET: /Batiment/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim batiment As BATIMENT = db.BATIMENT.Find(id)
            If IsNothing(batiment) Then
                Return HttpNotFound()
            End If
            Return View(batiment)
        End Function

        ' POST: /Batiment/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim batiment As BATIMENT = db.BATIMENT.Find(id)
            db.BATIMENT.Remove(batiment)
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
            ViewBag.activeMenu = "param-1"
        End Sub
    End Class
End Namespace
