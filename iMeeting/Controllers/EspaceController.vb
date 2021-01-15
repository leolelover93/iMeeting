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
    
    Public Class EspaceController
        Inherits BaseController

        Private db As New ApplicationDbContext

        ' GET: /LIEUX/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder

            ViewBag.IdBatSortParm = If(String.IsNullOrEmpty(sortOrder), "IdBat_desc", "")
            ViewBag.LibelleSortParm = If(sortOrder = "Libelle", "Libelle_desc", "Libelle")
            ViewBag.EmplacementSortParm = If(sortOrder = "Emplacement", "Emplacement_desc", "Emplacement")
            ViewBag.CapaciteSortParm = If(sortOrder = "Capacite", "Capacite_desc", "Capacite")
            ViewBag.TypeLieuSortParm = If(sortOrder = "TypeLieu", "TypeLieu_desc", "TypeLieu")
            ViewBag.DetailsSortParm = If(sortOrder = "Details", "Details_desc", "Details")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.LIEUX.Include(Function(l) l.BATIMENT)
                           Where e.TYPE_LIEU = TypeLieuEnum.EspaceCeremonie
                           Select e

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.LIBELLE.ToUpper.Contains(searchString.ToUpper) Or
                                              e.EMPLACEMENT.ToUpper.Contains(searchString.ToUpper) Or
                                              e.DETAILS.ToUpper.Contains(searchString.ToUpper))
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

                Case "Capacite"
                    entities = entities.OrderBy(Function(e) e.CAPACITE)
                Case "Capacite_desc"
                    entities = entities.OrderByDescending(Function(e) e.CAPACITE)

                Case "TypeLieu"
                    entities = entities.OrderBy(Function(e) e.TYPE_LIEU)
                Case "TypeLieu_desc"
                    entities = entities.OrderByDescending(Function(e) e.TYPE_LIEU)

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

        ' GET: /Lieu/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lieux As LIEUX = db.LIEUX.Find(id)
            If IsNothing(lieux) Then
                Return HttpNotFound()
            End If
            Return View(lieux)
        End Function

        ' GET: /Lieu/Create
        Function Create() As ActionResult
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE")
            Return View()
        End Function

        ' POST: /Lieu/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ID_LIEU,ID_BAT,LIBELLE,EMPLACEMENT,CAPACITE,DETAILS")> ByVal lieux As LIEUX) As ActionResult
            If ModelState.IsValid Then
                lieux.DATE_CREATION = Now
                lieux.TYPE_LIEU = TypeLieuEnum.EspaceCeremonie
                db.LIEUX.Add(lieux)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", lieux.ID_BAT)
            Return View(lieux)
        End Function

        ' GET: /Lieu/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lieux As LIEUX = db.LIEUX.Find(id)
            If IsNothing(lieux) Then
                Return HttpNotFound()
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", lieux.ID_BAT)
            Return View(lieux)
        End Function

        ' POST: /Lieu/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID_LIEU,ID_BAT,LIBELLE,EMPLACEMENT,CAPACITE,TYPE_LIEU,DETAILS,DATE_CREATION,Etat")> ByVal lieux As LIEUX) As ActionResult
            If ModelState.IsValid Then
                If Request("btnChangeStatus") IsNot Nothing Then
                    If lieux.Etat = 0 Then
                        lieux.Etat = -1
                    Else
                        lieux.Etat = 0
                    End If
                End If
                db.Entry(lieux).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.BATIMENT = New SelectList(db.BATIMENT, "ID_BAT", "LIBELLE", lieux.ID_BAT)
            Return View(lieux)
        End Function

        ' GET: /Lieu/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim lieux As LIEUX = db.LIEUX.Find(id)
            If IsNothing(lieux) Then
                Return HttpNotFound()
            End If
            Return View(lieux)
        End Function

        ' POST: /Lieu/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim lieux As LIEUX = db.LIEUX.Find(id)
            db.LIEUX.Remove(lieux)
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
            ViewBag.activeMenu = "param-3"
        End Sub
    End Class
End Namespace


