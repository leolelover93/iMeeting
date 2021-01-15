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
    Public Class PlanningSettingsController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Public Sub New()
            ViewBag.activeMenu = "play-2"
        End Sub
		
		' GET: /PlanningSettings/
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder 	
			ViewBag.PointAffichageIdSortParm = If(String.IsNullOrEmpty(sortOrder), "PointAffichageId_desc", "")
			ViewBag.LieuIdSortParm = If(sortOrder = "LieuId", "LieuId_desc", "LieuId")
			ViewBag.AfficherSortParm = If(sortOrder = "Afficher", "Afficher_desc", "Afficher")
			ViewBag.ViewTypeSortParm = If(sortOrder = "ViewType", "ViewType_desc", "ViewType")
			ViewBag.MaxCharsPageSortParm = If(sortOrder = "MaxCharsPage", "MaxCharsPage_desc", "MaxCharsPage")
			ViewBag.MaxPageElementsSortParm = If(sortOrder = "MaxPageElements", "MaxPageElements_desc", "MaxPageElements")
			ViewBag.LibelleVueSortParm = If(sortOrder = "LibelleVue", "LibelleVue_desc", "LibelleVue")
			ViewBag.AnimationSortParm = If(sortOrder = "Animation", "Animation_desc", "Animation")
			ViewBag.AnimationTimeSortParm = If(sortOrder = "AnimationTime", "AnimationTime_desc", "AnimationTime")
			ViewBag.ViewShowTimeSortParm = If(sortOrder = "ViewShowTime", "ViewShowTime_desc", "ViewShowTime")
			ViewBag.LibellePeriodeSortParm = If(sortOrder = "LibellePeriode", "LibellePeriode_desc", "LibellePeriode")
			ViewBag.BackgoundImageSortParm = If(sortOrder = "BackgoundImage", "BackgoundImage_desc", "BackgoundImage")
			ViewBag.DivClassNameSortParm = If(sortOrder = "DivClassName", "DivClassName_desc", "DivClassName")

	
            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.PlanningSettings()
	

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.DivClassName.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Select Case sortOrder 
				Case "PointAffichageId"
                    entities = entities.OrderBy(Function(e) e.PointAffichageId)
                Case "PointAffichageId_desc"
                    entities = entities.OrderByDescending(Function(e) e.PointAffichageId)
         	
				Case "LieuId"
                    entities = entities.OrderBy(Function(e) e.LieuId)
                Case "LieuId_desc"
                    entities = entities.OrderByDescending(Function(e) e.LieuId)
         	
				Case "Afficher"
                    entities = entities.OrderBy(Function(e) e.Afficher)
                Case "Afficher_desc"
                    entities = entities.OrderByDescending(Function(e) e.Afficher)
         	
				Case "ViewType"
                    entities = entities.OrderBy(Function(e) e.ViewType)
                Case "ViewType_desc"
                    entities = entities.OrderByDescending(Function(e) e.ViewType)
         	
				Case "MaxCharsPage"
                    entities = entities.OrderBy(Function(e) e.MaxCharsPage)
                Case "MaxCharsPage_desc"
                    entities = entities.OrderByDescending(Function(e) e.MaxCharsPage)
         	
				Case "MaxPageElements"
                    entities = entities.OrderBy(Function(e) e.MaxPageElements)
                Case "MaxPageElements_desc"
                    entities = entities.OrderByDescending(Function(e) e.MaxPageElements)
         	
				Case "LibelleVue"
                    entities = entities.OrderBy(Function(e) e.LibelleVue)
                Case "LibelleVue_desc"
                    entities = entities.OrderByDescending(Function(e) e.LibelleVue)
         	
				Case "Animation"
                    entities = entities.OrderBy(Function(e) e.Animation)
                Case "Animation_desc"
                    entities = entities.OrderByDescending(Function(e) e.Animation)
         	
				Case "AnimationTime"
                    entities = entities.OrderBy(Function(e) e.AnimationTime)
                Case "AnimationTime_desc"
                    entities = entities.OrderByDescending(Function(e) e.AnimationTime)
         	
				Case "ViewShowTime"
                    entities = entities.OrderBy(Function(e) e.ViewShowTime)
                Case "ViewShowTime_desc"
                    entities = entities.OrderByDescending(Function(e) e.ViewShowTime)
         	
				Case "LibellePeriode"
                    entities = entities.OrderBy(Function(e) e.LibellePeriode)
                Case "LibellePeriode_desc"
                    entities = entities.OrderByDescending(Function(e) e.LibellePeriode)
         	
				Case "BackgoundImage"
                    entities = entities.OrderBy(Function(e) e.BackgoundImage)
                Case "BackgoundImage_desc"
                    entities = entities.OrderByDescending(Function(e) e.BackgoundImage)
         	
				Case "DivClassName"
                    entities = entities.OrderBy(Function(e) e.DivClassName)
                Case "DivClassName_desc"
                    entities = entities.OrderByDescending(Function(e) e.DivClassName)
         	
                Case Else
                    entities = entities.OrderBy(Function(e) e.DivClassName)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

		' GET: /PlanningSettings/Details/5
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As PlanningSettings = db.PlanningSettings.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(New PlanningSettingsVM(entity))
        End Function

	
		' Remplir les combos pour réaffichage en cas d'erreur			
		Sub PopupCombo(entityVM As PlanningSettingsVM)
            Dim PointAffichages = db.PT_AFFICHAGE.ToList
            Dim lstPointAffichages As New List(Of SelectListItem)
            For Each e In PointAffichages
                lstPointAffichages.Add(New SelectListItem With {.Value = e.ID_PT_AFFICH, .Text = e.LIBELLE})
            Next 
			entityVM.PointAffichageIds = lstPointAffichages
			Dim Lieus = db.LIEUX.ToList
            Dim lstLieus As New List(Of SelectListItem)
            For Each e In Lieus
                lstLieus.Add(New SelectListItem With {.Value = e.ID_LIEU, .Text = e.LIBELLE})
            Next 
			entityVM.LieuIds = lstLieus

        End Sub
		
			
        ' GET: /PlanningSettings/Create
        Function Create() As ActionResult		
			Dim entityVM As New PlanningSettingsVM
	
            ' Remplir les combos						
			PopupCombo(entityVM)			
	
			Return View(entityVM)
        End Function
   
		' POST: /PlanningSettings/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal entityVM As PlanningSettingsVM) As ActionResult
	
			' Remplir les combos pour réaffichage en cas d'erreur						
			PopupCombo(entityVM)
			
            If ModelState.IsValid Then
								entityVM.UserId = User.Identity.GetUserId()
				entityVM.DateCreation = Now 

                
				db.PlanningSettings.Add(entityVM.getEntity())
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
   
		' GET: /PlanningSettings/Edit/5
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As PlanningSettings = db.PlanningSettings.Find(id)
			If IsNothing(entity) Then
                Return HttpNotFound()
            End If
			Dim entityVM As New PlanningSettingsVM(entity)
            
			' Remplir les combos						
			PopupCombo(entityVM)

            Return View(entityVM)
        End Function
		
		' POST: /PlanningSettings/Edit
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal entityVM As PlanningSettingsVM) As ActionResult
	
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
		
		' GET: /PlanningSettings/Delete/5
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entity As PlanningSettings = db.PlanningSettings.Find(id)
            If IsNothing(entity) Then
                Return HttpNotFound()
            End If
            Return View(New PlanningSettingsVM(entity))
        End Function

        ' POST: /PlanningSettings/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim entity As PlanningSettings = db.PlanningSettings.Find(id)
            
							  db.PlanningSettings.Remove(entity)
				
			Try
				db.SaveChanges()
				Return RedirectToAction("Index")
			Catch ex As Exception
                Util.getError(ex, ModelState)
			End Try
			
            Return View(New PlanningSettingsVM(entity))
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace

