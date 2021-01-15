
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class SERVICECfg
    Inherits EntityTypeConfiguration(Of SERVICE)
	
	Public Sub New()
		Me.ToTable("SERVICE")
		Me.HasKey(Function(p) p.ID_SERVICE)
		Me.Property(Function(p) p.ID_SERVICE).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.ID_BAT).IsRequired()
		Me.Property(Function(p) p.LIBELLE).IsRequired().HasMaxLength(150)
		Me.Property(Function(p) p.DETAILS).HasMaxLength(4000)
		Me.Property(Function(p) p.DATE_CREATION).IsRequired()
	End Sub
 End Class
    
