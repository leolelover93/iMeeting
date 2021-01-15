Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class ParticipantMetadata

    <Display(Name:="part_IdParticipant", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_IdParticipantRequired")> _
    <UIHint("Number")> _
    <Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_IdParticipantDataType")> _
       Public Property ID_PARTICIPANT As Long

    <Display(Name:="part_Nom", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_NomRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_NomLong")> _
       Public Property NOM As String

    <Display(Name:="part_Prenom", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_PrenomLong")> _
       Public Property PRENOM As String

    <Display(Name:="part_Fonction", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_FonctionLong")> _
       Public Property FONCTION As String

    <Display(Name:="part_Email", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_EmailLong")> _
    <DataType(DataType.EmailAddress, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_EmailDataType")> _
    Public Property EMAIL As String

    <Display(Name:="part_Telephone", ResourceType:=GetType(Resource))> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="part_TelephoneLong")> _
       Public Property TELEPHONE As String

End Class

Public Class ConvocationMetadata

    <Display(Name:="conv_IdReservation", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_IdReservationRequired")> _
    <UIHint("Number")> _
    <Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_IdReservationDataType")> _
       Public Property ID_RESERVATION As Long

    <Display(Name:="conv_Reference", ResourceType:=GetType(Resource))> _
    <StringLength(1000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_ReferenceLong")> _
    <DataType(DataType.Html)> _
    Public Property SMS As String

    <Display(Name:="conv_Objet", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_ObjetRequired")> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_ObjetLong")> _
    <DataType(DataType.Html)> _
       Public Property OBJET As String

    <DataType(DataType.Html)> _
    <Display(Name:="conv_Content", ResourceType:=GetType(Resource))> _
    Public Property CONTENT As String

    <Display(Name:="conv_DateCreation", ResourceType:=GetType(Resource))> _
                <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_DateCreationRequired")> _
                <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="conv_DateCreationDataType")> _
                Public Property DATE_CREATION As DateTime
End Class

Public Class PtAffichageMetadata

    <Display(Name:="pt_a_IdBat", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_IdBatRequired")> _
    <UIHint("Number")> _
    <Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_IdBatDataType")> _
       Public Property ID_BAT As Long

    <Display(Name:="pt_a_Libelle", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_LibelleRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_LibelleLong")> _
       Public Property LIBELLE As String

    <Display(Name:="pt_a_Emplacement", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_EmplacementRequired")> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_EmplacementLong")> _
    <DataType(DataType.Html)> _
       Public Property EMPLACEMENT As String

    <Display(Name:="pt_a_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="pt_a_DetailsLong")> _
    <DataType(DataType.Html)> _
       Public Property DETAILS As String

End Class

Public Class ServiceMetadata

    <Display(Name:="serv_IdBat", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="serv_IdBatRequired")> _
    <UIHint("Number")> _
    <Range(0, Long.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="serv_IdBatDataType")> _
       Public Property ID_BAT As Long

    <Display(Name:="serv_Libelle", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="serv_LibelleRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="serv_LibelleLong")> _
       Public Property LIBELLE As String

    <Display(Name:="serv_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="serv_DetailsLong")> _
    <DataType(DataType.Html)> _
       Public Property DETAILS As String

End Class

Public Class LieuxMetadata

    <Display(Name:="lieu_IdBat", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_IdBatRequired")> _
       Public Property ID_BAT As Long

    <Display(Name:="lieu_Libelle", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_LibelleRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_LibelleLong")> _
       Public Property LIBELLE As String

    <Display(Name:="lieu_Emplacement", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_EmplacementLong")> _
    <DataType(DataType.Html)> _
       Public Property EMPLACEMENT As String

    <Display(Name:="lieu_Capacite", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_CapaciteRequired")> _
    <UIHint("Number")> _
    <Range(1, Long.MaxValue)> _
    Public Property CAPACITE As Long

    <Display(Name:="lieu_TypeLieu", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_TypeLieuRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue)> _
    Public Property TYPE_LIEU As Integer

    <Display(Name:="lieu_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="lieu_DetailsLong")> _
    <DataType(DataType.Html)> _
       Public Property DETAILS As String

End Class

Public Class ReservationMetadata

    <Display(Name:="rese_IdLieu", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_IdLieuRequired")> _
    Public Property ID_LIEU As Long

    <Display(Name:="rese_Userid", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_UseridRequired")> _
    Public Property UserId As String

    <Display(Name:="rese_Libelle", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_LibelleRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_LibelleLong")> _
    Public Property LIBELLE As String

    <Display(Name:="rese_Motif", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_MotifRequired")> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_MotifLong")> _
    <DataType(DataType.Html)> _
    Public Property MOTIF As String

    <Display(Name:="rese_DateDebut", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateDebutRequired")> _
    <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateDebutDataType")> _
    Public Property DATE_DEBUT As DateTime

    <Display(Name:="rese_DateFin", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateFinRequired")> _
    <DataType(DataType.Date, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DateFinDataType")> _
    Public Property DATE_FIN As DateTime

    <Display(Name:="rese_HeureDebut", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureDebutRequired")> _
    <DataType(DataType.Time, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureDebutDataType")> _
    Public Property HEURE_DEBUT As DateTime

    <Display(Name:="rese_HeureFin", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureFinRequired")> _
    <DataType(DataType.Time, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_HeureFinDataType")> _
    Public Property HEURE_FIN As DateTime

    <Display(Name:="rese_NbrePers", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_NbrePersRequired")> _
    <UIHint("Number")> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_NbrePersDataType")> _
    Public Property NBRE_PERS As Integer

    <Display(Name:="rese_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DetailsLong")> _
    <DataType(DataType.Html)> _
    Public Property DETAILS As String

End Class

Public Class BatimentMetadata
    <Display(Name:="bati_Libelle", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_LibelleRequired")> _
    <StringLength(150, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_LibelleLong")> _
    Public Property LIBELLE As String

    <UIHint("Position")> _
    <Display(Name:="bati_Position", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_PositionRequired")> _
    <StringLength(2, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_PositionLong")> _
    Public Property POSITION As String

    <UIHint("Number")> _
    <Display(Name:="bati_NbrePiece", ResourceType:=GetType(Resource))> _
    <Range(0, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_NbrePieceDataType")> _
    Public Property NBRE_PIECE As Integer?

    <Display(Name:="bati_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="bati_DetailsLong")> _
    <DataType(DataType.Html)> _
    Public Property DETAILS As String

End Class
