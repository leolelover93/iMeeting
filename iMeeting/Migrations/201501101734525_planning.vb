Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class planning
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.PlanningSettings",
                Function(c) New With
                    {
                        .Id = c.Long(nullable:=False, identity:=True),
                        .PointAffichageId = c.Long(nullable:=False),
                        .LieuId = c.Long(nullable:=False),
                        .Afficher = c.Boolean(nullable:=False),
                        .ViewType = c.Int(nullable:=False),
                        .MaxCharsPage = c.Int(nullable:=False),
                        .MaxPageElements = c.Int(nullable:=False),
                        .LibelleVue = c.String(maxLength:=150),
                        .Animation = c.String(maxLength:=150),
                        .AnimationTime = c.Int(nullable:=False),
                        .ViewShowTime = c.Int(nullable:=False),
                        .LibellePeriode = c.String(maxLength:=150),
                        .BackgoundImage = c.String(maxLength:=150),
                        .DivClassName = c.String(maxLength:=150),
                        .UserId = c.String(nullable:=False, maxLength:=128),
                        .DateCreation = c.DateTime(nullable:=False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.LIEUX", Function(t) t.LieuId) _
                .ForeignKey("dbo.PT_AFFICHAGE", Function(t) t.PointAffichageId) _
                .Index(Function(t) t.LieuId) _
                .Index(Function(t) t.PointAffichageId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.PlanningSettings", "PointAffichageId", "dbo.PT_AFFICHAGE")
            DropForeignKey("dbo.PlanningSettings", "LieuId", "dbo.LIEUX")
            DropIndex("dbo.PlanningSettings", New String() { "PointAffichageId" })
            DropIndex("dbo.PlanningSettings", New String() { "LieuId" })
            DropTable("dbo.PlanningSettings")
        End Sub
    End Class
End Namespace
