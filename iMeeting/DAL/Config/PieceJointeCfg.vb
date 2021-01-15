Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class PieceJointeCfg
    Inherits EntityTypeConfiguration(Of PieceJointe)

    Public Sub New()
        Me.Property(Function(p) p.Libelle).HasMaxLength(250)
        Me.Property(Function(p) p.FileName).HasMaxLength(250)
        Me.Property(Function(p) p.UserId).IsRequired().HasMaxLength(128)
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
