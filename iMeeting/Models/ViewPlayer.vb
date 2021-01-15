
Public Enum EnumViewType
    Hebdomadaire
    Journalier
    Mensuel
End Enum

Public Class ViewPlayer
    Public Property Id As Integer
    Public Property IdLieu As Long
    Public Property LibelleLieu As String
    Public Property LibelleVue As String = ConfigurationManager.AppSettings("PlayerHebdoTitle") '"Planning hebdomadaire des réunions"
    Public Property BackgoundImage As String = "~/Ecran/bg.png"
    Public Property DivClassName As String = "StylePageDefault"
    Public Property Animation As String = ""
    Public Property AnimationTime As Integer = 2000
    Public Property ViewShowTime As Integer = 5000 ' 5 Sec mais modifiée par PlanningHebdoPlayer
    Public Property MaxPageElements As Integer = 4 ' 4 éléments par page par défaut
    Public Property LibellePeriode As String = ""
    Public Property ViewType As EnumViewType = EnumViewType.Hebdomadaire

    Public Property Pages As New List(Of PagePlayer)
End Class
