Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema

Partial Public Class SERVICE
    Public Property ID_SERVICE As Long
    Public Property ID_BAT As Long
    Public Property LIBELLE As String
    Public Property DETAILS As String
    Public Property DATE_CREATION As Date

    <ForeignKey("ID_BAT")>
    Public Overridable Property BATIMENT As BATIMENT
    Public Overridable Property BUREAU As ICollection(Of BUREAU) = New HashSet(Of BUREAU)

End Class
