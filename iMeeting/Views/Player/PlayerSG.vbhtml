@ModelType PlanningHebdoPlayer
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
    Layout = Nothing
    Dim iPage = 0
End Code
<!doctype html>
<html>
<head>
    <meta http-equiv="Cache-Control" content="no-store" />
    <meta name="viewport" content="width=device-width" />
    <title>Player</title>
    <link rel="stylesheet" type="text/css" href="@Url.Content("~/Content/bootstrap.min.css")" />
    <link rel="stylesheet" type="text/css" href="@Url.Content("~/Content/Player.css")" />

    <script type="text/javascript" src="@Url.Content("~/Scripts/bootstrap.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/jquery-1.10.2.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.shuffle.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.tile.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.flip.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.scrollVert.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.cycle2.scrollUp.js")"></script>

</head>
<body>
    <div id="container">
        @If Model.Views.Count <= 0 Then
             @<h1>@ConfigurationManager.AppSettings("NoDataText")</h1>
        End If
        <div id="output"></div>
        <div class="cycle-slideshow" id="slideshow"
             data-cycle-slides="> div"
             data-cycle-loop="1"
             data-cycle-auto-height="container">
            @Code
                Dim i = 0
                Dim prev_date As Date

            End Code

            @* Ce div est capital, il permet d'annuler l'effet de rafraichissement de la page *@
            <div class="FirstDiv" style="display: block;" data-cycle-timeout="1000" data-cycle-fx="scrollVert" data-cycle-speed="2000">
                <img src="~/Ecran/bg.jpg" />
            </div>
            @For Each vw In Model.Views
                @<div class="@vw.DivClassName" data-cycle-timeout="@vw.ViewShowTime" data-cycle-fx="@vw.Animation" data-cycle-speed="2000">

                    <label class="col-md-12 titre1"><b> @vw.LibelleVue </b></label>
                    @If vw.ViewType = EnumViewType.Hebdomadaire Then
                        @<label class="col-md-12 titre2"><b> @vw.LibelleLieu </b></label>
                    End If
                    <label class="col-md-12 titre3"><b> @vw.LibellePeriode</b></label>
                    @Code
                    prev_date = New Date
                    Dim innerSlideShow = vw.Pages.Count > 1
                    End Code
                    @If innerSlideShow Then
                        @<div class="cycle-pager" id='pager@(vw.Id)'></div>
                        @<div class="cycle-slideshow inner-slideshow"
                              data-cycle-fx="scrollVertUp"
                              data-cycle-slides="> div"
                              data-cycle-pager="#pager@(vw.Id)"
                              data-cycle-auto-height="container"
                              data-cycle-loop="1">
                            @For Each pg In vw.Pages
                            prev_date = New Date
                            i = 0
                                @<div data-cycle-timeout="@pg.PageShowTime">
                                    <div class="table_player">
                                        <div class="row_table_player header">
                                            @If vw.ViewType = EnumViewType.Hebdomadaire Then
                                                @<div class="cell_table_player col_player_1">DATE</div>
                                            Else
                                                @<div class="cell_table_player col_player_1_lieu">LIEU</div>
                                            End If
                                            <div class="cell_table_player col_player_2">HORAIRE</div>
                                            <div class="cell_table_player col_player_normal">OBJET</div>
                                            <div class="cell_table_player col_player_3">PRESIDEE PAR</div>
                                            <div class="cell_table_player deco_col_header"></div>
                                        </div>
                                        @For Each item In pg.Reservations
                                            @<div class="row_table_player @IIf((i Mod 2) = 0, "normal_row", "alternate_row")">
                                                @If vw.ViewType = EnumViewType.Hebdomadaire Then
                                                    @<div class="cell_table_player normal_small">
                                                        @If prev_date <> item.DATE_DEBUT.Date Then
                                                            @item.DATE_DEBUT.ToString("ddd dd/MM")
                                                        End If
                                                    </div>
                                                Else
                                                    @<div class="cell_table_player normal_small">
                                                        @item.LIEUX.LIBELLE
                                                    </div>
                                                End If

                                                <div class="cell_table_player normal_small">
                                                    @item.HEURE_DEBUT.ToString("HH:mm") - @item.HEURE_FIN.ToString("HH:mm")
                                                </div>
                                                <div class="cell_table_player normal">
                                                    @Html.DisplayFor(Function(modelItem) item.Objet)
                                                    @Select Case item.CurrentState
                                                    Case EnumCurrentState.Canceled
                                                        @:(<span class="canceled">Annulé</span>)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Case EnumCurrentState.Reported
                                                        @:(<span class="reported">Reporté</span>)
                        End Select
                                                </div>
                                                <div class="cell_table_player normal_medium">
                                                    @Html.DisplayFor(Function(modelItem) item.PresidentSeance)
                                                </div>
                                                <div class="cell_table_player deco_col"></div>
                                            </div>
                                        i = i + 1
                                        prev_date = item.DATE_DEBUT.Date
                                        Next
                                    </div>

                                </div>
                            Next
                        </div>
                    ElseIf vw.Pages.Count > 0 Then
                    Dim pg = vw.Pages(0)
                    i = 0
                        @<div class="table_player">
                                        <div class="row_table_player header">
                                            @If vw.ViewType = EnumViewType.Hebdomadaire Then
                                                @<div class="cell_table_player col_player_1">DATE</div>
                    Else
                                                @<div class="cell_table_player col_player_1_lieu">LIEU</div>
                    End If
                                            <div class="cell_table_player col_player_2">HORAIRE</div>
                                            <div class="cell_table_player col_player_normal">OBJET</div>
                                            <div class="cell_table_player col_player_3">PRESIDEE PAR</div>
                                            <div class="cell_table_player deco_col_header"></div>
                                        </div>
                                        @For Each item In pg.Reservations
                                            @<div class="row_table_player @IIf((i Mod 2) = 0, "normal_row", "alternate_row")">
                                                @If vw.ViewType = EnumViewType.Hebdomadaire Then
                                                    @<div class="cell_table_player normal_small">
                                                        @If prev_date <> item.DATE_DEBUT.Date Then
                                                            @item.DATE_DEBUT.ToString("ddd dd/MM")
                                                        End If
                                                    </div>
                                                Else
                                                    @<div class="cell_table_player normal_small">
                                                        @item.LIEUX.LIBELLE
                                                    </div>
                                                End If

                                                <div class="cell_table_player normal_small">
                                                    @item.HEURE_DEBUT.ToString("HH:mm") - @item.HEURE_FIN.ToString("HH:mm")
                                                </div>
                                                <div class="cell_table_player normal">
                                                    @Html.DisplayFor(Function(modelItem) item.Objet)
                                                    @Select Case item.CurrentState
                                                    Case EnumCurrentState.Canceled
                                                        @:(<span class="canceled">Annulé</span>)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Case EnumCurrentState.Reported
                                                        @:(<span class="reported">Reporté</span>)
                        End Select
                                                </div>
                                                <div class="cell_table_player normal_medium">
                                                    @Html.DisplayFor(Function(modelItem) item.PresidentSeance)
                                                </div>
                                                <div class="cell_table_player deco_col"></div>
                                            </div>
                    i = i + 1
                    prev_date = item.DATE_DEBUT.Date
                    Next
                                    </div>

                    End If

                </div>
            Next

        </div>

    </div>
    @* Rien ne se passe s'il n'y a rien.
        En fait la page se charge via le méta généré *@
    @If Model.Views.Count > 0 Then
        @<script>
        (function () {
            "use strict";

            function reload() {
                //alert('test reload !');

                $.ajax({
                    url: "",
                    context: document.body,
                    success: function (s, x) {
                        $(this).html(s);
                        //alert('Est-ce qu\'on peut recommencer ?');
                        //$('#slideshow').cycle('destroy').cycle();
                    },

                    error: function (resultat, statut, erreur) {
                        // réessayer au prochain cycle
                        $('#slideshow').cycle('destroy').cycle();
                    }
                });
            }

            $('#slideshow').on('cycle-finished', function (e) {
                // ignore bubbled events from inner slideshows
                if (e.target !== this) {
                    return;
                }

                // location.reload();
                reload();
            });

        })();

        var outer = $('#slideshow');
        var inners = $('.inner-slideshow');
        var output = $('#output');

        // On stoppe tous les inners cycles
        inners.cycle('stop');

        $('#slideshow').on('cycle-before', function (e, opts, curr, next) {
            // ignore bubbled events from inner slideshows
            if (e.target !== this)
                return;

            inners.cycle('stop');

            //var index = opts.slides.index(next);
            //output.html('starting slideshow #' + (index + 1));

            // start the next slideshow
            $(next).find('.inner-slideshow').cycle('destroy').cycle();
        });
    </script>
    Else
        ' Aucune donnée disponible
        @<script>
            function reload() {
                //alert('test reload no data !');

                $.ajax({
                    url: "",
                    context: document.body,
                    success: function (s, x) {
                        $(this).html(s);
                        //alert('Est-ce qu\'on peut recommencer ?');
                        //$('#slideshow').cycle('destroy').cycle();
                    },

                    error: function (resultat, statut, erreur) {
                        // réessayer au prochain cycle
                        window.setTimeout(reload, 60000);
                    }
                });
            };

            window.setTimeout(reload, 60000);
    </script>
    End If

</body>
</html>
