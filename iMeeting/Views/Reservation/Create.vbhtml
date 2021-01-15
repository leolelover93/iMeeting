@ModelType ReservationViewModel
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.rese_CreateTitle
End Code

<h2>@Resource.rese_CreateTitle</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
    @Html.ValidationSummary(True)

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
                    @Html.DropDownListFor(Function(m) m.ID_LIEU, DirectCast(ViewBag.LIEUX, SelectList), Resource.rese_select_lieu, New With {.class = "form-control"})
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
                <div class="col-md-2">
                    @Html.EditorFor(Function(model) model.NBRE_PERS)
                    @Html.ValidationMessageFor(Function(model) model.NBRE_PERS)
                </div>
            </div>

            
            </div>
        <div class="tab-pane" id="parti">
            @*<pre data-bind="text: ko.toJSON($data, null, 2)"></pre>*@
            <p><span data-bind='text: participants().length'>&nbsp;</span> personne(s) à informer</p>
            <table data-bind='visible: participants().length > 0' class="table table-responsive table-bordered table-striped table-condensed">
                <thead>
                    <tr>
                        <th class="hidden-xs hidden-sm">
                            @Resource.part_Email
                        </th>
                        <th>
                            @Resource.part_Nom
                        </th>
                        <th class="hidden-xs">
                            @Resource.part_Prenom
                        </th>
                        <th class="hidden-xs">
                            @Resource.part_Fonction
                        </th>
                        <th class="hidden-xs hidden-sm">
                            @Resource.part_Telephone
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody data-bind='foreach: participants'>
                    <tr>
                        <td>
                            @*<input class="form-control input-sm" data-val="true" data-val-length="Le champ Email ne peut dépasser 150 caractères !" data-val-length-max="150" id="EMAIL" type="text"
                        data-bind="value: EMAIL, attr: {name: 'Participants[' + $index() + '].EMAIL'}" />*@
                            <input class="form-control input-sm" data-val="true" data-val-length="Le champ Email ne peut dépasser 150 caractères !" data-val-length-max="150" id="EMAIL" type="text"
                                   data-bind="autoComplete: true, value: EMAIL, attr: {name: 'Participants[' + $index() + '].EMAIL'}" />
                            <span class="field-validation-valid" data-bind="attr: {'data-valmsg-for': 'Participants[' + $index() + '].EMAIL'}" data-valmsg-replace="true"></span>
                        </td>
                        <td>
                            <input data-val="true" data-val-number="Le champ ID_PARTICIPANT doit être un nombre." data-val-range="Le champ ID_PARTICIPANT doit être un nombre !" data-val-range-max="9.22337203685478E+18" data-val-range-min="0" data-val-required="Le champ ID_PARTICIPANT est requis!" id="ID_PARTICIPANT" type="hidden"
                                   data-bind="value: ID_PARTICIPANT, attr: {name: 'Participants[' + $index() + '].ID_PARTICIPANT'}" />
                            @*<input class="form-control validate-name-required input-sm" data-val="true" data-val-length="Le champ Noms ne peut dépasser 150 caractères !" data-val-length-max="150" data-val-required="Le champ Noms est requis!" id="NOM" type="text"
                        data-bind="value: NOM, attr: {name: 'Participants[' + $index() + '].NOM'}" />*@
                            <input class="form-control validate-name-required input-sm" data-val="true" data-val-length="Le champ Noms ne peut dépasser 150 caractères !" data-val-length-max="150" data-val-required="Le champ Noms est requis!" id="NOM" type="text"
                                   data-bind="autoComplete: true, value: NOM, attr: {name: 'Participants[' + $index() + '].NOM'}" />
                            <span class="field-validation-valid" data-bind="attr: {'data-valmsg-for': 'Participants[' + $index() + '].NOM'}" data-valmsg-replace="true"></span>
                        </td>
                        <td>
                            <input class="form-control input-sm" data-val="true" data-val-length="Le champ Prenoms ne peut dépasser 150 caractères !" data-val-length-max="150" id="PRENOM" type="text"
                                   data-bind="value: PRENOM, attr: {name: 'Participants[' + $index() + '].PRENOM'}" />
                            <span class="field-validation-valid" data-bind="attr: {'data-valmsg-for': 'Participants[' + $index() + '].PRENOM'}" data-valmsg-replace="true"></span>
                        </td>
                        <td>
                            <input class="form-control input-sm" data-val="true" data-val-length="Le champ Fonction ne peut dépasser 150 caractères !" data-val-length-max="150" id="FONCTION" type="text"
                                   data-bind="value: FONCTION, attr: {name: 'Participants[' + $index() + '].FONCTION'}" />
                            <span class="field-validation-valid" data-bind="attr: {'data-valmsg-for': 'Participants[' + $index() + '].FONCTION'}" data-valmsg-replace="true"></span>
                        </td>
                        <td>
                            <input class="form-control input-sm" data-val="true" data-val-length="Le champ Telephone ne peut dépasser 150 caractères !" data-val-length-max="150" id="TELEPHONE" type="text"
                                   data-bind="value: TELEPHONE, attr: {name: 'Participants[' + $index() + '].TELEPHONE'}" />
                            <span class="field-validation-valid" data-bind="attr: {'data-valmsg-for': 'Participants[' + $index() + '].TELEPHONE'}" data-valmsg-replace="true"></span>
                        </td>
                        <td><a href='#' data-bind='click: $root.removeParticipant'>Supprimer</a></td>
                    </tr>
                </tbody>
            </table>

            <button class="btn btn-primary btn-sm" data-bind='click: addParticipant'>Ajouter participant</button>
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
    <br />
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="@Resource.Save" class="btn btn-primary btn-sm" />
                    @Html.ActionLink(Resource.BackToList, "Calendar", Nothing, New With {.class = "btn btn-default btn-sm"})
                </div>
            </div>
        </div>
        End Using

        <div>
        </div>

        @Section Scripts
