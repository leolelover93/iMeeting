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
    Public Class AffichageController
        Inherits BaseController

        Private db As New ApplicationDbContext


        ' GET: /PT_AFFICHAGE/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            ViewBag.IdBatSortParm = If(String.IsNullOrEmpty(sortOrder), "IdBat_desc", "")
            ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
            ViewBag.EmplacementSortParm = If(sortOrder = "Emplacement", "Emplacement_desc", "Emplacement")
            ViewBag.DetailsSortParm = If(sortOrder = "Details", "Details_desc", "Details")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.PT_AFFICHAGE.Include(Function(p) p.BATIMENT)
                           Where e.ID_PT_AFFICH > 0

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

                Case "Emplacement"
                    entities = entities.OrderBy(Function(e) e.EMPLACEMENT)
                Case "Emplacement_desc"
                    entities = entities.OrderByDescending(Function(e) e.EMPLACEMENT)

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


        ' GET: /Affichage/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pt_affichage As PT_AFFICHAGE = db.PT_AFFICHAGE.Find(id)
            If IsNothing(pt_affichage) Then
                Return HttpNotFound()
            End If
            Return View(pt_affichage)
        End Function

        ' GET: /Affichage/Create
        Function Create() As ActionResult
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE")
            Return View()
        End Function

        ' POST: /Affichage/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ID_PT_AFFICH,ID_BAT,LIBELLE,EMPLACEMENT,DETAILS")> ByVal pt_affichage As PT_AFFICHAGE) As ActionResult
            If ModelState.IsValid Then
                pt_affichage.DATE_CREATION = Now
                db.PT_AFFICHAGE.Add(pt_affichage)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", pt_affichage.ID_BAT)
            Return View(pt_affichage)
        End Function

        ' GET: /Affichage/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pt_affichage As PT_AFFICHAGE = db.PT_AFFICHAGE.Find(id)
            If IsNothing(pt_affichage) Then
                Return HttpNotFound()
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", pt_affichage.ID_BAT)
            Return View(pt_affichage)
        End Function

        ' POST: /Affichage/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include := "ID_PT_AFFICH,ID_BAT,LIBELLE,EMPLACEMENT,DETAILS,DATE_CREATION")> ByVal pt_affichage As PT_AFFICHAGE) As ActionResult
            If ModelState.IsValid Then
                db.Entry(pt_affichage).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", pt_affichage.ID_BAT)
            Return View(pt_affichage)
        End Function

        ' GET: /Affichage/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim pt_affichage As PT_AFFICHAGE = db.PT_AFFICHAGE.Find(id)
            If IsNothing(pt_affichage) Then
                Return HttpNotFound()
            End If
            Return View(pt_affichage)
        End Function

        ' POST: /Affichage/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim pt_affichage As PT_AFFICHAGE = db.PT_AFFICHAGE.Find(id)
            db.PT_AFFICHAGE.Remove(pt_affichage)
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
            ViewBag.activeMenu = "param-5"
        End Sub
    End Class
End Namespace
