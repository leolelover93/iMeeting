Imports System.ComponentModel.DataAnnotations
Imports iMeeting.My.Resources
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class ManageUserViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <DataType(DataType.Password)>
    <Display(Name:="acc_currentpwd", ResourceType:=GetType(Resource))>
    Public Property OldPassword As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_pwdLong", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="acc_newpwd", ResourceType:=GetType(Resource))>
    Public Property NewPassword As String

    <DataType(DataType.Password)>
    <Display(Name:="acc_confirmnewpwd", ResourceType:=GetType(Resource))>
    <Compare("NewPassword", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_confirmError")>
    Public Property ConfirmPassword As String
End Class

Public Class LoginViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_username", ResourceType:=GetType(Resource))>
    Public Property UserName As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <DataType(DataType.Password)>
    <Display(Name:="acc_pwd", ResourceType:=GetType(Resource))>
    Public Property Password As String

    <Display(Name:="acc_remember", ResourceType:=GetType(Resource))>
    Public Property RememberMe As Boolean
End Class

Public Class RegisterViewModel
    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_username", ResourceType:=GetType(Resource))>
    Public Property UserName As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_pwdLong", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="acc_pwd", ResourceType:=GetType(Resource))>
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="acc_confirm", ResourceType:=GetType(Resource))>
    <Compare("Password", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_confirmError")>
    Public Property ConfirmPassword As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_service", ResourceType:=GetType(Resource))>
    Public Property ID_SERVICE As Long

    Public Property Service As SERVICE
    Public Overridable Property IDs As ICollection(Of SERVICE)

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_noms", ResourceType:=GetType(Resource))>
    Public Property NOMS As String

    <Display(Name:="acc_prenoms", ResourceType:=GetType(Resource))>
    Public Property PRENOMS As String

    <Display(Name:="acc_tel", ResourceType:=GetType(Resource))>
    Public Property TEL As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_email", ResourceType:=GetType(Resource))>
    Public Property EMAIL As String

    Public Function GetUser() As ApplicationUser
        Dim user As New ApplicationUser With {
                .UserName = UserName,
                .NOMS = NOMS,
                .PRENOMS = PRENOMS,
                .ID_SERVICE = ID_SERVICE,
                .TEL = TEL,
                .EMAIL = EMAIL,
        .DATE_CREATION = Now
            }

        Return user
    End Function
End Class

Public Class EditUserViewModel
    Public Sub New()

    End Sub

    Public Sub New(user As ApplicationUser)
        With user
            Id = user.Id
            UserName = .UserName
            NOMS = .NOMS
            PRENOMS = .PRENOMS
            ID_SERVICE = .ID_SERVICE
            Service = .SERVICE
            TEL = .TEL
            EMAIL = .EMAIL
        End With
    End Sub

    Public Function getEntity(user As ApplicationUser) As ApplicationUser
        With user
            .UserName = Me.UserName
            .NOMS = Me.NOMS
            .PRENOMS = Me.PRENOMS
            .ID_SERVICE = Me.ID_SERVICE
            '.SERVICE = Me.Service
            .TEL = Me.TEL
            .EMAIL = Me.EMAIL
        End With

        Return user
    End Function

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_service", ResourceType:=GetType(Resource))>
    Public Property ID_SERVICE As Long

    Public Property Service As SERVICE
    Public Overridable Property IDs As ICollection(Of SERVICE)

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_noms", ResourceType:=GetType(Resource))>
    Public Property NOMS As String

    <Display(Name:="acc_prenoms", ResourceType:=GetType(Resource))>
    Public Property PRENOMS As String

    <Display(Name:="acc_tel", ResourceType:=GetType(Resource))>
    Public Property TEL As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_email", ResourceType:=GetType(Resource))>
    Public Property EMAIL As String

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")>
    <Display(Name:="acc_username", ResourceType:=GetType(Resource))>
    Property UserName As String

    <Display(Name:="user_Id", ResourceType:=GetType(Resource))> _
        <StringLength(128, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="user_IdLong")> _
    Public Property Id As String

    <StringLength(100, ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_pwdLong", MinimumLength:=6)>
    <DataType(DataType.Password)>
    <Display(Name:="acc_pwd", ResourceType:=GetType(Resource))>
    Public Property Password As String

    <DataType(DataType.Password)>
    <Display(Name:="acc_confirm", ResourceType:=GetType(Resource))>
    <Compare("Password", ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="acc_confirmError")>
    Public Property ConfirmPassword As String


End Class

Public Class SelectUserRolesViewModel
    Public Sub New()
        Me.Roles = New List(Of SelectRoleEditorViewModel)()
    End Sub


    ' Enable initialization with an instance of ApplicationUser:
    Public Sub New(user As ApplicationUser)
        Me.New()
        Me.UserName = user.UserName
        Me.Id = user.Id
        Me.NOMS = user.NOMS
        Me.PRENOMS = user.PRENOMS
        Me.TEL = user.TEL
        Me.EMAIL = user.EMAIL
        Me.ID_SERVICE = user.ID_SERVICE

        Dim Db = New ApplicationDbContext()

        ' Add all available roles to the list of EditorViewModels:
        Dim allRoles = Db.Roles
        For Each role As IdentityRole In allRoles
            ' An EditorViewModel will be used by Editor Template:
            Dim rvm = New SelectRoleEditorViewModel(role)
            Me.Roles.Add(rvm)
        Next

        ' Set the Selected property to true for those roles for 
        ' which the current user is a member:
        For Each userRole As IdentityUserRole In user.Roles
            Dim checkUserRole = Me.Roles.Find(Function(r) r.RoleName = userRole.Role.Name)
            checkUserRole.Selected = True
        Next
    End Sub

    Public Property ID_SERVICE As Long
    Public Property NOMS As String
    Public Property PRENOMS As String
    Public Property TEL As String
    Public Property EMAIL As String

    Public Property Roles() As List(Of SelectRoleEditorViewModel)
        Get
            Return m_Roles
        End Get
        Set(value As List(Of SelectRoleEditorViewModel))
            m_Roles = value
        End Set
    End Property
    Private m_Roles As List(Of SelectRoleEditorViewModel)

    Property UserName As String

    Property Id As String

End Class

Public Class SelectRoleEditorViewModel
    Public Sub New()
    End Sub
    Public Sub New(role As IdentityRole)
        Me.RoleName = role.Name
    End Sub

    Public Property Selected() As Boolean
        Get
            Return m_Selected
        End Get
        Set(value As Boolean)
            m_Selected = value
        End Set
    End Property
    Private m_Selected As Boolean

    <Required(ErrorMessageResourceType:=GetType(Resource), ErrorMessageResourceName:="FieldRequired")> _
    Public Property RoleName() As String
        Get
            Return m_RoleName
        End Get
        Set(value As String)
            m_RoleName = value
        End Set
    End Property
    Private m_RoleName As String
End Class