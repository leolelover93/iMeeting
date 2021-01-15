
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class BATIMENTCfg
    Inherits EntityTypeConfiguration(Of BATIMENT)
	
	Public Sub New()
		Me.ToTable("BATIMENT")
		Me.HasKey(Function(p) p.ID_BAT)
		Me.Property(Function(p) p.ID_BAT).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.LIBELLE).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.POSITION).IsRequired().HasMaxLength(2)
		Me.Property(Function(p) p.DETAILS).HasMaxLength(4000)
		Me.Property(Function(p) p.DATE_CREATION).IsRequired()
	End Sub
 End Class
    
