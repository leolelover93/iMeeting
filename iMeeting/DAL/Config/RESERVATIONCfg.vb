
Imports System.Data.Entity.ModelConfiguration
Imports System.ComponentModel.DataAnnotations.Schema

Public Class RESERVATIONCfg
    Inherits EntityTypeConfiguration(Of RESERVATION)
	
	Public Sub New()
		Me.ToTable("RESERVATION")
		Me.HasKey(Function(p) p.ID_RESERVATION)
		Me.Property(Function(p) p.ID_RESERVATION).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired()
		Me.Property(Function(p) p.ID_LIEU).IsRequired()
		Me.Property(Function(p) p.UserId).IsRequired().HasMaxLength(128)
        Me.Property(Function(p) p.TelPresiSce).HasMaxLength(150)
        Me.Property(Function(p) p.PresidentSeance).IsRequired().HasMaxLength(250)
        Me.Property(Function(p) p.Objet).IsRequired().HasMaxLength(4000)
		Me.Property(Function(p) p.DATE_DEBUT).IsRequired()
		Me.Property(Function(p) p.DATE_FIN).IsRequired()
		Me.Property(Function(p) p.HEURE_DEBUT).IsRequired()
		Me.Property(Function(p) p.HEURE_FIN).IsRequired()
        Me.Property(Function(p) p.ETAT).IsRequired()
        Me.Property(Function(p) p.CurrentState).IsRequired()
		Me.Property(Function(p) p.DATE_CREATION).IsRequired()
	End Sub
 End Class
    
