Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema

Partial Public Class PT_AFFICHAGE
    Public Property ID_PT_AFFICH As Long
    Public Property ID_BAT As Long
    Public Property LIBELLE As String
    Public Property EMPLACEMENT As String
    Public Property DETAILS As String
    Public Property DATE_CREATION As Date

    <ForeignKey("ID_BAT")>
    Public Overridable Property BATIMENT As BATIMENT

End Class
