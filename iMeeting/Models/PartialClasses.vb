Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Data.Entity.Validation

'<MetadataType(GetType(ParticipantMetadata))> _
'Partial Public Class PARTICIPANT

'End Class

'<MetadataType(GetType(ConvocationMetadata))> _
'Partial Public Class CONVOCATION

'End Class

'<MetadataType(GetType(PtAffichageMetadata))> _
'Partial Public Class PT_AFFICHAGE

'End Class

'<MetadataType(GetType(ServiceMetadata))> _
'Partial Public Class SERVICE

'End Class

'<MetadataType(GetType(LieuxMetadata))> _
'Partial Public Class LIEUX

'End Class

'<MetadataType(GetType(BatimentMetadata))> _
'Partial Public Class BATIMENT

'End Class

'<MetadataType(GetType(ReservationMetadata))> _
'Partial Public Class RESERVATION

'End Class


'Partial Public Class iMeetingEntities
'    Inherits DbContext

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

'End Class

