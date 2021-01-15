Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class TexteDefil
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.TexteDefilants",
                Function(c) New With
                    {
                        .Id = c.Long(nullable := False, identity := True),
                        .Message = c.String(nullable := False),
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .DateCreation = c.DateTime(nullable := False),
                        .EstPublie = c.Boolean(nullable := False),
                        .DateDebut = c.DateTime(),
                        .DateFin = c.DateTime(),
                        .PointAffichageId = c.Long(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.PT_AFFICHAGE", Function(t) t.PointAffichageId, cascadeDelete := True) _
                .Index(Function(t) t.PointAffichageId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.TexteDefilants", "PointAffichageId", "dbo.PT_AFFICHAGE")
            DropIndex("dbo.TexteDefilants", New String() { "PointAffichageId" })
            DropTable("dbo.TexteDefilants")
        End Sub
    End Class
End Namespace
