Imports System.Data.Entity.ModelConfiguration

Public Class TexteDefilantCfg
    Inherits EntityTypeConfiguration(Of TexteDefilant)

    Public Sub New()
        Me.Property(Function(p) p.Message).IsRequired()
        Me.Property(Function(p) p.UserId).IsRequired().HasMaxLength(128)
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
