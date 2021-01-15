
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class PARTICIPANTCfg
    Inherits EntityTypeConfiguration(Of PARTICIPANT)
	
	Public Sub New()
		Me.ToTable("PARTICIPANT")
		Me.HasKey(Function(p) p.ID_PARTICIPANT)
		Me.Property(Function(p) p.ID_PARTICIPANT).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.NOM).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.PRENOM).HasMaxLength(150)
		Me.Property(Function(p) p.FONCTION).HasMaxLength(150)
		Me.Property(Function(p) p.EMAIL).HasMaxLength(150)
		Me.Property(Function(p) p.TELEPHONE).HasMaxLength(150)
	End Sub
 End Class
    
