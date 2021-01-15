Imports System.Data.Entity.ModelConfiguration

Public Class ThemeCfg
    Inherits EntityTypeConfiguration(Of Theme)

    Public Sub New()
        Me.Property(Function(p) p.Libelle).IsRequired().HasMaxLength(250)
    End Sub
End Class
