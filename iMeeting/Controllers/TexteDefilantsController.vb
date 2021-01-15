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

Namespace iSaisine
    Public Class TexteDefilantsController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Public Sub New()
            ViewBag.activeMenu = "play-1"
        End Sub
		
		' GET: /TexteDefilants/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder 	
			ViewBag.MessageSortParm = If(String.IsNullOrEmpty(sortOrder), "Message_desc", "")
			ViewBag.EstPublieSortParm = If(sortOrder = "EstPublie", "EstPublie_desc", "EstPublie")
			ViewBag.DateDebutSortParm = If(sortOrder = "DateDebut", "DateDebut_desc", "DateDebut")
			ViewBag.DateFinSortParm = If(sortOrder = "DateFin", "DateFin_desc", "DateFin")
			ViewBag.PointAffichageIdSortParm = If(sortOrder = "PointAffichageId", "PointAffichageId_desc", "PointAffichageId")

	
            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.TexteDefilants()
	

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Message.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder 
				Case "Message"
                    entities = entities.OrderBy(Function(e) e.Message)
                Case "Message_desc"
                    entities = entities.OrderByDescending(Function(e) e.Message)
         	
				Case "EstPublie"
                    entities = entities.OrderBy(Function(e) e.EstPublie)
                Case "EstPublie_desc"
                    entities = entities.OrderByDescending(Function(e) e.EstPublie)
         	
				Case "DateDebut"
                    entities = entities.OrderBy(Function(e) e.DateDebut)
                Case "DateDebut_desc"
                    entities = entities.OrderByDescending(Function(e) e.DateDebut)
         	
				Case "DateFin"
                    entities = entities.OrderBy(Function(e) e.DateFin)
                Case "DateFin_desc"
                    entities = entities.OrderByDescending(Function(e) e.DateFin)
         	
				Case "PointAffichageId"
                    entities = entities.OrderBy(Function(e) e.PointAffichage.LIBELLE)
                Case "PointAffichageId_desc"
                    entities = entities.OrderByDescending(Function(e) e.PointAffichage.LIBELLE)
         	
                Case Else
                    entities = entities.OrderBy(Function(e) e.Id)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

		' GET: /TexteDefilants/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As TexteDefilant = db.TexteDefilants.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(New TexteDefilantsVM(entity))
        End Function

	
		' Remplir les combos pour réaffichage en cas d'erreur			
		Sub PopupCombo(entityVM As TexteDefilantsVM)
            Dim AFFICHAGEs = db.PT_AFFICHAGE.ToList
            Dim lstAFFICHAGEs As New List(Of SelectListItem)
            For Each e In AFFICHAGEs
                lstAFFICHAGEs.Add(New SelectListItem With {.Value = e.ID_PT_AFFICH, .Text = e.LIBELLE})
            Next 
			entityVM.PointAffichageIds = lstAFFICHAGEs

        End Sub
		
			
        ' GET: /TexteDefilants/Create
        Function Create() As ActionResult
			Dim entityVM As New TexteDefilantsVM

            ' Remplir les combos						
			PopupCombo(entityVM)
				
            Return View(entityVM)						
        End Function
   
		' POST: /TexteDefilants/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal entityVM As TexteDefilantsVM) As ActionResult
	
			' Remplir les combos pour réaffichage en cas d'erreur						
			PopupCombo(entityVM)
			
            If ModelState.IsValid Then
								entityVM.UserId = User.Identity.GetUserId()
				entityVM.DateCreation = Now 

                
				db.TexteDefilants.Add(entityVM.getEntity())
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
   
		' GET: /TexteDefilants/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As TexteDefilant = db.TexteDefilants.Find(id)
			If IsNothing(entity) Then
                Return HttpNotFound()
            End If
			Dim entityVM As New TexteDefilantsVM(entity)
            
			' Remplir les combos						
			PopupCombo(entityVM)

            Return View(entityVM)
        End Function
		
		' POST: /TexteDefilants/Edit
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal entityVM As TexteDefilantsVM) As ActionResult
	
			' Remplir les combos pour réaffichage en cas d'erreur						
			PopupCombo(entityVM)

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
            End If 
            Return View(entityVM)
        End Function
		
		' GET: /TexteDefilants/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As TexteDefilant = db.TexteDefilants.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(New TexteDefilantsVM(entity))
        End Function

        ' POST: /TexteDefilants/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim entity As TexteDefilant = db.TexteDefilants.Find(id)
            
							  db.TexteDefilants.Remove(entity)
				
			Try
				db.SaveChanges()
				Return RedirectToAction("Index")
			Catch ex As Exception
                Util.getError(ex, ModelState)
			End Try
			
            Return View(New TexteDefilantsVM(entity))
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

