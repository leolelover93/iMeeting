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
    Public Class ServiceController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: /SERVICE/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            ViewBag.IdBatSortParm = If(String.IsNullOrEmpty(sortOrder), "IdBat_desc", "")
            ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
            ViewBag.DetailsSortParm = If(sortOrder = "Details", "Details_desc", "Details")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.SERVICE.Include(Function(s) s.BATIMENT)

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.LIBELLE.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder

                Case "IdBat"
                    entities = entities.OrderBy(Function(e) e.ID_BAT)
                Case "IdBat_desc"
                    entities = entities.OrderByDescending(Function(e) e.ID_BAT)

                Case "Libelle"
                    entities = entities.OrderBy(Function(e) e.LIBELLE)
                Case "Libelle_desc"
                    entities = entities.OrderByDescending(Function(e) e.LIBELLE)

                Case "Details"
                    entities = entities.OrderBy(Function(e) e.DETAILS)
                Case "Details_desc"
                    entities = entities.OrderByDescending(Function(e) e.DETAILS)

                Case Else
                    entities = entities.OrderBy(Function(e) e.ID_BAT)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function


        ' GET: /Service/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim service As SERVICE = db.SERVICE.Find(id)
            If IsNothing(service) Then
                Return HttpNotFound()
            End If
            Return View(service)
        End Function

        ' GET: /Service/Create
        Function Create() As ActionResult
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE")
            Return View()
        End Function

        ' POST: /Service/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ID_SERVICE,ID_BAT,LIBELLE,DETAILS")> ByVal service As SERVICE) As ActionResult
            If ModelState.IsValid Then
                service.DATE_CREATION = Now
                db.SERVICE.Add(service)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", service.ID_BAT)
            Return View(service)
        End Function

        ' GET: /Service/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim service As SERVICE = db.SERVICE.Find(id)
            If IsNothing(service) Then
                Return HttpNotFound()
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", service.ID_BAT)
            Return View(service)
        End Function

        ' POST: /Service/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include := "ID_SERVICE,ID_BAT,LIBELLE,DETAILS,DATE_CREATION")> ByVal service As SERVICE) As ActionResult
            If ModelState.IsValid Then
                db.Entry(service).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", service.ID_BAT)
            Return View(service)
        End Function

        ' GET: /Service/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim service As SERVICE = db.SERVICE.Find(id)
            If IsNothing(service) Then
                Return HttpNotFound()
            End If
            Return View(service)
        End Function

        ' POST: /Service/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim service As SERVICE = db.SERVICE.Find(id)
            db.SERVICE.Remove(service)
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
            ViewBag.activeMenu = "param-4"
        End Sub
    End Class
End Namespace
