Imports System.Web.Mvc

Public Class PlayerController
    Inherits Controller

    <AllowAnonymous>
    Function PlayerSG(id As Long) As ActionResult
        Return View(New PlanningHebdoPlayer(id))
    End Function

    <AllowAnonymous>
    Function TextDefil(id As Long?) As ActionResult
        Dim id_pt_affich As Long = -1
        If id.HasValue Then
            id_pt_affich = id.Value
        End If
        Dim date_du_jour As Date = Now.Date
        Dim db As New ApplicationDbContext

        Dim msg = (From e In db.TexteDefilants
                  Where e.EstPublie = True And (e.PointAffichageId = id_pt_affich Or e.PointAffichageId = 0) And
                    (e.DateDebut <= date_du_jour Or Not e.DateDebut.HasValue) And
                    (e.DateFin >= date_du_jour Or Not e.DateFin.HasValue)
                  Select e.Message).ToList

        ' En principe on prend dans la BD
        'Return View(model:=ConfigurationManager.AppSettings("PlayerDefilTexte") & "#" & id_pt_affich)
        Return View(msg)
    End Function

    <AllowAnonymous>
    Function PlayerNB(id As Long) As ActionResult
        Return View(New PlanningHebdoPlayer(id))
    End Function

    <AllowAnonymous>
    Function PlayerGuerite(id As Long) As ActionResult
        Return View(New PlanningHebdoPlayer(id))
    End Function

    <AllowAnonymous>
    Function Etoile() As ActionResult
        ViewBag.PlanningUrl = Url.Action("PlayerEtoile", New With {.id = 1})
        ViewBag.TextDefilUrl = Url.Action("TextDefil", New With {.id = 1})
        ViewBag.HeureUrl = Url.Action("Heure")
        Return View()
    End Function

    <AllowAnonymous>
    Function SG() As ActionResult
        ViewBag.PlanningUrl = Url.Action("PlayerSG", New With {.id = 3})
        ViewBag.TextDefilUrl = Url.Action("TextDefil", New With {.id = 3})
        ViewBag.HeureUrl = Url.Action("Heure")
        Return View()
    End Function

    <AllowAnonymous>
    Function NB() As ActionResult
        ViewBag.PlanningUrl = Url.Action("PlayerNB", New With {.id = 4})
        ViewBag.TextDefilUrl = Url.Action("TextDefil", New With {.id = 4})
        ViewBag.HeureUrl = Url.Action("Heure")
        Return View()
    End Function

    <AllowAnonymous>
    Function Guerite() As ActionResult
        ViewBag.PlanningUrl = Url.Action("PlayerGuerite", New With {.id = 2})
        ViewBag.TextDefilUrl = Url.Action("TextDefil", New With {.id = 2})
        ViewBag.HeureUrl = Url.Action("Heure")
        Return View()
    End Function

    <AllowAnonymous>
    Function PlayerEtoile(id As Long) As ActionResult
        Return View(New PlanningHebdoPlayer(id))
    End Function

    <AllowAnonymous>
    Function Heure() As ActionResult
        Return View()
    End Function
End Class