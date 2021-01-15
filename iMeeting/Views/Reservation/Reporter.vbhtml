@ModelType RESERVATION
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.rese_reportTitle
End Code

<h2>@Resource.rese_reportTitle</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        @Html.ValidationSummary(True)
        @Html.HiddenFor(Function(model) model.ID_RESERVATION)
        @Html.HiddenFor(Function(model) model.UserId)
        @Html.HiddenFor(Function(model) model.DATE_CREATION)
        @Html.HiddenFor(Function(model) model.ETAT)
        @Html.HiddenFor(Function(model) model.CurrentState)

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li class="active"><a href="#prof" role="tab" data-toggle="tab">Informations</a></li>
            <li><a href="#parti" role="tab" data-toggle="tab">Participants</a></li>
            <li><a href="#other" role="tab" data-toggle="tab">Autres</a></li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div class="tab-pane active" id="prof">
                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.ID_LIEU, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.DropDownListFor(Function(m) m.ID_LIEU, DirectCast(ViewBag.LIEUX, SelectList), New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.ID_LIEU)
                    </div>
                </div>

                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.Objet, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Objet)
                        @Html.ValidationMessageFor(Function(model) model.Objet)
                    </div>
                </div>


                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.DATE_DEBUT, New With {.class = "control-label col-md-2"})
                    <div class="col-md-2">
                        @Html.EditorFor(Function(model) model.DATE_DEBUT)
                        @Html.HiddenFor(Function(model) model.DATE_FIN)
                        @Html.ValidationMessageFor(Function(model) model.DATE_DEBUT)
                    </div>
                </div>

                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.HEURE_DEBUT, Resource.rese_Plage, New With {.class = "control-label col-md-2"})
                    <div class="col-md-2">
                        @Html.EditorFor(Function(model) model.HEURE_DEBUT)
                        @Html.ValidationMessageFor(Function(model) model.HEURE_DEBUT)
                    </div>
                    <div class="col-md-2">
                        @Html.EditorFor(Function(model) model.HEURE_FIN)
                        @Html.ValidationMessageFor(Function(model) model.HEURE_FIN)
                    </div>
                </div>

                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.PresidentSeance, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.PresidentSeance)
                        @Html.ValidationMessageFor(Function(model) model.PresidentSeance)
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.TelPresiSce, New With {.class = "control-label col-md-2"})
                    <div class="col-md-2">
                        @Html.EditorFor(Function(model) model.TelPresiSce)
                        @Html.ValidationMessageFor(Function(model) model.TelPresiSce)
                    </div>
                </div>

                <div class="form-group required-field">
                    @Html.LabelFor(Function(model) model.ThemeId, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.DropDownListFor(Function(m) m.ThemeId, DirectCast(ViewBag.Themes, SelectList), Resource.rese_select_theme, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.ThemeId)
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.NBRE_PERS, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.NBRE_PERS)
                        @Html.ValidationMessageFor(Function(model) model.NBRE_PERS)
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="parti">

            </div>
            <div class="tab-pane" id="other">

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.DETAILS, New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.DETAILS)
                        @Html.ValidationMessageFor(Function(model) model.DETAILS)
                    </div>
                </div>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="@Resource.rese_report" class="btn btn-primary btn-sm" />
                @Html.ActionLink(Resource.BackToList, "Calendar", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
