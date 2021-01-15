
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class TexteDefilantsVM

	<Display(Name:="txtd_Id", ResourceType:=GetType(Resource))> _			
	<Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_IdDataType")> _			
	Public Property Id As Long

    <Display(Name:="txtd_Message", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_MessageRequired")> _
    <UIHint("Html")> _
    Public Property Message As String

	<Display(Name:="txtd_UserId", ResourceType:=GetType(Resource))> _			
	<StringLength(128, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_UserIdLong")> _			
	Public Property UserId As String

	<Display(Name:="txtd_DateCreation", ResourceType:=GetType(Resource))> _			
	<DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_DateCreationDataType")> _			
	Public Property DateCreation As DateTime

	<Display(Name:="txtd_EstPublie", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_EstPublieRequired")> _			
	Public Property EstPublie As Boolean

	<Display(Name:="txtd_DateDebut", ResourceType:=GetType(Resource))> _			
	<DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_DateDebutDataType")> _			
	Public Property DateDebut As DateTime?

	<Display(Name:="txtd_DateFin", ResourceType:=GetType(Resource))> _			
	<DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_DateFinDataType")> _			
	Public Property DateFin As DateTime?

	<Display(Name:="txtd_PointAffichageId", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="txtd_PointAffichageIdRequired")> _			
	Public Property PointAffichageId As Long
	Public Overridable Property PointAffichageIds As ICollection(Of SelectListItem)
    Public Property PointAffichage As PT_AFFICHAGE


    Public Sub New()
    End Sub

    Public Sub New(entity As TexteDefilant)
        With Me
            .Id = entity.Id
            .Message = entity.Message
            .UserId = entity.UserId
            .DateCreation = entity.DateCreation
            .EstPublie = entity.EstPublie
            .DateDebut = entity.DateDebut
            .DateFin = entity.DateFin
            .PointAffichageId = entity.PointAffichageId
            .PointAffichage = entity.PointAffichage
        End With
    End Sub
	
    Public Function getEntity() As TexteDefilant
        Dim entity As New TexteDefilant

        With entity
            .Id = Me.Id
            .Message = Me.Message
            .UserId = Me.UserId
            .DateCreation = Me.DateCreation
            .EstPublie = Me.EstPublie
            .DateDebut = Me.DateDebut
            .DateFin = Me.DateFin
            .PointAffichageId = Me.PointAffichageId
        End With

        Return entity
    End Function
 End Class
    
