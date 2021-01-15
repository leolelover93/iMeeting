Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.ComponentModel

' resource: html-color-codes  http://html-color-codes.info/
' left part stores color, right part stores className for html rendering
Public Enum EnumReservation
    ' orange
    <Description("#FF8000:ENQUIRY")> _
    Initial = 0
    ' Gray Cloud
    <Description("#B6B6B4:BOOKED")> _
    Passed = 1
    ' green
    <Description("#01DF3A:CONFIRMED")> _
    Confirmed = 2

End Enum

Public Enum EnumCurrentState
    ' green
    <Description("#01DF3A:CONFIRMED")> _
    Initial = 0
    <Description("#4B0082:CONFIRMED")> _
    EnCours = 3
    <Description("#FF0000:BOOKED")> _
    EnRetard = 4 ' Automatique
    <Description("#0000FF:CONFIRMED")> _
    Terminee = 5
    <Description("#0000FF:CONFIRMED")> _
    CanceledPending = 10
    <Description("#7F9B71:CONFIRMED")> _
    Canceled = 11
    <Description("#9C7280:CONFIRMED")> _
    ReportedPending = 20
    <Description("#254117:CONFIRMED")> _
    Reported = 21
End Enum

Public NotInheritable Class Enums
    Private Sub New()
    End Sub
    ''' Get all values
    Public Shared Function GetValues(Of T)() As IEnumerable(Of T)
        Return [Enum].GetValues(GetType(T)).Cast(Of T)()
    End Function

    ''' Get all the names
    Public Shared Function GetNames(Of T)() As IEnumerable(Of T)
        Return [Enum].GetNames(GetType(T)).Cast(Of T)()
    End Function

    ''' Get the name for the enum value
    Public Shared Function GetName(Of T)(enumValue As T) As String
        Return [Enum].GetName(GetType(T), enumValue)
    End Function

    ''' Get the underlying value for the Enum string
    Public Shared Function GetValue(Of T)(enumString As String) As Integer
        Return CInt([Enum].Parse(GetType(T), enumString.Trim()))
    End Function

    Public Shared Function GetEnumDescription(Of T)(value As String) As String
        Dim type As Type = GetType(T)
        Dim name = [Enum].GetNames(type).Where(Function(f) f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).[Select](Function(d) d).FirstOrDefault()

        If name Is Nothing Then
            Return String.Empty
        End If
        Dim field = type.GetField(name)
        Dim customAttribute = field.GetCustomAttributes(GetType(DescriptionAttribute), False)
        Return If(customAttribute.Length > 0, DirectCast(customAttribute(0), DescriptionAttribute).Description, name)
    End Function
End Class