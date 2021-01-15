@ModelType ReservationViewModel
@Imports iMeeting.My.Resources 
@Imports Microsoft.AspNet.Identity
@Code
    ViewData("Title") = Resource.rese_ListTitle
    Dim Culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant()
    If Culture = "fr-fr" Then
        Culture = "fr"
    End If
    Dim calendar_selectable As String = IIf(User.IsInRole("Utilisateur"), "true", "false") ' Javascript est sensible à la case
    Dim calendar_editable As String = IIf(Request.IsAuthenticated AndAlso User.IsInRole("SCC"), "true", "false") ' Javascript est sensible à la case
End Code

@Section css
    @Styles.Render("~/Content/css/calendar")
    <link href="@Styles.Url("~/Content/fullcalendar.print.css")" rel="stylesheet" type="text/css" media="print" />

    <style>
        table tr td:last-child {
            white-space: initial;
            width: auto;
        }
    </style>

End Section

@ViewBag.Message

<div class="form-horizontal">
    <div class="form-group">
        <span class="h2 control-label col-md-4" style="margin-top:-10px">@Resource.rese_ListTitle</span>
        <div class="dropdown col-md-7">
            @Html.DropDownListFor(Function(m) m.ID_LIEU, DirectCast(ViewBag.LIEUX, SelectList),  New With {.class = "form-control", .id="cboLieux"})
        </div>
        <a class="btn btn-primary col-md-1" title="@Resource.Create" href="@Url.Action("Create")">
            <span class="glyphicon glyphicon-plus"></span>
            @Resource.Create
        </a>
    </div>
</div>
<div id='calendar'></div>


