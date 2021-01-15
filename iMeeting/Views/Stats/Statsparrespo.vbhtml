@ModelType PagedList.IPagedList(Of RESERVATION)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code

End Code

<h2>Planing de reservations par Responsable</h2> 
@Using (Html.BeginForm("Statsparrespo", "Stats", FormMethod.Get, New With {.class = "form-inline", .role = "form", .id = "formSearch"}))
    @Html.Hidden("date_debut_iso")
    @Html.Hidden("date_fin_iso")
    @<text>
        <div class="form-group input-group">
            <span class="input-group-addon">Président Séance</span>
            @Html.TextBox("respo", ViewBag.respo, New With {.class = "form-control"})

            <span class="input-group-addon">Date début</span>
            @Html.TextBox("date_debut", "", New With {.class = "form-control datefield", .data_val = "false", .type = "text"})
            <span class="input-group-addon">Date fin</span>
            @Html.TextBox("date_fin", "", New With {.class = "form-control datefield", .data_val = "false", .type = "text"})
        </div>
        <button class="btn btn-primary" id="showData"><i class="glyphicon glyphicon-export"></i> Afficher</button>
        @*<button class="btn btn-primary" id="lnkReport"><i class="fa fa-print"></i> Imprimer</button>*@
    </text>
End Using
<br />
<table class="table table-responsive table-bordered table-striped table-condensed">
    <thead>
        <tr>
            <th>Date</th>
            <th>
                    Heure
            </th>
            <th class="hidden-xs">
                    Emplacement
            </th>
            <th class="hidden-xs">
                    Objet
            </th>
            <th class="hidden-xs">
                    Thème
            </th>
            <th class="hidden-xs hidden-sm">
                    Président
            </th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.DATE_DEBUT)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.HEURE_DEBUT) - @Html.DisplayFor(Function(modelItem) item.HEURE_FIN)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.LIEUX.LIBELLE)
                </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Objet)
                    </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Theme.Libelle)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.PresidentSeance)
                </td>
                    
            </tr>
        Next
    </tbody>
</table>


@Html.PagedListPager(Model, Function(page) Url.Action("Statsparrespo",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .date_debut = ViewBag.date_debut, .date_fin = ViewBag.date_fin, .respo = ViewBag.respo, .date_debut_iso = ViewBag.date_debut_iso, .date_fin_iso = ViewBag.date_fin_iso}))


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")

<script type="text/javascript">
    $(document).ready(function () {
        $('#respo').autocomplete({
            source: function (request, response) {
                var uo = new Array();
                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: '@Url.Action("AutoComplete", "Reservation")',
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

        $('#formSearch #showData').on('click', function () {
            var date = $('#formSearch #date_debut').datepicker('getDate');
            $('#formSearch #date_debut_iso').val();
            $('#formSearch #date_fin_iso').val();
            if (date) {
                $('#formSearch #date_debut_iso').val($.datepicker.formatDate('yy-mm-dd', date));
            }
            var date = $('#formSearch #date_fin').datepicker('getDate');
            if (date) {
                $('#formSearch #date_fin_iso').val($.datepicker.formatDate('yy-mm-dd', date));
            }
        });


        // Pour enlever la partie heure du datepicker date_debut", ViewBag.date_debut
        $("#date_debut").datepicker('setDate', '@ViewBag.date_debut_iso');
        $("#date_fin").datepicker('setDate', '@ViewBag.date_fin_iso');
    });
</script>

End Section


