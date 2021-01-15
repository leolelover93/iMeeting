@Imports iMeeting.My.Resources
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - @Resource.app_name</title>
    @Styles.Render("~/Content/css")
        @Scripts.Render("~/bundles/modernizr")
    <link href="@Url.Content("~/Content/font-awesome-4.2.0/css/font-awesome.min.css")" rel="stylesheet" />
    @RenderSection("css", required:=False)
</head>



<body>
    <div class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink(Resource.app_name, "Calendar", "Reservation", Nothing, New With {.[class] = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                @*<ul class="nav navbar-nav">
                    <li>@Html.ActionLink("À propos de", "About", "Home")</li>
                    <li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                </ul>*@
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="container-fluid body-content">
        <div class="row">
            <div class="col-sm-3 col-md-2">
                @*Le menu ici*@
                <img class="img-rounded center-block" src="~/Content/Images/SPM.png " />
                @Html.Partial("_MenuVertical")
            </div>
            <div class="col-sm-9 col-md-10 main">
                @RenderBody()
            </div>
        </div>
                
  </div>
    <nav class="navbar navbar-inverse navbar-fixed-bottom" role="navigation">
        <div class="container-fluid">
            <p class="navbar-text col-md-9">&copy; @DateTime.Now.Year - @Resource.app_copyrigth</p>
            <p class="navbar-text text-right">Powered by TEAM Information System</p>
        </div>
    </nav>
    <script>
        @Code
            Dim Culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant()
            If Culture = "fr-fr" Then
                Culture = "fr"
            Else
                Culture = ""
            End If
            @:var Culture = "@Culture";
        End Code
    </script>
            @Scripts.Render("~/bundles/jquery")
            @Scripts.Render("~/bundles/bootstrap")
            @RenderSection("scripts", required:=False)

    <script type="text/javascript">
        (function poll() {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: '@Url.Action("RequiredActionCount", "Reservation")',
                success: function (data) {
                    if (data > 0) {
                        $('#waiting_count, #waiting_count2')
                            .html(data)
                            .css("background-color", "orange");
                    }
                    else {
                        $('#waiting_count, #waiting_count2').html(data).animate({ 'background-color': "auto" }, 1200);
                    }
                },
                complete: setTimeout(poll, 50000) // 50 sec
            });
        })();
    </script>
</body>
</html>