<!-- Modal -->
<div class="modal fade" id="myModalEvnt" tabindex="-1" role="dialog" aria-labelledby="myModalEvntLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="myModalEvntLabel"><span id="title"></span></h4>
            </div>
            <div class="modal-body">
                <div class="form-horizontal">
                    @Html.Hidden("id_reservation")
                    <div class="form-group">
                        <label class="control-label col-md-3">@Resource.rese_periode</label>
                        <div class="col-md-9">
                            <span class="form-control-static" id="when"></span>
                        </div>
                    </div>

                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.PresidentSeance, New With {.class = "control-label col-md-3"})
                        <div class="col-md-9">
                            <span class="form-control-static" id="president"></span>
                        </div>
                    </div>

                    <div class="form-group" id="divState">
                        @Html.LabelFor(Function(model) model.CurrentState, Resource.rese_CurrentState, New With {.class = "control-label col-md-3"})
                        @If User.IsInRole("SCC") Then
                             @Using (Html.BeginForm("UpdateStatus", "Reservation", FormMethod.Post))
                                @Html.AntiForgeryToken()
                                @Html.Hidden("id")
                                @<div class="col-md-6">
                                    @Html.EditorFor(Function(model) model.CurrentState)
                                    @Html.ValidationMessageFor(Function(model) model.CurrentState)
                                </div>
                           
                                @<div class="form-actions no-color col-md-3">
                                    <input type="submit" value="@Resource.Save" class="btn btn-default" /> 
                                </div>
                            End Using
                        Else
                            @<div class="col-md-9">
                                 <span class="form-control-static" id="currentStateName"></span>
                            </div>
                        End If
                            
                        </div>
                    </div>
            </div>
            <div class="modal-footer">
                @Using (Html.BeginForm("Reporter", "Reservation", FormMethod.Get, New With {.id = "ReportForm"}))
                    @Html.Hidden("id")
                End Using
                <button type="button" id="btnReport" onclick="Reporter()" class="btn btn-primary">@Resource.rese_report</button>
                <button type="button" id="btnAnnulation" onclick="Annulation()" data-dismiss="modal" class="btn btn-primary">@Resource.rese_annulation</button>
                @If User.IsInRole("SCC") Then
                    @<a href="#" class="btn btn-primary" id="edit" role="button">Validation</a>
                End If
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="myModalForValid" tabindex="-1" role="dialog" aria-labelledby="myModalLabelFV" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h3 class="modal-title" id="myModalLabelFV">
                    Validation Réservation 
                </h3>
            </div>
            <div class="modal-body">
                @Using (Html.BeginForm("Calendar", "Reservation", FormMethod.Post, New With {.id = "reservFormV"}))
                    @Html.AntiForgeryToken()

                    @Html.ValidationSummary(True)
                    @Html.HiddenFor(Function(m) m.ID_RESERVATION)
                    
                    
                    @<div class="form-horizontal">
                          <div id="rootwizardFV">
                              <!-- Nav tabs -->
                              <ul class="nav nav-tabs" role="tablist">
                                  <li class="active"><a href="#tabFV1" role="tab" data-toggle="tab">Informations</a></li>
                                  <li><a href="#tabFV2" role="tab" data-toggle="tab">Participants</a></li>
                                  <li><a href="#tabFV3" role="tab" data-toggle="tab">Autres</a></li>
                              </ul>

                              <div class="tab-content">
                                <div class="tab-pane active" id="tabFV1">
                                    <div class="form-group required-field">
                                        @Html.LabelFor(Function(model) model.ID_LIEU, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.DropDownListFor(Function(m) m.ID_LIEU, DirectCast(ViewBag.LIEUX, SelectList), Resource.rese_select_lieu, New With {.class = "form-control"})
                                            @Html.ValidationMessageFor(Function(model) model.ID_LIEU)
                                        </div>
                                    </div>

                                    <div class="form-group required-field">
                                        @Html.LabelFor(Function(model) model.Objet, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.EditorFor(Function(model) model.Objet)
                                            @Html.ValidationMessageFor(Function(model) model.Objet)
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        @Html.LabelFor(Function(model) model.DATE_DEBUT, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-3">
                                            @Html.EditorFor(Function(model) model.DATE_DEBUT)
                                            @Html.HiddenFor(Function(model) model.DATE_FIN)
                                            @Html.ValidationMessageFor(Function(model) model.DATE_DEBUT)
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        @Html.LabelFor(Function(model) model.HEURE_DEBUT, Resource.rese_Plage, New With {.class = "control-label col-md-3"})
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
                                        @Html.LabelFor(Function(model) model.PresidentSeance, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.EditorFor(Function(model) model.PresidentSeance)
                                            @Html.ValidationMessageFor(Function(model) model.PresidentSeance)
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        @Html.LabelFor(Function(model) model.TelPresiSce, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.EditorFor(Function(model) model.TelPresiSce)
                                            @Html.ValidationMessageFor(Function(model) model.TelPresiSce)
                                        </div>
                                    </div>

                                    <div class="form-group required-field">
                                        @Html.LabelFor(Function(model) model.ThemeId, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.DropDownListFor(Function(m) m.ThemeId, DirectCast(ViewBag.Themes, SelectList), Resource.rese_select_theme, New With {.class = "form-control"})
                                            @Html.ValidationMessageFor(Function(model) model.ThemeId)
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        @Html.LabelFor(Function(model) model.NBRE_PERS, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-2">
                                            @Html.EditorFor(Function(model) model.NBRE_PERS)
                                            @Html.ValidationMessageFor(Function(model) model.NBRE_PERS)
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane" id="tabFV3">
                                    <div class="form-group">
                                        @Html.LabelFor(Function(model) model.DETAILS, New With {.class = "control-label col-md-3"})
                                        <div class="col-md-9">
                                            @Html.EditorFor(Function(model) model.DETAILS)
                                            @Html.ValidationMessageFor(Function(model) model.DETAILS)
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane" id="tabFV2">
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
                                <div class="pager wizard" style="float:right; padding-top:10px;">
                                    <input type="submit" class="btn btn-primary btn-sm finish" name="finish" id="btnConfirm" value="@Resource.validate" />
                                    <button type="button" class="btn btn-default btn-sm cancel" name="cancel" data-dismiss="modal">@Resource.Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>
                End Using
            </div>
            <div class="modal-footer" style="padding:0px; margin:0px;">

            </div>
        </div>
    </div>
</div>

@*<a onclick="$('#myModalForValid').modal('show');" href="#">Click here</a>*@

@Section Scripts
    @Scripts.Render("~/bundles/calendar")
    @Scripts.Render("~/bundles/jqueryval")


    <script>

    $(document).ready(function () {
        var sourceFullView = { url: '@Url.Action("GetDiaryEvents")/' };
        var sourceSummaryView = { url: '@Url.Action("GetDiarySummary")/' };
        var CalLoading = true;

        // Le combobox
        $("#cboLieux").change(function () {
            //alert($('#cboLieux option:selected').html());
            $('#calendar').fullCalendar('refetchEvents')
        });

        // Le calendrier
        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay prevYear,nextYear'
            },
            defaultDate: '@Model.DATE_DEBUT.ToString("yyyy-MM-dd")',
            minTime: '@String.Format("{0}:00:00", ConfigurationManager.AppSettings("calMinTime"))',
            maxTime: '@String.Format("{0}:00:00", ConfigurationManager.AppSettings("calMaxTime"))',
            defaultView: "agendaWeek",
            lang: '@Culture',
            selectable: @(calendar_selectable) +0,
            allDaySlot: false,
            selectHelper: true,
            select: function (start, end) {
                //alert(start.format('L'));
                $("#myModalForValid #ID_RESERVATION").val("0");
                $("#myModalForValid #ID_LIEU").val($('#cboLieux option:selected').val());
                $('#myModalForValid [name=DATE_DEBUT]').val(start.format('L'));
                //$('#myModalForValid #DATE_FIN').val(end.format('L'));
                $('#myModalForValid #HEURE_DEBUT').val(start.format('HH:mm'));
                $('#myModalForValid #HEURE_FIN').val(end.format('HH:mm'));
                $('#myModalForValid #myModalLabelFV').text('@Html.Raw(Resource.rese_CreateTitle)');
                $('#myModalForValid #btnConfirm').val('@Html.Raw(Resource.Create)');
                $('#myModalForValid #TelPresiSce').val(""); // reserv.LIBELLE
                $('#myModalForValid #Objet').val(""); // reserv.MOTIF
                $('#myModalForValid #PresidentSeance').val("");
                $('#myModalForValid #ThemeId').val("");
                $('#myModalForValid #NBRE_PERS').val(""); // reserv.NBRE_PERS
                $('#myModalForValid #DETAILS').val(""); // reserv.DETAILS

                viewModel.participants.removeAll();

                $('#myModalForValid').modal('show');

                //http://stackoverflow.com/questions/22405259/use-jquery-datepicker-in-bootstrap-3-modal
                $('.datefield').css("z-index", "0");
            },
            editable: @(calendar_editable) +0,
            events: function (start, end, timezone, callback) {
                var vue = $('#calendar').fullCalendar('getView').name;
                var id_lieu = $("#cboLieux").val();
                //alert(id_lieu);
                $.ajax({
                    url: '@Url.Action("GetDiaryEvents")/',
                    type: 'GET',
                    dataType: 'json',
                    data: {
                        start: start.toISOString(),
                        end: end.toISOString(),
                        id_lieu: id_lieu,
                        vue: vue
                    },
                    success: function (doc) {
                        var events = eval(doc);
                        callback(events);
                    }
                });
            },

            eventClick: function (calEvent, jsEvent, view) {
                end = calEvent.end;
                start = calEvent.start;
                endtime = (end) ? end.format('HH:mm') : '';
                starttime = start.format('dddd DD/MM/YYYY, HH:mm'); // dddd jour complet à localiser
                allDay = (end) ? false : true;
                var mywhen = starttime + ' - ' + endtime;
                $('#myModalEvnt #id_reservation').val(calEvent.id);
                $('#myModalEvnt #ReportForm #id').val(calEvent.id);
                $('#myModalEvnt #divState #id').val(calEvent.id);
                $('#myModalEvnt #title').text(calEvent.title);
                $('#myModalEvnt #when').text(mywhen);
                $('#myModalEvnt #president').text(calEvent.president);
                $('#myModalEvnt #currentStateName').text(calEvent.currentStateDisplayName);
                $('#myModalEvnt #edit').attr('href', calEvent.url);

                $('#myModalEvnt #divState').hide();
                $('#myModalEvnt #edit').hide();
                $('#myModalEvnt #edit').text("@Resource.validate");

                if (calEvent.State == 2 || calEvent.State == 0) { // Evenement déjà validé ou initial
                    $('#myModalEvnt #edit').show();

                    if (calEvent.State == 2) { // Validé
                        $('#myModalEvnt #divState').show();
                        $('#myModalEvnt #edit').text("@Resource.Edit");
                    }
                }

                //alert(calEvent.userId);
                if (calEvent.userId == '@User.Identity.GetUserId()') {
                    $('#myModalEvnt #btnAnnulation').show();
                    $('#myModalEvnt #btnReport').show();
                }
                else {
                    $('#myModalEvnt #btnAnnulation').hide();
                    $('#myModalEvnt #btnReport').hide();
                }

                // Si le current state est différent de initial, l'afficher
                if (calEvent.currentState != 0) {
                    $('#myModalEvnt #divState').show();

                    // Si le current state est cancel pending ou cancel, cacher
                    if (calEvent.currentState == 10 || calEvent.currentState == 11) {
                        $('#myModalEvnt #btnAnnulation').hide();
                        $('#myModalEvnt #btnReport').hide();
                    }

                    // Si le current state est report pending ou report, cacher
                    if (calEvent.currentState == 20 || calEvent.currentState == 21) {
                        $('#myModalEvnt #btnAnnulation').hide();
                        $('#myModalEvnt #btnReport').hide();
                    }
                }



                //alert($('#myModalEvnt #divState #CurrentState').val());
                $('#myModalEvnt #divState #CurrentState').val(calEvent.currentStateName);

                $('#myModalEvnt').modal('show');

                return false; // Ne pas poursuivre l'url
            },

            eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
                if (confirm("Confirm move?")) {
                    UpdateEvent(event.id, event.start, event.end, revertFunc);
                }
                else {
                    revertFunc();
                }
            },

            eventResize: function (event, delta, revertFunc, jsEvent, ui, view) {

                if (confirm("Confirm change reservation length?")) {
                    UpdateEvent(event.id, event.start, event.end, revertFunc);
                }
                else {
                    revertFunc();
                }
            },

            //dayClick: function (date, allDay, jsEvent, view) {
            //    $('#eventTitle').val("");
            //    $('#eventDate').val(date.format('dd/MM/yyyy'));
            //    $('#eventTime').val(date.format('HH:mm'));
            //    ShowEventPopup(date);
            //}
        });

        function UpdateEvent(EventID, EventStart, EventEnd, revertFunc) {

            var dataRow = {
                'ID': EventID,
                'NewEventStart': EventStart.toISOString(),
                'NewEventEnd': EventEnd.toISOString()
            }

            $.ajax({
                type: 'POST',
                url: '@Url.Action("UpdateEvent")',
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(dataRow),
                success: function (msg) {
                    if (msg == "ok") {
                        $('#calendar').fullCalendar('refetchEvents')
                    }
                    else {
                        revertFunc();
                        alert(msg);
                    }
                }
            });
        }


        CalLoading = false;

        // Initialiser le wizard
        var $validator = $("#createAppointmentForm").validate();

        function valideFormulaire(tab, navigation, index) {
            var $valid = $("#createAppointmentForm").valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
            return true;
        }

        // Initialiser le wizard
        var $validatorFV = $("#reservFormV").validate();

        function valideFormulaireFV(tab, navigation, index) {
            var $valid = $("#reservFormV").valid();
            if (!$valid) {
                $validatorFV.focusInvalid();
                return false;
            }
            return true;
        }



        @If Model.AutoShowDialog Then
@<text>
        $('#myModalForValid').modal('show');
        //http://stackoverflow.com/questions/22405259/use-jquery-datepicker-in-bootstrap-3-modal
        $('.datefield').css("z-index", "0");
        </text>
        End If


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
            minLength: 2,
            appendTo: '#reservFormV'
        });

    }); // Fin document.ready

        function Reporter() {
            $('#ReportForm').submit();
        }
        function Annulation() {
            if (confirm("Annuler la réservation ?")) {
                var dataRow = {
                    'id': $('#myModalEvnt #id_reservation').val()
                }

                $.ajax({
                    type: 'POST',
                    url: '@Url.Action("Annulation")',
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify(dataRow),
                    success: function (msg) {
                        if (msg == "ok") {
                            $('#calendar').fullCalendar('refetchEvents')
                        }
                        else {
                            alert(msg);
                        }
                    }
                });

            }
        }

    function CloseModalFV() {
        $('#myModalForValid').modal('hide');

    }


    function DefineValidationRules() {
        $.validator.addMethod("partName", $.validator.methods.required, "@Resource.part_NomRequired");
        $.validator.addMethod("partNameLong", $.validator.methods.maxlength, "@Resource.part_NomLong");
        //$.validator.addMethod("gameScore", ValidateInteger, "The Score must be an integer");
        //$.validator.addMethod("gameScoreMin", $.validator.methods.min, $.format("The Score must be greater than or equal to {0}"));
        //$.validator.addMethod("gameScoreMax", $.validator.methods.max, $.format("The Score value must be less than or equal to {0}"));

        $.validator.addClassRules("validate-name-required", { partName: true, partNameLong: 150 });

        //$.validator.addClassRules("validate-score", { gameScore: true, gameScoreMin: 0, gameScoreMax: 25 });
    }

    </script>

    @* ViewModel Knockout.js *@
    <script>
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


