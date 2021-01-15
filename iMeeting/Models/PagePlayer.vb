''' <summary>
''' Représente une page de réservations dans une vue
''' </summary>
''' <remarks></remarks>
Public Class PagePlayer
    Public Property Id As Integer

    Public Property PageChars As Integer = 0
    Public Property PageShowTime As Integer = 5000 ' 5 Sec mais modifiée par PlanningHebdoPlayer

    Public Property Reservations As New List(Of RESERVATION)
End Class
