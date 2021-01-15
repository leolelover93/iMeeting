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
    Public Class BUREAUController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Public Sub New()
            ViewBag.activeMenu = "param-8"
        End Sub
		
		' GET: /BUREAU/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder 	
			ViewBag.IDSERVICESortParm = If(String.IsNullOrEmpty(sortOrder), "IDSERVICE_desc", "")
			ViewBag.LIBELLESortParm = If(sortOrder = "LIBELLE", "LIBELLE_desc", "LIBELLE")
			ViewBag.EMPLACEMENTSortParm = If(sortOrder = "EMPLACEMENT", "EMPLACEMENT_desc", "EMPLACEMENT")
			ViewBag.DETAILSSortParm = If(sortOrder = "DETAILS", "DETAILS_desc", "DETAILS")

	
            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.BUREAU.OfType(Of BUREAU)()
	

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.LIBELLE.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder 
				Case "LIBELLE"
                    entities = entities.OrderBy(Function(e) e.LIBELLE)
                Case "LIBELLE_desc"
                    entities = entities.OrderByDescending(Function(e) e.LIBELLE)
         	
				Case "EMPLACEMENT"
                    entities = entities.OrderBy(Function(e) e.EMPLACEMENT)
                Case "EMPLACEMENT_desc"
                    entities = entities.OrderByDescending(Function(e) e.EMPLACEMENT)
         	
				Case "DETAILS"
                    entities = entities.OrderBy(Function(e) e.DETAILS)
                Case "DETAILS_desc"
                    entities = entities.OrderByDescending(Function(e) e.DETAILS)
         	
                Case Else
                    entities = entities.OrderBy(Function(e) e.DETAILS)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

		' GET: /BUREAU/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As BUREAU = db.BUREAU.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(entity)
        End Function

        ' GET: /BUREAU/Create
        Function Create() As ActionResult
			Dim entityVM As New BUREAUVM

            ' Remplir les combos						
			 entityVM.IDs = db.SERVICE.OfType(Of SERVICE)().ToList 

				
            Return View(entityVM)						
        End Function
   
		' POST: /BUREAU/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include := "IDSERVICE, LIBELLE, EMPLACEMENT, DETAILS")> ByVal entityVM As BUREAUVM) As ActionResult
	
			' Remplir les combos pour réaffichage en cas d'erreur						
			 entityVM.IDs = db.SERVICE.OfType(Of SERVICE)().ToList 

            If ModelState.IsValid Then
								entityVM.DateCreation = Now 

                
				db.BUREAU.Add(entityVM.getEntity())
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
   
		' GET: /BUREAU/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As BUREAU = db.BUREAU.Find(id)
			If IsNothing(entity) Then
                Return HttpNotFound()
            End If
			Dim entityVM As New BUREAUVM(entity)
            
			' Remplir les combos						
			 entityVM.IDs = db.SERVICE.OfType(Of SERVICE)().ToList 

            Return View(entityVM)
        End Function
		
		' POST: /BUREAU/Edit
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include := "IDBUREAU, IDSERVICE, LIBELLE, EMPLACEMENT, DETAILS, DATECREATION")> ByVal entityVM As BUREAUVM) As ActionResult
	
			' Remplir les combos pour réaffichage en cas d'erreur						
			 entityVM.IDs = db.SERVICE.OfType(Of SERVICE)().ToList 

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
		
		' GET: /BUREAU/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As BUREAU = db.BUREAU.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(entity)
        End Function

        ' POST: /BUREAU/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim entity As BUREAU = db.BUREAU.Find(id)
            
            db.BUREAU.Remove(entity)
				
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