@Scripts.Render("~/bundles/calendar")
            @Scripts.Render("~/bundles/jqueryval")
<script>
    $(document).ready(function () {
        $('#PresidentSeance').autocomplete({
            source: function (request, response) {
                var uo = new Array();
                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: '@Url.Action("AutoComplete")',
                    data: { "term": request.term },
                    success: function (data) {
                        for (var i = 0; i < data.length; i++) {
                            uo[i] = data[i];
                        }
                    }
                });
                response(uo);
            },
            minLength: 2
        });
    });
            </script>
@* ViewModel Knockout.js *@
<script>
    function DefineValidationRules() {
        $.validator.addMethod("partName", $.validator.methods.required, "@Resource.part_NomRequired");
        $.validator.addMethod("partNameLong", $.validator.methods.maxlength, "@Resource.part_NomLong");

        $.validator.addClassRules("validate-name-required", { partName: true, partNameLong: 150 });
    }

        var ParticipantModel = function (participants) {
            var self = this;
            self.participants = ko.observableArray(participants);

            self.addParticipant = function () {
                self.participants.push({
                    ID_PARTICIPANT: ko.observable(0),
                    NOM: ko.observable(''),
                    PRENOM: ko.observable(''),
                    FONCTION: ko.observable(''),
                    EMAIL: ko.observable(''),
                    TELEPHONE: ko.observable('')
                });
            };

            self.removeParticipant = function (participant) {
                self.participants.remove(participant);
            };

            self.save = function (form) {
                alert("Could now transmit to server: " + ko.utils.stringifyJson(self.participants));
                // To actually transmit to server as a regular form post, write this: ko.utils.postJson($("form")[0], self.participants);
            };
        };

        var viewModel = new ParticipantModel([
            @If Model IsNot Nothing AndAlso Model.Participants IsNot Nothing Then
                Dim bFirst = True
                For Each item In Model.Participants
                    If bFirst Then
                        @:{ ID_PARTICIPANT: ko.observable(@item.ID_PARTICIPANT), NOM: ko.observable("@item.NOM"), PRENOM: ko.observable("@item.PRENOM"), FONCTION: ko.observable("@item.FONCTION"), EMAIL: ko.observable("@item.EMAIL"), TELEPHONE: ko.observable("@item.TELEPHONE") }
                    Else
                        @:, { ID_PARTICIPANT: ko.observable(@item.ID_PARTICIPANT), NOM: ko.observable("@item.NOM"), PRENOM: ko.observable("@item.PRENOM"), FONCTION: ko.observable("@item.FONCTION"), EMAIL: ko.observable("@item.EMAIL"), TELEPHONE: ko.observable("@item.TELEPHONE") }
                    End If
                    bFirst = False
                Next
            End If
        ]);

    DefineValidationRules();

    ko.bindingHandlers.autoComplete = {
        // Only using init event because the Jquery.UI.AutoComplete widget will take care of the update callbacks
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // { selected: mySelectedOptionObservable, options: myArrayOfLabelValuePairs }
            var settings = valueAccessor();

            var context = bindingContext;

            var updateElementValueWithLabel = function (event, ui) {
                // Stop the default behavior
                event.preventDefault();

                // Update the value of the html element with the label
                // of the activated option in the list (ui.item)
                //$(element).val(ui.item.email);
                context.$data.EMAIL(ui.item.email);
                context.$data.NOM(ui.item.nom);
                context.$data.PRENOM(ui.item.prenom);
                context.$data.FONCTION(ui.item.fonction);
                context.$data.TELEPHONE(ui.item.tel);
                //context.$data.ID_PARTICIPANT(ui.item.id);
            };

            $(element).autocomplete({
                source: function (request, response) {
                    $.ajax({
                        async: false,
                        cache: false,
                        type: "POST",
                        url: '@Url.Action("AutoComplete", "Participant")',
                        data: { "term": request.term },
                        success: function (data) {
                            response(data);
                        }
                    });
                },
                minLength: 2,
                appendTo: '#reservFormV',
                select: function (event, ui) {
                    updateElementValueWithLabel(event, ui);
                }
            });
        }
    };

        ko.applyBindings(viewModel);
</script>

        End Section
