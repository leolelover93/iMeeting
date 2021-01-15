Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports PagedList
Imports System.Data.Entity
Imports System.Net

<LocalizedAuthorize>
Public Class AccountController
    Inherits BaseController

    Private db As New ApplicationDbContext

    Public Sub New()
        Me.New(New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext())))
    End Sub

    Public Sub New(manager As UserManager(Of ApplicationUser))
        UserManager = manager
    End Sub

    Public Property UserManager As UserManager(Of ApplicationUser)

    '
    ' GET: /Account/Login
    <AllowAnonymous>
    Public Function Login(returnUrl As String) As ActionResult
        ViewBag.ReturnUrl = returnUrl
        Return View()
    End Function


    <LocalizedAuthorize(Roles:="Administrateur")>
    Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
        ViewBag.CurrentSort = sortOrder
        ViewBag.UserNameSortParm = If(String.IsNullOrEmpty(sortOrder), "UserName_desc", "")
        ViewBag.NOMSSortParm = If(sortOrder = "NOMS", "NOMS_desc", "NOMS")
        ViewBag.PRENOMSSortParm = If(sortOrder = "PRENOMS", "PRENOMS_desc", "PRENOMS")
        ViewBag.TELSortParm = If(sortOrder = "TEL", "TEL_desc", "TEL")
        ViewBag.EMAILSortParm = If(sortOrder = "EMAIL", "EMAIL_desc", "EMAIL")
        ViewBag.IDSERVICESortParm = If(sortOrder = "IDSERVICE", "IDSERVICE_desc", "IDSERVICE")


        If Not String.IsNullOrEmpty(searchString) Then
            page = 1
        Else
            searchString = currentFilter
        End If

        ViewBag.CurrentFilter = searchString

        Dim entities = From e In db.Users()


        If Not String.IsNullOrEmpty(searchString) Then
            entities = entities.Where(Function(e) e.UserName.ToUpper.Contains(searchString.ToUpper) Or
                                          e.NOMS.ToUpper.Contains(searchString.ToUpper) Or
                                          e.PRENOMS.ToUpper.Contains(searchString.ToUpper) Or
                                          e.EMAIL.ToUpper.Contains(searchString.ToUpper))
        End If
        ViewBag.EnregCount = entities.Count

        Select Case sortOrder
            Case "UserName"
                entities = entities.OrderBy(Function(e) e.UserName)
            Case "UserName_desc"
                entities = entities.OrderByDescending(Function(e) e.UserName)

            Case "NOMS"
                entities = entities.OrderBy(Function(e) e.NOMS)
            Case "NOMS_desc"
                entities = entities.OrderByDescending(Function(e) e.NOMS)

            Case "PRENOMS"
                entities = entities.OrderBy(Function(e) e.PRENOMS)
            Case "PRENOMS_desc"
                entities = entities.OrderByDescending(Function(e) e.PRENOMS)

            Case "TEL"
                entities = entities.OrderBy(Function(e) e.TEL)
            Case "TEL_desc"
                entities = entities.OrderByDescending(Function(e) e.TEL)

            Case "EMAIL"
                entities = entities.OrderBy(Function(e) e.EMAIL)
            Case "EMAIL_desc"
                entities = entities.OrderByDescending(Function(e) e.EMAIL)

            Case "IDSERVICE"
                entities = entities.OrderBy(Function(e) e.SERVICE.LIBELLE)
            Case "IDSERVICE_desc"
                entities = entities.OrderByDescending(Function(e) e.SERVICE.LIBELLE)

            Case Else
                entities = entities.OrderBy(Function(e) e.NOMS)
                Exit Select
        End Select

        Dim pageSize As Integer = ConfigurationManager.AppSettings("pageSize")
        Dim pageNumber As Integer = If(page, 1)

        Return View(entities.ToPagedList(pageNumber, pageSize))
    End Function

    '
    ' POST: /Account/Login
    <HttpPost>
    <AllowAnonymous>
    <ValidateAntiForgeryToken>
    Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
        If ModelState.IsValid Then
            ' Valider le mot de passe
            Dim appUser = Await UserManager.FindAsync(model.UserName, model.Password)
            If appUser IsNot Nothing Then
                Await SignInAsync(appUser, model.RememberMe)
                Return RedirectToLocal(returnUrl)
            Else
                ModelState.AddModelError("", "Invalid username or password.")
            End If
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Return View(model)
    End Function

    '
    ' GET: /Account/Register
    <LocalizedAuthorize(Roles:="Administrateur")>
    Public Function Register() As ActionResult
        Dim Db = New ApplicationDbContext()
        Dim model = New RegisterViewModel()
        ' Remplir les combos						
        model.IDs = Db.SERVICE.OfType(Of SERVICE)().ToList
        Return View(model)
    End Function

    '
    ' POST: /Account/Register
    <HttpPost>
    <LocalizedAuthorize(Roles:="Administrateur")>
    <ValidateAntiForgeryToken>
    Public Async Function Register(model As RegisterViewModel) As Task(Of ActionResult)
        ' Remplir les combos						
        model.IDs = Db.SERVICE.OfType(Of SERVICE)().ToList
        If ModelState.IsValid Then
            ' Créer un identifiant local avant de connecter l'utilisateur
            Dim user = model.GetUser ' New ApplicationUser() With {.UserName = model.UserName}
            Try
                Dim result = Await UserManager.CreateAsync(user, model.Password)
                If result.Succeeded Then
                    Return RedirectToAction("UserRoles", "Account", New With {.id = user.Id})
                Else
                    AddErrors(result)
                End If
            Catch ex As Exception
                Util.getError(ex, ModelState)
            End Try
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Return View(model)
    End Function

    <LocalizedAuthorize(Roles:="Administrateur")> _
    Public Function Edit(id As String, Optional Message As System.Nullable(Of ManageMessageId) = Nothing) As ActionResult
        If IsNothing(id) Then
            Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        End If
        Dim user = db.Users.Find(id)
        If IsNothing(user) Then
            Return HttpNotFound()
        End If
        Dim model = New EditUserViewModel(user)
        model.IDs = db.SERVICE.OfType(Of SERVICE)().ToList
        ViewBag.MessageId = Message
        Return View(model)
    End Function

    <HttpPost> _
    <LocalizedAuthorize(Roles:="Administrateur")> _
    <ValidateAntiForgeryToken> _
    Public Async Function Edit(model As EditUserViewModel) As Task(Of ActionResult)
        Dim Db = New ApplicationDbContext()
        ' Remplir les combos						
        model.IDs = Db.SERVICE.OfType(Of SERVICE)().ToList

        If ModelState.IsValid Then
            Dim id = model.Id
            Dim user = Db.Users.Find(id)
            If IsNothing(user) Then
                Return HttpNotFound()
            End If
            Dim entity = model.getEntity(user)
            Db.Entry(entity).State = EntityState.Modified

            Try
                Await Db.SaveChangesAsync()

                ' Changer le mot de passe eventuellement
                If Not String.IsNullOrEmpty(model.Password) Then
                    UserManager.RemovePassword(model.Id)

                    Dim result = UserManager.AddPassword(model.Id, model.Password)
                    If result.Succeeded Then
                        Return RedirectToAction("Index")
                    Else
                        AddErrors(result)
                    End If
                Else
                    Return RedirectToAction("Index")
                End If

            Catch ex As Exception
                Util.getError(ex, ModelState)
            End Try
        End If
        ' If we got this far, something failed, redisplay form
        Return View(model)
    End Function

    <LocalizedAuthorize(Roles:="Administrateur")> _
    Public Function Delete(Optional id As String = Nothing) As ActionResult
        If IsNothing(id) Then
            Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        End If
        Dim user = db.Users.Find(id)
        If IsNothing(user) Then
            Return HttpNotFound()
        End If
        Dim model = New EditUserViewModel(user)
        Return View(model)
    End Function


    <HttpPost, ActionName("Delete")> _
    <ValidateAntiForgeryToken> _
    <LocalizedAuthorize(Roles:="Administrateur")> _
    Public Function DeleteConfirmed(id As String) As ActionResult
        Dim user = db.Users.Find(id)
        db.Users.Remove(user)
        Try
            db.SaveChanges()
        Catch ex As Exception
        End Try
        Return RedirectToAction("Index")
    End Function

    <LocalizedAuthorize(Roles:="Administrateur")> _
    Public Function UserRoles(id As String) As ActionResult
        If IsNothing(id) Then
            Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
        End If
        Dim user = db.Users.Find(id)
        If IsNothing(user) Then
            Return HttpNotFound()
        End If
        Dim model = New SelectUserRolesViewModel(user)
        Return View(model)
    End Function


    <HttpPost> _
    <LocalizedAuthorize(Roles:="Administrateur")> _
    <ValidateAntiForgeryToken> _
    Public Function UserRoles(model As SelectUserRolesViewModel) As ActionResult
        If ModelState.IsValid Then
            Dim idManager = New IdentityManager()
            Dim user = db.Users.Find(model.Id)
            idManager.ClearUserRoles(user.Id)
            For Each role As SelectRoleEditorViewModel In model.Roles
                If role.Selected Then
                    idManager.AddUserToRole(user.Id, role.RoleName)
                End If
            Next
            Return RedirectToAction("index")
        End If
        Return View()
    End Function
    '
    ' GET: /Account/Manage
    Public Function Manage(ByVal message As ManageMessageId?) As ActionResult
        ViewData("StatusMessage") =
            If(message = ManageMessageId.ChangePasswordSuccess, "Votre mot de passe a été modifié.", _
                If(message = ManageMessageId.SetPasswordSuccess, "Votre mot de passe a été défini.", _
                    If(message = ManageMessageId.RemoveLoginSuccess, "La connexion externe a été supprimée.", _
                        If(message = ManageMessageId.UnknownError, "Une erreur s'est produite.", _
                        ""))))
        ViewBag.HasLocalPassword = HasPassword()
        ViewBag.ReturnUrl = Url.Action("Manage")
        Return View()
    End Function

    '
    ' POST: /Account/Manage
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Async Function Manage(model As ManageUserViewModel) As Task(Of ActionResult)
        Dim hasLocalLogin As Boolean = HasPassword()
        ViewBag.HasLocalPassword = hasLocalLogin
        ViewBag.ReturnUrl = Url.Action("Manage")
        If hasLocalLogin Then
            If ModelState.IsValid Then
                Dim result As IdentityResult = Await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword)
                If result.Succeeded Then
                    Return RedirectToAction("Manage", New With {
                        .Message = ManageMessageId.ChangePasswordSuccess
                    })
                Else
                    AddErrors(result)
                End If
            End If
        Else
            ' L’utilisateur ne possède pas de mot de passe local. Supprimez donc toutes les erreurs de validation causées par un champ OldPassword manquant
            Dim state As ModelState = ModelState("OldPassword")
            If state IsNot Nothing Then
                state.Errors.Clear()
            End If

            If ModelState.IsValid Then
                Dim result As IdentityResult = Await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword)
                If result.Succeeded Then
                    Return RedirectToAction("Manage", New With {
                        .Message = ManageMessageId.SetPasswordSuccess
                    })
                Else
                    AddErrors(result)
                End If
            End If
        End If

        ' Si nous sommes arrivés là, un échec s’est produit. Réafficher le formulaire
        Return View(model)
    End Function


    '
    ' POST: /Account/LogOff
    <HttpPost>
    <ValidateAntiForgeryToken>
    Public Function LogOff() As ActionResult
        AuthenticationManager.SignOut()
        Return RedirectToAction("Calendar", "Reservation")
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso UserManager IsNot Nothing Then
            UserManager.Dispose()
            UserManager = Nothing
            db.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Assistants"

    Private Function AuthenticationManager() As IAuthenticationManager
        Return HttpContext.GetOwinContext().Authentication
    End Function

    Private Async Function SignInAsync(user As ApplicationUser, isPersistent As Boolean) As Task
        AuthenticationManager.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie)
        Dim identity = Await UserManager.CreateIdentityAsync(user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie)
        AuthenticationManager.SignIn(New AuthenticationProperties() With {.IsPersistent = isPersistent}, identity)
    End Function

    Private Sub AddErrors(result As IdentityResult)
        For Each [error] As String In result.Errors
            ModelState.AddModelError("", [error])
        Next
    End Sub

    Private Function HasPassword() As Boolean
        Dim appUser = UserManager.FindById(User.Identity.GetUserId())
        If (appUser IsNot Nothing) Then
            Return appUser.PasswordHash IsNot Nothing
        End If
        Return False
    End Function

    Private Function RedirectToLocal(returnUrl As String) As ActionResult
        If Url.IsLocalUrl(returnUrl) Then
            Return Redirect(returnUrl)
        Else
            Return RedirectToAction("Index", "Home")
        End If
    End Function

    Public Enum ManageMessageId
        ChangePasswordSuccess
        SetPasswordSuccess
        RemoveLoginSuccess
        UnknownError
    End Enum

#End Region

End Class
