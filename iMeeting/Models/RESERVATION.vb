Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class RESERVATION
    Public Property ID_RESERVATION As Long

    <Display(Name:="rese_IdLieu", ResourceType:=GetType(Resource))> _
    Public Property ID_LIEU As Long

    <Display(Name:="rese_themeId", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_themeIdRequired")> _
    Public Property ThemeId As Long

    <Display(Name:="rese_Userid", ResourceType:=GetType(Resource))> _
    Public Property UserId As String

    <Display(Name:="rese_Libelle", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_LibelleLong")> _
    Public Property TelPresiSce As String

    <Display(Name:="rese_president", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_PresidentLong")> _
    Public Property PresidentSeance As String

    <Display(Name:="rese_Motif", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_MotifRequired")> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_MotifLong")> _
    <DataType(DataType.Html)> _
    Public Property Objet As String

    <Display(Name:="rese_DateDebut", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateDebutRequired")> _
    <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateDebutDataType")> _
    Public Property DATE_DEBUT As DateTime = Now

    <Display(Name:="rese_DateFin", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateFinRequired")> _
    <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateFinDataType")> _
    Public Property DATE_FIN As DateTime = Now

    <Display(Name:="rese_HeureDebut", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureDebutRequired")> _
    <DataType(DataType.Time, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureDebutDataType")> _
    Public Property HEURE_DEBUT As DateTime

    <Display(Name:="rese_HeureFin", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureFinRequired")> _
    <DataType(DataType.Time, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureFinDataType")> _
    Public Property HEURE_FIN As DateTime

    <Display(Name:="rese_NbrePers", ResourceType:=GetType(Resource))> _
    <UIHint("Number")> _
    <Range(1, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_NbrePersDataType")> _
    Public Property NBRE_PERS As Integer?

    <Display(Name:="rese_Details", ResourceType:=GetType(Resource))> _
    <DataType(DataType.Html)> _
    Public Property DETAILS As String

    <UIHint("EtatGeneral")> _
    Public Property ETAT As EnumReservation
    <UIHint("EtatCourant")> _
    Public Property CurrentState As EnumCurrentState = EnumCurrentState.Initial
    Public Property DATE_CREATION As Date

    <ForeignKey("ID_LIEU")>
    Public Overridable Property LIEUX As LIEUX

    Public Property NextReservationId As Long?
    <ForeignKey("NextReservationId")>
    Public Overridable Property NextReservation As RESERVATION

    Public Overridable Property Theme As Theme
    Public Overridable Property PARTICIPANT As ICollection(Of PARTICIPANT) = New HashSet(Of PARTICIPANT)
End Class
