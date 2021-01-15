
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class LIEUXCfg
    Inherits EntityTypeConfiguration(Of LIEUX)
	
	Public Sub New()
		Me.ToTable("LIEUX")
		Me.HasKey(Function(p) p.ID_LIEU)
		Me.Property(Function(p) p.ID_LIEU).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.ID_BAT).IsRequired()
		Me.Property(Function(p) p.LIBELLE).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.EMPLACEMENT).HasMaxLength(4000)
		Me.Property(Function(p) p.CAPACITE).IsRequired()
		Me.Property(Function(p) p.TYPE_LIEU).IsRequired()
		Me.Property(Function(p) p.DETAILS).HasMaxLength(4000)
        Me.Property(Function(p) p.DATE_CREATION).IsRequired()
        Me.Property(Function(p) p.Etat).IsRequired()
    End Sub
 End Class
    
