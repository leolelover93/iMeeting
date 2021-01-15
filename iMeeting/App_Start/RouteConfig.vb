Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.IgnoreRoute("{resource}.aspx/{*pathInfo}")

        routes.MapRoute(
            name:="Default",
            url:="{culture}/{controller}/{action}/{id}",
            defaults:=New With {.culture = CultureHelper.GetDefaultCulture(), .controller = "Reservation", .action = "Calendar", .id = UrlParameter.Optional}
        )
    End Sub
End Module