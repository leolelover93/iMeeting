Imports System.Data.Entity
Imports Microsoft.AspNet.Identity.EntityFramework

Public Class ApplicationDbContext
    Inherits IdentityDbContext(Of ApplicationUser)

    Public Sub New()
        MyBase.New("DefaultConnection")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        MyBase.OnModelCreating(modelBuilder)
        modelBuilder.Configurations.Add(New BATIMENTCfg())
        modelBuilder.Configurations.Add(New BUREAUCfg())
        modelBuilder.Configurations.Add(New PieceJointeCfg())
        modelBuilder.Configurations.Add(New LIEUXCfg())
        modelBuilder.Configurations.Add(New PARTICIPANTCfg())
        modelBuilder.Configurations.Add(New PT_AFFICHAGECfg())
        modelBuilder.Configurations.Add(New RESERVATIONCfg())
        modelBuilder.Configurations.Add(New SERVICECfg())
        modelBuilder.Configurations.Add(New ThemeCfg())
        modelBuilder.Configurations.Add(New TexteDefilantCfg())
        modelBuilder.Configurations.Add(New PlanningSettingsCfg())
    End Sub

    '    Public Overloads Function SaveChanges(modelStateDictionary As ModelStateDictionary) As Integer
    '        Dim result = -1
    '        Try
    '            result = MyBase.SaveChanges()
    '        Catch ex As DbEntityValidationException

    '            For Each a In ex.EntityValidationErrors
    '                For Each b In a.ValidationErrors
    '                    Dim st1 As String = b.PropertyName
    '                    Dim st2 As String = b.ErrorMessage

    '                    modelStateDictionary.AddModelError(st1, st2)
    '                Next
    '            Next

    '            Throw New Exception()
    '        End Try
    '        Return result
    '    End Function

    Public Overridable Property BATIMENT() As DbSet(Of BATIMENT)
    Public Overridable Property BUREAU() As DbSet(Of BUREAU)
    Public Overridable Property PARTICIPANT() As DbSet(Of PARTICIPANT)
    Public Overridable Property PT_AFFICHAGE() As DbSet(Of PT_AFFICHAGE)
    Public Overridable Property SERVICE() As DbSet(Of SERVICE)
    Public Overridable Property LIEUX() As DbSet(Of LIEUX)
    Public Overridable Property PiecesJointes() As DbSet(Of PieceJointe)
    Public Overridable Property RESERVATION() As DbSet(Of RESERVATION)
    Public Overridable Property Theme() As DbSet(Of Theme)
    Public Overridable Property TexteDefilants() As DbSet(Of TexteDefilant)
    Public Overridable Property PlanningSettings() As DbSet(Of PlanningSettings)
End Class
