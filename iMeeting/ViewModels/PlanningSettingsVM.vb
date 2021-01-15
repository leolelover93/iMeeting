
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class PlanningSettingsVM

	<Display(Name:="ppla_Id", ResourceType:=GetType(Resource))> _			
	Public Property Id As Long

	<Display(Name:="ppla_PointAffichageId", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_PointAffichageIdRequired")> _			
	Public Property PointAffichageId As Long
	Public Overridable Property PointAffichageIds As ICollection(Of SelectListItem)
    Public Property PointAffichage As PT_AFFICHAGE

	<Display(Name:="ppla_LieuId", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_LieuIdRequired")> _			
	Public Property LieuId As Long
	Public Overridable Property LieuIds As ICollection(Of SelectListItem)
	Public Property Lieu As LIEUX

    <Display(Name:="ppla_Afficher", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_AfficherRequired")> _
    Public Property Afficher As Boolean = True

    <Display(Name:="ppla_ViewType", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_ViewTypeRequired")> _
    <UIHint("EnumLineaire")> _
    Public Property ViewType As EnumViewType = EnumViewType.Hebdomadaire

    <Display(Name:="ppla_MaxCharsPage", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_MaxCharsPageRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_MaxCharsPageDataType")> _
    Public Property MaxCharsPage As Integer = 400

    <Display(Name:="ppla_MaxPageElements", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_MaxPageElementsRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_MaxPageElementsDataType")> _
    Public Property MaxPageElements As Integer = 4

	<Display(Name:="ppla_LibelleVue", ResourceType:=GetType(Resource))> _			
	<StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_LibelleVueLong")> _			
	Public Property LibelleVue As String

    <Display(Name:="ppla_Animation", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_AnimationLong")> _
    Public Property Animation As String = ""

    <Display(Name:="ppla_AnimationTime", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_AnimationTimeRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_AnimationTimeDataType")> _
    Public Property AnimationTime As Integer = 2000

    <Display(Name:="ppla_ViewShowTime", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_ViewShowTimeRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_ViewShowTimeDataType")> _
    Public Property ViewShowTime As Integer = 30000

    <Display(Name:="ppla_LibellePeriode", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_LibellePeriodeLong")> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="app_FieldRequired")> _
    Public Property LibellePeriode As String

	<Display(Name:="ppla_BackgoundImage", ResourceType:=GetType(Resource))> _			
	<StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_BackgoundImageLong")> _			
	Public Property BackgoundImage As String

	<Display(Name:="ppla_DivClassName", ResourceType:=GetType(Resource))> _			
	<StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_DivClassNameLong")> _			
	Public Property DivClassName As String

	<Display(Name:="ppla_UserId", ResourceType:=GetType(Resource))> _			
	<StringLength(128, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_UserIdLong")> _			
	Public Property UserId As String

    <Display(Name:="ppla_DateCreation", ResourceType:=GetType(Resource))> _
    <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="ppla_DateCreationDataType")> _
    Public Property DateCreation As DateTime = Now

	
	Public Sub New()
    End Sub

	Public Sub New(entity as PlanningSettings)
		With Me
			.Id = entity.Id
			.PointAffichageId = entity.PointAffichageId
			.PointAffichage = entity.PointAffichage
			.LieuId = entity.LieuId
			.Lieu = entity.Lieu
			.Afficher = entity.Afficher
			.ViewType = entity.ViewType
			.MaxCharsPage = entity.MaxCharsPage
			.MaxPageElements = entity.MaxPageElements
			.LibelleVue = entity.LibelleVue
			.Animation = entity.Animation
			.AnimationTime = entity.AnimationTime
			.ViewShowTime = entity.ViewShowTime
			.LibellePeriode = entity.LibellePeriode
			.BackgoundImage = entity.BackgoundImage
			.DivClassName = entity.DivClassName
			.UserId = entity.UserId
			.DateCreation = entity.DateCreation
		End With
	End Sub
	
	Public Function getEntity() as PlanningSettings
		Dim entity as New PlanningSettings
		
		With entity
			.Id = Me.Id
			.PointAffichageId = Me.PointAffichageId
			.LieuId = Me.LieuId
			.Afficher = Me.Afficher
			.ViewType = Me.ViewType
			.MaxCharsPage = Me.MaxCharsPage
			.MaxPageElements = Me.MaxPageElements
			.LibelleVue = Me.LibelleVue
			.Animation = Me.Animation
			.AnimationTime = Me.AnimationTime
			.ViewShowTime = Me.ViewShowTime
			.LibellePeriode = Me.LibellePeriode
			.BackgoundImage = Me.BackgoundImage
			.DivClassName = Me.DivClassName
			.UserId = Me.UserId
			.DateCreation = Me.DateCreation
		End With
		
		Return entity
	End Function
 End Class
    
