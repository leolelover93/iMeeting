Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation
Imports PagedList

Namespace iMeeting
    Public Class ThemesController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Public Sub New()
            ViewBag.activeMenu = "param-7"
        End Sub

        ' GET: /Themes/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder
            ViewBag.LibelleSortParm = If(String.IsNullOrEmpty(sortOrder), "Libelle_desc", "")


            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.Theme.OfType(Of Theme)()


            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Libelle.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder
                Case "Libelle"
                    entities = entities.OrderBy(Function(e) e.Libelle)
                Case "Libelle_desc"
                    entities = entities.OrderByDescending(Function(e) e.Libelle)

                Case Else
                    entities = entities.OrderBy(Function(e) e.Libelle)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: /Themes/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As Theme = db.Theme.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(entity)
        End Function

        ' GET: /Themes/Create
        Function Create() As ActionResult

            Return View()

        End Function

        ' POST: /Themes/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Libelle")> ByVal entityVM As ThemesVM) As ActionResult

            If ModelState.IsValid Then
                entityVM.DateCreation = Now

                db.Theme.Add(entityVM.getEntity())
                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.getError(ex, ModelState)
                Catch ex As Exception
                    Util.getError(ex, ModelState)
                End Try
            End If
            Return View(entityVM)
        End Function

        ' GET: /Themes/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As Theme = db.Theme.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Dim entityVM As New ThemesVM(entity)

            Return View(entityVM)
        End Function

        ' POST: /Themes/Edit
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id, Libelle, DateCreation")> ByVal entityVM As ThemesVM) As ActionResult

            If ModelState.IsValid Then
                Dim entity = entityVM.getEntity()
                db.Entry(entity).State = EntityState.Modified

                Try
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As DbEntityValidationException
                    Util.getError(ex, ModelState)
                Catch ex As Exception
                    Util.getError(ex, ModelState)
                End Try
                Return RedirectToAction("Index")
            End If
            Return View(entityVM)
        End Function

        ' GET: /Themes/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As Theme = db.Theme.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(entity)
        End Function

        ' POST: /Themes/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim entity As Theme = db.Theme.Find(id)

            db.Theme.Remove(entity)

            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                Dim strError As String = ""
                For Each val_errors In ex.EntityValidationErrors
                    For Each val_error In val_errors.ValidationErrors
                        ModelState.AddModelError(val_error.PropertyName, val_error.ErrorMessage)
                        strError &= val_error.ErrorMessage & vbCrLf
                    Next
                Next
                ModelState.AddModelError("", strError)
            Catch ex As Exception
            End Try

            Return View(entity)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

