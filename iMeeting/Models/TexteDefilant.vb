Imports System.ComponentModel.DataAnnotations.Schema

Public Class TexteDefilant
    Public Property Id As Long
    Public Property Message As String
    Public Property UserId As String
    Public Property DateCreation As DateTime = Now
    Public Property EstPublie As Boolean = False
    Public Property DateDebut As DateTime?
    Public Property DateFin As DateTime?
    Public Property PointAffichageId As Long

    <ForeignKey("PointAffichageId")>
    Public Overridable Property PointAffichage As PT_AFFICHAGE
End Class
