Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.BATIMENT",
                Function(c) New With
                    {
                        .ID_BAT = c.Long(nullable := False, identity := True),
                        .LIBELLE = c.String(nullable := False, maxLength := 150),
                        .POSITION = c.String(nullable := False, maxLength := 2),
                        .NBRE_PIECE = c.Int(),
                        .DETAILS = c.String(maxLength := 4000),
                        .DATE_CREATION = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID_BAT)
            
            CreateTable(
                "dbo.LIEUX",
                Function(c) New With
                    {
                        .ID_LIEU = c.Long(nullable := False, identity := True),
                        .ID_BAT = c.Long(nullable := False),
                        .LIBELLE = c.String(nullable := False, maxLength := 150),
                        .EMPLACEMENT = c.String(maxLength := 4000),
                        .CAPACITE = c.Long(nullable := False),
                        .TYPE_LIEU = c.Int(nullable := False),
                        .DETAILS = c.String(maxLength := 4000),
                        .DATE_CREATION = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID_LIEU) _
                .ForeignKey("dbo.BATIMENT", Function(t) t.ID_BAT, cascadeDelete := True) _
                .Index(Function(t) t.ID_BAT)
            
            CreateTable(
                "dbo.RESERVATION",
                Function(c) New With
                    {
                        .ID_RESERVATION = c.Long(nullable:=False, identity:=True),
                        .ID_LIEU = c.Long(nullable:=False),
                        .ThemeId = c.Long(nullable:=False),
                        .UserId = c.String(nullable:=False, maxLength:=128),
                        .TelPresiSce = c.String(maxLength:=150),
                        .PresidentSeance = c.String(nullable:=False, maxLength:=250),
                        .Objet = c.String(nullable:=False, maxLength:=4000),
                        .DATE_DEBUT = c.DateTime(nullable:=False),
                        .DATE_FIN = c.DateTime(nullable:=False),
                        .HEURE_DEBUT = c.DateTime(nullable:=False),
                        .HEURE_FIN = c.DateTime(nullable:=False),
                        .NBRE_PERS = c.Int(),
                        .DETAILS = c.String(),
                        .ETAT = c.Int(nullable:=False),
                        .CurrentState = c.Int(nullable:=False),
                        .DATE_CREATION = c.DateTime(nullable:=False),
                        .NextReservationId = c.Long()
                    }) _
                .PrimaryKey(Function(t) t.ID_RESERVATION) _
                .ForeignKey("dbo.LIEUX", Function(t) t.ID_LIEU, cascadeDelete:=True) _
                .ForeignKey("dbo.RESERVATION", Function(t) t.NextReservationId) _
                .ForeignKey("dbo.Themes", Function(t) t.ThemeId, cascadeDelete:=True) _
                .Index(Function(t) t.ID_LIEU) _
                .Index(Function(t) t.NextReservationId) _
                .Index(Function(t) t.ThemeId)

            CreateTable(
                "dbo.PARTICIPANT",
                Function(c) New With
                    {
                        .ID_PARTICIPANT = c.Long(nullable:=False, identity:=True),
                        .NOM = c.String(nullable:=False, maxLength:=150),
                        .PRENOM = c.String(maxLength:=150),
                        .FONCTION = c.String(maxLength:=150),
                        .EMAIL = c.String(maxLength:=150),
                        .TELEPHONE = c.String(maxLength:=150)
                    }) _
                .PrimaryKey(Function(t) t.ID_PARTICIPANT)

            CreateTable(
                "dbo.Themes",
                Function(c) New With
                    {
                        .Id = c.Long(nullable:=False, identity:=True),
                        .Libelle = c.String(nullable:=False, maxLength:=250),
                        .DateCreation = c.DateTime(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.Id)

            CreateTable(
                "dbo.PT_AFFICHAGE",
                Function(c) New With
                    {
                        .ID_PT_AFFICH = c.Long(nullable:=False, identity:=True),
                        .ID_BAT = c.Long(nullable:=False),
                        .LIBELLE = c.String(nullable:=False, maxLength:=150),
                        .EMPLACEMENT = c.String(nullable:=False, maxLength:=4000),
                        .DETAILS = c.String(maxLength:=4000),
                        .DATE_CREATION = c.DateTime(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.ID_PT_AFFICH) _
                .ForeignKey("dbo.BATIMENT", Function(t) t.ID_BAT, cascadeDelete:=True) _
                .Index(Function(t) t.ID_BAT)

            CreateTable(
                "dbo.SERVICE",
                Function(c) New With
                    {
                        .ID_SERVICE = c.Long(nullable:=False, identity:=True),
                        .ID_BAT = c.Long(nullable:=False),
                        .LIBELLE = c.String(nullable:=False, maxLength:=150),
                        .DETAILS = c.String(maxLength:=4000),
                        .DATE_CREATION = c.DateTime(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.ID_SERVICE) _
                .ForeignKey("dbo.BATIMENT", Function(t) t.ID_BAT, cascadeDelete:=True) _
                .Index(Function(t) t.ID_BAT)

            CreateTable(
                "dbo.BUREAU",
                Function(c) New With
                    {
                        .ID_BUREAU = c.Long(nullable:=False, identity:=True),
                        .ID_SERVICE = c.Long(nullable:=False),
                        .LIBELLE = c.String(nullable:=False, maxLength:=150),
                        .EMPLACEMENT = c.String(maxLength:=4000),
                        .DETAILS = c.String(maxLength:=4000),
                        .DATE_CREATION = c.DateTime(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.ID_BUREAU) _
                .ForeignKey("dbo.SERVICE", Function(t) t.ID_SERVICE, cascadeDelete:=True) _
                .Index(Function(t) t.ID_SERVICE)

            CreateTable(
                "dbo.PieceJointes",
                Function(c) New With
                    {
                        .Id = c.Long(nullable:=False, identity:=True),
                        .Libelle = c.String(maxLength:=250),
                        .FileName = c.String(maxLength:=250),
                        .UserId = c.String(nullable:=False, maxLength:=128),
                        .DateCreation = c.DateTime(nullable:=False),
                        .ID_RESERVATION = c.Long(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.RESERVATION", Function(t) t.ID_RESERVATION, cascadeDelete:=True) _
                .Index(Function(t) t.ID_RESERVATION)

            CreateTable(
                "dbo.AspNetRoles",
                Function(c) New With
                    {
                        .Id = c.String(nullable:=False, maxLength:=128),
                        .Name = c.String(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.Id)

            CreateTable(
                "dbo.AspNetUsers",
                Function(c) New With
                    {
                        .Id = c.String(nullable:=False, maxLength:=128),
                        .UserName = c.String(),
                        .PasswordHash = c.String(),
                        .SecurityStamp = c.String(),
                        .NOMS = c.String(maxLength:=150),
                        .PRENOMS = c.String(maxLength:=150),
                        .TEL = c.String(maxLength:=150),
                        .EMAIL = c.String(maxLength:=150),
                        .DATE_CREATION = c.DateTime(),
                        .ID_SERVICE = c.Long(),
                        .Discriminator = c.String(nullable:=False, maxLength:=128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.SERVICE", Function(t) t.ID_SERVICE, cascadeDelete:=True) _
                .Index(Function(t) t.ID_SERVICE)

            CreateTable(
                "dbo.AspNetUserClaims",
                Function(c) New With
                    {
                        .Id = c.Int(nullable:=False, identity:=True),
                        .ClaimType = c.String(),
                        .ClaimValue = c.String(),
                        .User_Id = c.String(nullable:=False, maxLength:=128)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.User_Id, cascadeDelete:=True) _
                .Index(Function(t) t.User_Id)

            CreateTable(
                "dbo.AspNetUserLogins",
                Function(c) New With
                    {
                        .UserId = c.String(nullable:=False, maxLength:=128),
                        .LoginProvider = c.String(nullable:=False, maxLength:=128),
                        .ProviderKey = c.String(nullable:=False, maxLength:=128)
                    }) _
                .PrimaryKey(Function(t) New With {t.UserId, t.LoginProvider, t.ProviderKey}) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete:=True) _
                .Index(Function(t) t.UserId)

            CreateTable(
                "dbo.AspNetUserRoles",
                Function(c) New With
                    {
                        .UserId = c.String(nullable:=False, maxLength:=128),
                        .RoleId = c.String(nullable:=False, maxLength:=128)
                    }) _
                .PrimaryKey(Function(t) New With {t.UserId, t.RoleId}) _
                .ForeignKey("dbo.AspNetRoles", Function(t) t.RoleId, cascadeDelete:=True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete:=True) _
                .Index(Function(t) t.RoleId) _
                .Index(Function(t) t.UserId)

            CreateTable(
                "dbo.PARTICIPANTRESERVATIONs",
                Function(c) New With
                    {
                        .PARTICIPANT_ID_PARTICIPANT = c.Long(nullable:=False),
                        .RESERVATION_ID_RESERVATION = c.Long(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) New With {t.PARTICIPANT_ID_PARTICIPANT, t.RESERVATION_ID_RESERVATION}) _
                .ForeignKey("dbo.PARTICIPANT", Function(t) t.PARTICIPANT_ID_PARTICIPANT, cascadeDelete:=True) _
                .ForeignKey("dbo.RESERVATION", Function(t) t.RESERVATION_ID_RESERVATION, cascadeDelete:=True) _
                .Index(Function(t) t.PARTICIPANT_ID_PARTICIPANT) _
                .Index(Function(t) t.RESERVATION_ID_RESERVATION)

        End Sub

        Public Overrides Sub Down()
            DropForeignKey("dbo.AspNetUsers", "ID_SERVICE", "dbo.SERVICE")
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles")
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.PieceJointes", "ID_RESERVATION", "dbo.RESERVATION")
            DropForeignKey("dbo.BUREAU", "ID_SERVICE", "dbo.SERVICE")
            DropForeignKey("dbo.SERVICE", "ID_BAT", "dbo.BATIMENT")
            DropForeignKey("dbo.PT_AFFICHAGE", "ID_BAT", "dbo.BATIMENT")
            DropForeignKey("dbo.RESERVATION", "ThemeId", "dbo.Themes")
            DropForeignKey("dbo.PARTICIPANTRESERVATIONs", "RESERVATION_ID_RESERVATION", "dbo.RESERVATION")
            DropForeignKey("dbo.PARTICIPANTRESERVATIONs", "PARTICIPANT_ID_PARTICIPANT", "dbo.PARTICIPANT")
            DropForeignKey("dbo.RESERVATION", "NextReservationId", "dbo.RESERVATION")
            DropForeignKey("dbo.RESERVATION", "ID_LIEU", "dbo.LIEUX")
            DropForeignKey("dbo.LIEUX", "ID_BAT", "dbo.BATIMENT")
            DropIndex("dbo.AspNetUsers", New String() {"ID_SERVICE"})
            DropIndex("dbo.AspNetUserClaims", New String() {"User_Id"})
            DropIndex("dbo.AspNetUserRoles", New String() {"UserId"})
            DropIndex("dbo.AspNetUserRoles", New String() {"RoleId"})
            DropIndex("dbo.AspNetUserLogins", New String() {"UserId"})
            DropIndex("dbo.PieceJointes", New String() {"ID_RESERVATION"})
            DropIndex("dbo.BUREAU", New String() {"ID_SERVICE"})
            DropIndex("dbo.SERVICE", New String() {"ID_BAT"})
            DropIndex("dbo.PT_AFFICHAGE", New String() {"ID_BAT"})
            DropIndex("dbo.RESERVATION", New String() {"ThemeId"})
            DropIndex("dbo.PARTICIPANTRESERVATIONs", New String() {"RESERVATION_ID_RESERVATION"})
            DropIndex("dbo.PARTICIPANTRESERVATIONs", New String() {"PARTICIPANT_ID_PARTICIPANT"})
            DropIndex("dbo.RESERVATION", New String() {"NextReservationId"})
            DropIndex("dbo.RESERVATION", New String() {"ID_LIEU"})
            DropIndex("dbo.LIEUX", New String() {"ID_BAT"})
            DropTable("dbo.PARTICIPANTRESERVATIONs")
            DropTable("dbo.AspNetUserRoles")
            DropTable("dbo.AspNetUserLogins")
            DropTable("dbo.AspNetUserClaims")
            DropTable("dbo.AspNetUsers")
            DropTable("dbo.AspNetRoles")
            DropTable("dbo.PieceJointes")
            DropTable("dbo.BUREAU")
            DropTable("dbo.SERVICE")
            DropTable("dbo.PT_AFFICHAGE")
            DropTable("dbo.Themes")
            DropTable("dbo.PARTICIPANT")
            DropTable("dbo.RESERVATION")
            DropTable("dbo.LIEUX")
            DropTable("dbo.BATIMENT")
        End Sub
    End Class
End Namespace
