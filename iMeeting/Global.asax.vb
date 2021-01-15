Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        ' For localizing default error messages in ASP.NET MVC
        ClientDataTypeModelValidatorProvider.ResourceClassKey = "Resource"
        DefaultModelBinder.ResourceClassKey = "Resource"

    End Sub
End Class

