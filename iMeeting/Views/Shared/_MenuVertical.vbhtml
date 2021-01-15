@Imports iMeeting.My.Resources

@Code
    Dim activeMenu As String = ViewBag.activeMenu
    If String.IsNullOrEmpty(activeMenu) Then
        activeMenu = "param"
    End If
End Code

@Helper selectedMenu(menu As String, activeMenu As String)
    If activeMenu.StartsWith(menu) Then
    @:in
    End If
End Helper

@Helper selectedItem(menu As String, activeMenu As String)
    If menu = activeMenu Then
    @:active
    End If
End Helper

@If Request.IsAuthenticated = True Then

                @<div class="panel-group sidebar" id="accordion">
                     <div class="list-group panel panel-primary">
                         @If User.IsInRole("Administrateur") Then
                         @<a id="titre" href="#menuparam" class="list-group-item panel-heading" data-toggle="collapse" data-parent="#MainMenu">
                             <i class="fa fa-gears"></i> @Resource.menu_param <i class="fa fa-caret-down"></i>
                         </a>
                         @<div class="collapse @selectedMenu("param", activeMenu)" id="menuparam">
                             <a href="@Url.Action("Index", "Batiment")" class="list-group-item @selectedItem("param-1", activeMenu)">@Resource.menu_batiment</a>
                             <a href="@Url.Action("Index", "Salle")" class="list-group-item @selectedItem("param-2", activeMenu)">@Resource.menu_salles</a>
                             <a href="@Url.Action("Index", "Espace")" class="list-group-item @selectedItem("param-3", activeMenu)">@Resource.menu_espaces</a>
                             <a href="@Url.Action("Index", "Service")" class="list-group-item @selectedItem("param-4", activeMenu)">@Resource.menu_service</a>
                             <a href="@Url.Action("Index", "BUREAU")" class="list-group-item @selectedItem("param-8", activeMenu)">@Resource.menu_BUREAU</a>
                             <a href="@Url.Action("Index", "Affichage")" class="list-group-item @selectedItem("param-5", activeMenu)">@Resource.menu_pt_affich</a>
                             <a href="@Url.Action("Index", "Themes")" class="list-group-item @selectedItem("param-7", activeMenu)">@Resource.menu_Themes</a>
                             <a href="@Url.Action("Index", "Account")" class="list-group-item @selectedItem("param-6", activeMenu)">@Resource.menu_users</a>
                         </div>
                         End If
                         @If User.IsInRole("Utilisateur") Then
                         @<a id="titre" href="#Misencause" class="list-group-item  panel-heading" data-toggle="collapse" data-parent="#MainMenu">
                             <i class="glyphicon glyphicon-export"></i> @Resource.menu_operation <i class="fa fa-caret-down"></i>
                         </a>
                         @<div class="collapse @selectedMenu("oper", activeMenu)" id="Misencause">
                             <a href="@Url.Action("Calendar", "Reservation")" class="list-group-item @selectedItem("oper-1", activeMenu)">@Resource.menu_reserv</a>
                             <a href="@Url.Action("Index", "Reservation")" class="list-group-item @selectedItem("oper-2", activeMenu)">@Resource.menu_reserv_export <span id="waiting_count2" class="badge"></span></a>
                             @*<a href="@Url.Action("Index", "Participant")" class="list-group-item @selectedItem("oper-3", activeMenu)">@Resource.menu_participant</a>*@
                         </div>
                         End If
                         @If User.IsInRole("SCC") Then
                                 @<a id="titre" href="#Players" class="list-group-item  panel-heading" data-toggle="collapse" data-parent="#MainMenu">
    <i class="glyphicon glyphicon-film"></i> Points d'affichage <i class="fa fa-caret-down"></i>
</a>
                         @<div class="collapse @selectedMenu("play", activeMenu)" id="Players">
                              <a href="@Url.Action("Index", "TexteDefilants")" class="list-group-item @selectedItem("play-1", activeMenu)">@Resource.menu_TexteDefilants</a>
                              <a href="@Url.Action("Index", "PlanningSettings")" class="list-group-item @selectedItem("play-2", activeMenu)">@Resource.menu_PlanningSettings</a>
                         </div>
                         @<a id="titre" href="#Etat" class="list-group-item  panel-heading" data-toggle="collapse" data-parent="#MainMenu">
                         <i class="glyphicon glyphicon-stats"></i> Statistiques <i class="fa fa-caret-down"></i>
                         </a>
                         @<div class="collapse @selectedMenu("stats", activeMenu)" id="Etat">
                             <a href="@Url.Action("Index", "Stats")" class="list-group-item @selectedItem("stats-1", activeMenu)">Statistiques graphiques</a>
                              <a href="#SubMenuInst" class="list-group-item" data-toggle="collapse" data-parent="#SubMenuInst">Réservations <i class="fa fa-caret-down"></i></a>
                             <div class="collapse @selectedMenu("stats-2", activeMenu) list-group-submenu" id="SubMenuInst">
                                 <a href="@Url.Action("Statsparrespo", "Stats")" class="list-group-item @selectedItem("stats-2_1", activeMenu)">par Président de séance</a>
                                 <a href="@Url.Action("Statsparsal", "Stats")" class="list-group-item @selectedItem("stats-2_2", activeMenu)">par salle</a>
                                 <a href="@Url.Action("Statspartheme", "Stats")" class="list-group-item @selectedItem("stats-2_3", activeMenu)">par thème</a>
                             </div>
                          </div>
                         End If
                         <a id="titre" href="#Help" class="list-group-item  panel-heading" data-toggle="collapse" data-parent="#MainMenu"><i class="glyphicon glyphicon-question-sign"></i> Aide <i class="fa fa-caret-down"></i></a>
                         <div class="collapse @selectedMenu("hlp", activeMenu)" id="Help">
                             <a href="@Url.Content("~/docs/UserGuide.pdf")" class="list-group-item @selectedItem("hlp-1", activeMenu)" target="_blank">Guide Utilisateur</a>
                         </div>
                     </div>
                </div>
Else
    
End If
