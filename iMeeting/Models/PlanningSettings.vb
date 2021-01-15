Imports System.ComponentModel.DataAnnotations.Schema

Public Class PlanningSettings
    Public Property Id As Long

    Public Property PointAffichageId As Long
    Public Property LieuId As Long
    Public Property Afficher As Boolean = True
    Public Property ViewType As EnumViewType = EnumViewType.Hebdomadaire
    Public Property MaxCharsPage As Integer = 400
    Public Property MaxPageElements As Integer = 4 ' 4 éléments par page par défaut
    Public Property LibelleVue As String = "" 'ConfigurationManager.AppSettings("PlayerHebdoTitle") '"Planning hebdomadaire des réunions"
    Public Property Animation As String = ""
    Public Property AnimationTime As Integer = 2000
    Public Property ViewShowTime As Integer = 5000 ' 5 Sec mais modifiée par PlanningHebdoPlayer
    Public Property LibellePeriode As String = ""
    Public Property BackgoundImage As String = "~/Ecran/bg.png"
    Public Property DivClassName As String = "StylePageDefault"

    Public Property UserId As String
    Public Property DateCreation As DateTime = Now

    <ForeignKey("PointAffichageId")>
    Public Overridable Property PointAffichage As PT_AFFICHAGE

    <ForeignKey("LieuId")>
    Public Overridable Property Lieu As LIEUX
End Class
