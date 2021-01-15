Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of ApplicationDbContext)

        Public Sub New()
            AutomaticMigrationsEnabled = True
        End Sub

        Protected Overrides Sub Seed(context As ApplicationDbContext)
            'AddBatiment(context)
            'AddUserAndRoles()
        End Sub

        Private Function AddUserAndRoles() As Boolean
            Dim success As Boolean = False

            Dim idManager = New IdentityManager()
            success = idManager.CreateRole("Administrateur")
            If Not success = True Then
                Return success
            End If

            success = idManager.CreateRole("SCC")
            If Not success = True Then
                Return success
            End If

            success = idManager.CreateRole("Utilisateur")
            If Not success Then
                Return success
            End If


            Dim newUser = New ApplicationUser() With { _
                 .UserName = "administrateur", _
                 .NOMS = "Administrateur", _
                 .PRENOMS = "", _
                 .TEL = "97979797", _
                 .ID_SERVICE = 1, _
                 .DATE_CREATION = Now,
                 .EMAIL = "admin@spm.gov.cm" _
            }

            ' Be careful here - you  will need to use a password which will 
            ' be valid under the password rules for the application, 
            ' or the process will abort:
            success = idManager.CreateUser(newUser, "Password123*")
            If Not success Then
                Return success
            End If

            success = idManager.AddUserToRole(newUser.Id, "Administrateur")
            If Not success Then
                Return success
            End If

            success = idManager.AddUserToRole(newUser.Id, "SCC")
            If Not success Then
                Return success
            End If

            success = idManager.AddUserToRole(newUser.Id, "Utilisateur")
            If Not success Then
                Return success
            End If

            Return success
        End Function

        Private Sub AddBatiment(context As ApplicationDbContext)
            Dim items As New List(Of Object)

            items.Add(New BATIMENT With {.LIBELLE = "Immeuble étoile", .POSITION = "C", .DATE_CREATION = Now})
            items.Add(New BATIMENT With {.LIBELLE = "Extension 1", .POSITION = "N", .DATE_CREATION = Now})
            items.Add(New BATIMENT With {.LIBELLE = "Extension 2", .POSITION = "N", .DATE_CREATION = Now})
            items.Add(New BATIMENT With {.LIBELLE = "Extension 3", .POSITION = "N", .DATE_CREATION = Now})
            items.Add(New BATIMENT With {.LIBELLE = "Guérite Nord", .POSITION = "N", .DATE_CREATION = Now})
            items.Add(New BATIMENT With {.LIBELLE = "Guérite Sud", .POSITION = "S", .DATE_CREATION = Now})
            For Each item In items
                context.Set(Of BATIMENT).AddOrUpdate(Function(e) e.LIBELLE, item)
                context.SaveChanges()
            Next

            Dim bat = items(0)
            Dim an1 = items(1)
            Dim Gn = items(4)
            Dim Gs = items(5)
            items.Clear()
            items.Add(New SERVICE With {.LIBELLE = "SCC", .BATIMENT = bat, .DATE_CREATION = Now})
            items.Add(New SERVICE With {.LIBELLE = "SG", .BATIMENT = bat, .DATE_CREATION = Now})
            items.Add(New SERVICE With {.LIBELLE = "CI", .BATIMENT = bat, .DATE_CREATION = Now})
            For Each item In items
                context.Set(Of SERVICE).AddOrUpdate(Function(e) e.LIBELLE, item)
                context.SaveChanges()
            Next
            Dim svc = items(0)

            items.Clear()
            items.Add(New PT_AFFICHAGE With {.LIBELLE = "Hall", .BATIMENT = bat, .EMPLACEMENT = "Entrée Hall", .DATE_CREATION = Now})
            items.Add(New PT_AFFICHAGE With {.LIBELLE = "Guérite Sud", .BATIMENT = Gs, .EMPLACEMENT = "Entrée Guérite Sud", .DATE_CREATION = Now})
            items.Add(New PT_AFFICHAGE With {.LIBELLE = "Guérite Nord", .BATIMENT = Gn, .EMPLACEMENT = "Entrée Guérite Nord", .DATE_CREATION = Now})
            For Each item In items
                context.Set(Of PT_AFFICHAGE).AddOrUpdate(Function(e) e.LIBELLE, item)
                context.SaveChanges()
            Next

            items.Clear()
            items.Add(New BUREAU With {.LIBELLE = "Chef SCC", .SERVICE = svc, .DATE_CREATION = Now})
            items.Add(New BUREAU With {.LIBELLE = "Sécretariat SCC", .SERVICE = svc, .DATE_CREATION = Now})
            For Each item In items
                context.Set(Of BUREAU).AddOrUpdate(Function(e) e.LIBELLE, item)
                context.SaveChanges()
            Next

            Dim themes = New List(Of Theme)
            themes.Add(New Theme With {.Libelle = "Finance", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Politique", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Social", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Culture", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Sport", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Energie", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Mines", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Economie", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Education", .DateCreation = Now})
            themes.Add(New Theme With {.Libelle = "Juridique", .DateCreation = Now})
            For Each item In themes
                context.Set(Of Theme).AddOrUpdate(Function(e) e.Libelle, item)
                context.SaveChanges()
            Next

            items.Clear()
            items.Add(New LIEUX With {.LIBELLE = "Salle Rez-de-Chaussée", .BATIMENT = bat, .TYPE_LIEU = 0, .EMPLACEMENT = "Rez-de-Chaussée", .DATE_CREATION = Now})
            items.Add(New LIEUX With {.LIBELLE = "Salle du Ministre", .BATIMENT = bat, .TYPE_LIEU = 0, .EMPLACEMENT = "Sixième niveau", .DATE_CREATION = Now})
            items.Add(New LIEUX With {.LIBELLE = "Salle SCC", .BATIMENT = bat, .TYPE_LIEU = 0, .EMPLACEMENT = "Bureau du Chef SCC", .DATE_CREATION = Now})
            items.Add(New LIEUX With {.LIBELLE = "Salle Annexe 1", .BATIMENT = an1, .TYPE_LIEU = 0, .EMPLACEMENT = "Premier niveau", .DATE_CREATION = Now})
            items.Add(New LIEUX With {.LIBELLE = "Espace cérémonie Nord", .BATIMENT = bat, .TYPE_LIEU = 1, .EMPLACEMENT = "Près de la guérite nord", .DATE_CREATION = Now})
            items.Add(New LIEUX With {.LIBELLE = "Espace cérémonie principal", .BATIMENT = bat, .TYPE_LIEU = 1, .EMPLACEMENT = "A proximité du batiment principal", .DATE_CREATION = Now})
            For Each item In items
                context.Set(Of LIEUX).AddOrUpdate(Function(e) e.LIBELLE, item)
                context.SaveChanges()
            Next

            Dim rsv = New List(Of RESERVATION)
            Dim nbre_lieux = items.Count
            Dim nbre_theme = themes.Count
            Dim date_ref = New Date(2014, 9, 29)
            Dim heure_ref = New Date(2014, 9, 29, 8, 0, 0)
            Dim nbre_reserv = nbre_lieux * 14
            For i = 1 To nbre_reserv
                Dim myreserv = New RESERVATION With {
                        .LIEUX = items(i Mod nbre_lieux),
                        .Theme = themes(i Mod nbre_theme),
                        .Objet = String.Format("Objet {0}", i),
                        .TelPresiSce = String.Format("Numéro {0}", i),
                        .UserId = "initial",
                        .PresidentSeance = String.Format("Président séance {0}", i),
                        .NBRE_PERS = i * 4,
                        .DATE_DEBUT = date_ref.AddDays(i Mod 7),
                        .DATE_FIN = .DATE_DEBUT,
                        .HEURE_DEBUT = heure_ref.AddDays(i Mod 7).AddHours((i Mod 5) * 2),
                        .HEURE_FIN = .HEURE_DEBUT.AddHours((i Mod 2) + 1),
                        .DATE_CREATION = Now
                    }

                For j = 1 To 5
                    myreserv.PARTICIPANT.Add(New PARTICIPANT With {
                                           .NOM = String.Format("Nom {0}{1}", i, j),
                                           .PRENOM = String.Format("Prenom {0}{1}", i, j),
                                           .EMAIL = String.Format("mail{0}{1}@example.com", i, j)
                                       })
                Next

                rsv.Add(myreserv)
            Next
            For Each item In rsv
                context.Set(Of RESERVATION).AddOrUpdate(Function(e) e.TelPresiSce, item)
                Try
                    context.SaveChanges()
                Catch ex As Exception
                    Throw New Exception(Util.getError(ex))
                End Try
            Next
        End Sub

    End Class

End Namespace
