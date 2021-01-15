Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class salleaddstate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.LIEUX", "Etat", Function(c) c.Int(nullable:=False, defaultValue:=0))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.LIEUX", "Etat")
        End Sub
    End Class
End Namespace
