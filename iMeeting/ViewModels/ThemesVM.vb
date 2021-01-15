
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class ThemesVM

	<Display(Name:="theme_Id", ResourceType:=GetType(Resource))> _			
	Public Property Id As Long

	<Display(Name:="theme_Libelle", ResourceType:=GetType(Resource))> _			
	<Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="theme_LibelleRequired")> _			
	<StringLength(250, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="theme_LibelleLong")> _			
	Public Property Libelle As String

	<Display(Name:="theme_DateCreation", ResourceType:=GetType(Resource))> _			
	<DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="theme_DateCreationDataType")> _			
	Public Property DateCreation As DateTime

	
	Public Sub New()
    End Sub

    Public Sub New(entity As Theme)
        With Me
            .Id = entity.Id
            .Libelle = entity.Libelle
            .DateCreation = entity.DateCreation
        End With
    End Sub
	
    Public Function getEntity() As Theme
        Dim entity As New Theme

        With entity
            .Id = Me.Id
            .Libelle = Me.Libelle
            .DateCreation = Me.DateCreation
        End With

        Return entity
    End Function
 End Class
    
