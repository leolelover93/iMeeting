
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class PT_AFFICHAGECfg
    Inherits EntityTypeConfiguration(Of PT_AFFICHAGE)
	
	Public Sub New()
		Me.ToTable("PT_AFFICHAGE")
		Me.HasKey(Function(p) p.ID_PT_AFFICH)
		Me.Property(Function(p) p.ID_PT_AFFICH).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.ID_BAT).IsRequired()
		Me.Property(Function(p) p.LIBELLE).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.EMPLACEMENT).IsRequired().HasMaxLength(4000)
		Me.Property(Function(p) p.DETAILS).HasMaxLength(4000)
		Me.Property(Function(p) p.DATE_CREATION).IsRequired()
	End Sub
 End Class
    
