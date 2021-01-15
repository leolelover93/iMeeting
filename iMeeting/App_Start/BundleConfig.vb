Imports System.Web.Optimization

Public Module BundleConfig
    ' Pour plus d’informations sur le regroupement, rendez-vous sur http://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery-ui.js",
                    "~/Scripts/i18n/jquery-ui-i18n.js",
                    "~/Scripts/jquery.placeholder.js",
                    "~/Scripts/jquery.globalize/globalize.js",
                    "~/Scripts/jquery.globalize/cultures/globalize.culture.fr-FR.js",
                    "~/Scripts/DatePickerReady.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/GlobalizeReady.js"))

        bundles.Add(New ScriptBundle("~/bundles/player").Include(
                    "~/Scripts/player/jquery.cycle2.js"))

        ' Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
        ' prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/bootstrap.js",
                  "~/Scripts/respond.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap-theme.css",
                  "~/Content/font-awesome-4.2.0/css/font-awesome.min.css",
                  "~/Content/jquery-ui.css",
                  "~/Content/site.css"))

        ' Pour les réservations
        bundles.Add(New ScriptBundle("~/bundles/calendar").Include(
                  "~/Scripts/Calendar/lib/moment.min.js",
                  "~/Scripts/Calendar/fullcalendar.js",
                  "~/Scripts/Calendar/lang-all.js",
                  "~/Scripts/jquery.bootstrap.wizard.js",
                  "~/Scripts/knockout-3.1.0.js"))

        bundles.Add(New StyleBundle("~/Content/css/calendar").Include(
                  "~/Scripts/Calendar/lib/cupertino/jquery-ui.min.css",
                  "~/Content/fullcalendar.css"))

        ' Pour les graphs
        bundles.Add(New ScriptBundle("~/bundles/graph").Include(
                 "~/Scripts/d3.v3.js",
                 "~/Scripts/nv.d3.js",
                 "~/Scripts/tooltip.js",
                 "~/Scripts/utils.js",
                 "~/Scripts/models/axis.js",
                 "~/Scripts/models/discreteBar.js",
                 "~/Scripts/models/discreteBarChart.js",
                 "~/Scripts/models/pieChart.js",
                 "~/Scripts/models/pie.js",
                 "~/Scripts/models/linePlusBarChart.js",
                 "~/Scripts/models/line.js",
                 "~/Scripts/models/scatter.js"))

        bundles.Add(New StyleBundle("~/Content/css/graph").Include(
                  "~/Content/nv.d3.css"))
    End Sub
End Module

