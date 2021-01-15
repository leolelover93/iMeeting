Imports System.Data.Entity.ModelConfiguration

Public Class PlanningSettingsCfg
    Inherits EntityTypeConfiguration(Of PlanningSettings)

    Public Sub New()
        Me.HasRequired(Function(p) p.Lieu).WithMany().WillCascadeOnDelete(False)
        Me.HasRequired(Function(p) p.PointAffichage).WithMany().WillCascadeOnDelete(False)
        Me.Property(Function(p) p.ViewType).IsRequired()
        Me.Property(Function(p) p.ViewType).IsRequired()
        Me.Property(Function(p) p.LibelleVue).HasMaxLength(150)
        Me.Property(Function(p) p.Animation).HasMaxLength(150)
        Me.Property(Function(p) p.LibellePeriode).HasMaxLength(150)
        Me.Property(Function(p) p.BackgoundImage).HasMaxLength(150)
        Me.Property(Function(p) p.DivClassName).HasMaxLength(150)
        Me.Property(Function(p) p.UserId).IsRequired().HasMaxLength(128)
        Me.Property(Function(p) p.DateCreation).IsRequired()
    End Sub
End Class
