@Imports Microsoft.AspNet.Identity

@Code
   
@If Request.IsAuthenticated
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With { .id = "logoutForm", .class = "navbar-right" })
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right">
             <li>
                 <a href="@Url.Action("Index", "Reservation")"><i class="fa fa-bell"></i> <span id="waiting_count" class="badge"></span></a>
             </li>
             <!-- /.dropdown -->
             <li class="dropdown">
                 <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                     <span class="glyphicon glyphicon-user"></span> @User.Identity.GetUserName() <i class="fa fa-caret-down"></i>
                 </a>
                 <ul class="dropdown-menu dropdown-user">
                     <li><a href="@Url.Action("Manage", "Account")" title="Gérer"><i class="fa fa-gear fa-fw"></i> Changer mon mot de passe</a></li>
                     <li class="divider"></li>
                     <li><a href="javascript:document.getElementById('logoutForm').submit()"><span class="glyphicon glyphicon-log-out"></span> Déconnexion</a></li>
                 </ul>
             </li>
             <!-- /.dropdown-user -->
        
             <!-- /.dropdown -->
             <li class="dropdown">
                 <a class="dropdown-toggle" data-toggle="dropdown" href="">
                     @If ViewContext.RouteData.Values("culture") = "en" Then
                         @:<img src="@Url.Content("~/Content/Images/en.png")" /> English <i class="fa fa-caret-down"></i>
                                                  Else
                         @:<img src="@Url.Content("~/Content/Images/fr.png")" /> Français <i class="fa fa-caret-down"></i>
                         End If
                 </a>
                 <ul class="dropdown-menu dropdown-user">
                     <li><a href="@Url.RouteUrl(New RouteValueDictionary(ViewContext.RouteData.Values.ToDictionary(Function(r) r.Key, Function(r) IIf(r.Key = "culture", "fr-FR", r.Value))))"><img src="~/Content/Images/fr.png " /> Français</a></li>
                     <li><a href="@Url.RouteUrl(New RouteValueDictionary(ViewContext.RouteData.Values.ToDictionary(Function(r) r.Key, Function(r) IIf(r.Key = "culture", "en", r.Value))))"><img src="~/Content/Images/en.png " /> English</a></li>
                 </ul>
             </li>
             <!-- /.dropdown-langue -->
             <li><a href="javascript:document.getElementById('logoutForm').submit()" style="padding:7px 15px;"><span class="glyphicon glyphicon-off gi-2x"></span></a></li>
</ul>
        
        
    End Using
    
    
Else
   @<ul class="nav navbar-nav navbar-right">
        <li><a href="@Url.Action("Login", "Account", routeValues:=Nothing)" id="loginLink"><span class=" glyphicon glyphicon-user"></span> Se connecter</a></li>
        <li class="dropdown">
            <a class="dropdown-toggle" data-toggle="dropdown" href="">
                @If ViewContext.RouteData.Values("culture") = "en" Then
                    @:<img src="@Url.Content("~/Content/Images/en.png")" /> English <i class="fa fa-caret-down"></i>
                                                                      Else
                    @:<img src="@Url.Content("~/Content/Images/fr.png")" /> Français <i class="fa fa-caret-down"></i>
                         End If
            </a>
            <ul class="dropdown-menu dropdown-user">
                <li><a href="@Url.RouteUrl(New RouteValueDictionary(ViewContext.RouteData.Values.ToDictionary(Function(r) r.Key, Function(r) IIf(r.Key = "culture", "fr-FR", r.Value))))"><img src="~/Content/Images/fr.png " /> Français</a></li>
                <li><a href="@Url.RouteUrl(New RouteValueDictionary(ViewContext.RouteData.Values.ToDictionary(Function(r) r.Key, Function(r) IIf(r.Key = "culture", "en", r.Value))))"><img src="~/Content/Images/en.png " /> English</a></li>
            </ul>
        </li>
        <!-- /.dropdown-langue -->
</ul>
End If


End code
