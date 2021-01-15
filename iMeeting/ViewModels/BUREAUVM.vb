
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class BUREAUVM

	<Display(Name:="bur_IDBUREAU", ResourceType:=GetType(Resource))> _			
	<UIHint("Number")> _
	<Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_IDBUREAUDataType")> _			
	Public Property IDBUREAU As Long

	<Display(Name:="bur_IDSERVICE", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_IDSERVICERequired")> _			
	Public Property IDSERVICE As Long
	Public Overridable Property IDs As ICollection(Of SERVICE)

	<Display(Name:="bur_LIBELLE", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_LIBELLERequired")> _			
	<StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_LIBELLELong")> _			
	Public Property LIBELLE As String

	<Display(Name:="bur_EMPLACEMENT", ResourceType:=GetType(Resource))> _			
	<StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_EMPLACEMENTLong")> _			
	<DataType(DataType.Html)> _		
	Public Property EMPLACEMENT As String

	<Display(Name:="bur_DETAILS", ResourceType:=GetType(Resource))> _			
	<StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_DETAILSLong")> _			
	<DataType(DataType.Html)> _		
	Public Property DETAILS As String

	<Display(Name:="bur_DATECREATION", ResourceType:=GetType(Resource))> _			
	<DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bur_DATECREATIONDataType")> _			
	Public Property DATECREATION As DateTime

	
	Public Sub New()
    End Sub

	Public Sub New(entity as BUREAU)
		With Me
            .IDBUREAU = entity.ID_BUREAU
            .IDSERVICE = entity.ID_SERVICE
			.LIBELLE = entity.LIBELLE
			.EMPLACEMENT = entity.EMPLACEMENT
			.DETAILS = entity.DETAILS
            .DATECREATION = entity.DATE_CREATION
		End With
	End Sub
	
	Public Function getEntity() as BUREAU
		Dim entity as New BUREAU
		
		With entity
            .ID_BUREAU = Me.IDBUREAU
            .ID_SERVICE = Me.IDSERVICE
			.LIBELLE = Me.LIBELLE
			.EMPLACEMENT = Me.EMPLACEMENT
			.DETAILS = Me.DETAILS
            .DATE_CREATION = Me.DATECREATION
		End With
		
		Return entity
	End Function
 End Class
    
