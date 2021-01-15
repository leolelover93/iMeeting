Imports System.ComponentModel.DataAnnotations.Schema

Public Class PieceJointe
    Public Property Id As Long
    Public Property Libelle As String
    Public Property FileName As String
    Public Property UserId As String
    Public Property DateCreation As DateTime = Now

    Public Property ID_RESERVATION As Long
    <ForeignKey("ID_RESERVATION")>
    Public Overridable Property RESERVATION As RESERVATION
End Class
