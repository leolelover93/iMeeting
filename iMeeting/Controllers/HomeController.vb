Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return RedirectToAction("Calendar", "Reservation")
        'Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function

    'Public Function SetCulture(culture As String) As ActionResult
    '    ' Validate input
    '    culture = CultureHelper.GetImplementedCulture(culture)
    '    ' Save culture in a cookie
    '    Dim cookie As HttpCookie = Request.Cookies("_culture")
    '    If cookie IsNot Nothing Then
    '        cookie.Value = culture
    '    Else
    '        ' update cookie value
    '        cookie = New HttpCookie("_culture")
    '        cookie.Value = culture
    '        cookie.Expires = DateTime.Now.AddYears(1)
    '    End If
    '    Response.Cookies.Add(cookie)
    '    Return RedirectToAction("Index")
    'End Function

    Public Function SetCulture(culture As String) As ActionResult
        ' Validate input
        culture = CultureHelper.GetImplementedCulture(culture)
        RouteData.Values("culture") = culture
        ' set culture

        Return RedirectToAction("Index")
    End Function
End Class
