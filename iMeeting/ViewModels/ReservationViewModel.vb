Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources

Public Class ReservationViewModel

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property ID_RESERVATION As Long = 0

    <Display(Name:="rese_IdLieu", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_IdLieuRequired")> _
    Public Property ID_LIEU As Long = 0

    <Display(Name:="rese_themeId", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_themeIdRequired")> _
    Public Property ThemeId As Long

    <Display(Name:="rese_Userid", ResourceType:=GetType(Resource))> _
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_UseridRequired")> _
    Public Property UserId As System.Guid

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
    <UIHint("Number")> _
    <Range(1, Integer.MaxValue, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_NbrePersDataType")> _
    Public Property NBRE_PERS As Integer?

    <Display(Name:="rese_Details", ResourceType:=GetType(Resource))> _
    <StringLength(4000, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="rese_DetailsLong")> _
    <DataType(DataType.Html)> _
    Public Property DETAILS As String

    <UIHint("EtatGeneral")> _
    Public Property Etat As EnumReservation = EnumReservation.Initial
    <UIHint("EtatCourant")> _
    Public Property CurrentState As EnumCurrentState = EnumCurrentState.Initial

    Public Property AutoShowDialog As Boolean = False
    ' Permet de savoir s'il faut gérer les états automatiquement,
    ' notammment pour les réunions déjà validées
    Public Property ManualState As Boolean = False

    Public Property Participants As List(Of PARTICIPANT)

    Public Function getReservation(db As ApplicationDbContext) As RESERVATION
        If Me.ID_RESERVATION <= 0 Then
            Return getReservationNew()
        Else
            Return getReservationMod(db)
        End If
    End Function

    Sub LoadEntity(reserv As RESERVATION)
        Me.AutoShowDialog = True

        Me.ID_RESERVATION = reserv.ID_RESERVATION
        Me.ID_LIEU = reserv.ID_LIEU
        Me.ThemeId = reserv.ThemeId
        Me.PresidentSeance = reserv.PresidentSeance
        Me.Etat = reserv.ETAT
        Me.CurrentState = reserv.CurrentState
        Me.TelPresiSce = reserv.TelPresiSce
        Me.Objet = reserv.Objet
        Me.DATE_DEBUT = reserv.DATE_DEBUT
        Me.DATE_FIN = reserv.DATE_FIN
        Me.HEURE_DEBUT = reserv.HEURE_DEBUT
        Me.HEURE_FIN = reserv.HEURE_FIN
        Me.NBRE_PERS = reserv.NBRE_PERS
        Me.DETAILS = reserv.DETAILS

        Me.Participants = New List(Of PARTICIPANT)

        For Each parti In reserv.PARTICIPANT
            Me.Participants.Add(parti)
        Next
    End Sub

    Private Function getReservationNew() As RESERVATION
        Dim reserv As New RESERVATION
        reserv.ID_RESERVATION = Me.ID_RESERVATION
        reserv.ID_LIEU = Me.ID_LIEU
        reserv.ThemeId = Me.ThemeId
        reserv.PresidentSeance = Me.PresidentSeance
        reserv.TelPresiSce = Me.TelPresiSce
        reserv.Objet = Regex.Replace(Me.Objet, "\s+", " ")
        reserv.DATE_DEBUT = Me.DATE_DEBUT.Date.Add(Me.HEURE_DEBUT.TimeOfDay)
        reserv.DATE_FIN = Me.DATE_DEBUT.Date.Add(Me.HEURE_FIN.TimeOfDay)
        reserv.HEURE_DEBUT = reserv.DATE_DEBUT
        reserv.HEURE_FIN = reserv.DATE_FIN
        reserv.NBRE_PERS = Me.NBRE_PERS
        reserv.DETAILS = Me.DETAILS
        reserv.ETAT = EnumReservation.Initial ' Etat initial
        reserv.CurrentState = EnumReservation.Initial
        reserv.DATE_CREATION = Now

        If Not Me.Participants Is Nothing Then ' Eléments du formulaire
            For Each parti In Me.Participants   ' Gérer le cas des participants déjà existants dans la BD
                reserv.PARTICIPANT.Add(parti)
            Next
        End If

        Return reserv
    End Function

    Private Function getReservationMod(db As ApplicationDbContext) As RESERVATION
        Dim reserv = db.RESERVATION.Find(Me.ID_RESERVATION)
        If Not reserv Is Nothing Then
            reserv.ID_LIEU = Me.ID_LIEU
            reserv.ThemeId = Me.ThemeId
            reserv.PresidentSeance = Me.PresidentSeance
            reserv.TelPresiSce = Me.TelPresiSce
            reserv.Objet = Regex.Replace(Me.Objet, "\s+", " ")
            reserv.DATE_DEBUT = Me.DATE_DEBUT.Date.Add(Me.HEURE_DEBUT.TimeOfDay)
            reserv.DATE_FIN = Me.DATE_DEBUT.Date.Add(Me.HEURE_FIN.TimeOfDay)
            reserv.HEURE_DEBUT = reserv.DATE_DEBUT
            reserv.HEURE_FIN = reserv.DATE_FIN
            reserv.NBRE_PERS = Me.NBRE_PERS
            reserv.DETAILS = Me.DETAILS
            reserv.ETAT = Me.Etat
            reserv.CurrentState = Me.CurrentState

            '--------------------------------------------------------------------------------------------------
            ' Supprimer les éléments inutiles
            Dim al As New List(Of PARTICIPANT)
            For Each parti In reserv.PARTICIPANT ' les éléments dans la bd
                Dim item As PARTICIPANT = Nothing
                If Me.Participants IsNot Nothing Then ' les éléments du formulaire
                    item = Me.Participants.Where(Function(p) p.ID_PARTICIPANT = parti.ID_PARTICIPANT).FirstOrDefault()
                End If

                If IsNothing(item) Then
                    al.Add(parti)
                End If
            Next

            For Each parti In al
                db.Entry(parti).State = Entity.EntityState.Deleted
            Next
            ' Fin suppression
            '--------------------------------------------------------------------------------------------------

            If Me.Participants IsNot Nothing Then ' éléments du formulaire
                For Each parti In Me.Participants
                    Dim item = reserv.PARTICIPANT.Where(Function(p) p.ID_PARTICIPANT = parti.ID_PARTICIPANT).FirstOrDefault
                    If IsNothing(item) Then
                        ' Nouveau participant
                        reserv.PARTICIPANT.Add(parti)
                    Else
                        ' Ancien participant
                        With item
                            .NOM = parti.NOM
                            .PRENOM = parti.PRENOM
                            .EMAIL = parti.EMAIL
                            .FONCTION = parti.FONCTION
                            .TELEPHONE = parti.TELEPHONE
                        End With

                        'db.Entry(item).State = Entity.EntityState.Modified
                    End If
                Next
            End If
        End If

        Return reserv
    End Function


End Class
