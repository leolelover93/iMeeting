
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class BUREAUCfg
    Inherits EntityTypeConfiguration(Of BUREAU)
	
	Public Sub New()
		Me.ToTable("BUREAU")
		Me.HasKey(Function(p) p.ID_BUREAU)
		Me.Property(Function(p) p.ID_BUREAU).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.ID_SERVICE).IsRequired()
		Me.Property(Function(p) p.LIBELLE).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.EMPLACEMENT).HasMaxLength(4000)
		Me.Property(Function(p) p.DETAILS).HasMaxLength(4000)
		Me.Property(Function(p) p.DATE_CREATION).IsRequired()
	End Sub
 End Class
    
