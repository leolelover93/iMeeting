Public Class Util

    ''' <summary>
    ''' The connection string property that pulls from the web.config
    ''' </summary>
    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        End Get
    End Property

    Shared Sub getError(cex As Exception, modelState As ModelStateDictionary)
        If TypeOf (cex) Is Entity.Validation.DbEntityValidationException Then
            Dim ex = CType(cex, Entity.Validation.DbEntityValidationException)
            Dim strError As String = ""
            For Each val_errors In ex.EntityValidationErrors
                For Each val_error In val_errors.ValidationErrors
                    modelState.AddModelError(val_error.PropertyName, val_error.ErrorMessage)
                    strError &= val_error.ErrorMessage & vbCrLf
                Next
            Next
            modelState.AddModelError("", strError)
        ElseIf TypeOf (cex) Is Exception Then
            Dim ex = cex
            Dim strError As String = ""
            Dim ie = ex
            While ie IsNot Nothing
                If Not ie.Message.StartsWith("Une erreur") Then
                    strError &= ie.Message & "|"
                End If

                ie = ie.InnerException
            End While
            Dim ar_errors = strError.Split("|".ToCharArray)
            For Each val_error In ar_errors
                modelState.AddModelError("", val_error)
            Next
        End If
    End Sub

    Shared Function getError(cex As Exception) As String
        Dim strError As String = ""
        If TypeOf (cex) Is Entity.Validation.DbEntityValidationException Then
            Dim ex = CType(cex, Entity.Validation.DbEntityValidationException)
            For Each val_errors In ex.EntityValidationErrors
                For Each val_error In val_errors.ValidationErrors
                    strError &= val_error.ErrorMessage & vbCrLf
                Next
            Next

        ElseIf TypeOf (cex) Is Exception Then
            Dim ex = cex
            Dim ie = ex
            While ie IsNot Nothing
                If Not ie.Message.StartsWith("Une erreur") Then
                    strError &= ie.Message & "|"
                End If

                ie = ie.InnerException
            End While
        End If

        Return strError
    End Function
End Class