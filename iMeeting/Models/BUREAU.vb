Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema

Partial Public Class BUREAU
    Public Property ID_BUREAU As Long
    Public Property ID_SERVICE As Long
    Public Property LIBELLE As String
    Public Property EMPLACEMENT As String
    Public Property DETAILS As String
    Public Property DATE_CREATION As Date

    <ForeignKey("ID_SERVICE")>
    Public Overridable Property SERVICE As SERVICE

End Class
