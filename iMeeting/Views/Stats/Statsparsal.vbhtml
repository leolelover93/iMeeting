@ModelType PagedList.IPagedList(Of RESERVATION)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
  
End Code
<h2>Planing de reservations par Salle</h2>

@Using (Html.BeginForm("Statsparsal", "Stats", FormMethod.Get, New With {.class = "form-inline", .role = "form", .id = "formSearch"}))
    @Html.Hidden("date_debut_iso")
    @Html.Hidden("date_fin_iso")
    @<text>
        <div class="form-group input-group">
            <span class="input-group-addon">Emplacement</span>
            @Html.DropDownList("respo", DirectCast(ViewBag.LIEUX, SelectList), Resource.stat_select_salle, New With {.class = "form-control", .id = "sal", .data_val = "false"})
            <span class="input-group-addon">Date début</span>
            @Html.TextBox("date_debut", "", New With {.class = "form-control datefield", .data_val = "false", .type = "text"})
            <span class="input-group-addon">Date fin</span>
            @Html.TextBox("date_fin", "", New With {.class = "form-control datefield", .data_val = "false", .type = "text"})
        </div>
        <button class="btn btn-primary" id="showData"><i class="glyphicon glyphicon-export"></i> Afficher</button>
        <button class="btn btn-primary" id="lnkReport"><i class="fa fa-print"></i> Imprimer</button>
    </text>
End Using
<br />
<table class="table table-responsive table-bordered table-striped table-condensed">
    <thead>
        <tr>
            <th class="col-md-1">Date</th>
            <th class="col-md-1">
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

@Html.PagedListPager(Model, Function(page) Url.Action("Statsparsal",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .date_debut = ViewBag.date_debut, .date_fin = ViewBag.date_fin, .respo = ViewBag.respo, .date_debut_iso = ViewBag.date_debut_iso, .date_fin_iso = ViewBag.date_fin_iso}))


<!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="myModalLabel">Aperçu planning présionnel : </h4>
            </div>
            <div class="modal-body">
                <div class="embed-responsive embed-responsive-16by9">
                    <iframe id="ifrReport" class="embed-responsive-item"></iframe>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" id="lnkMail">Mail</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
            </div>
        </div>
    </div>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")

    <script type="text/javascript">
        $(document).ready(function () {
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

            $('#lnkReport').on('click', function () {
                var date = $('#formSearch #date_debut').datepicker('getDate');
                var params = 'id=' + $('#formSearch #sal').val();
                params = params + '&date_debut=';
                if (date) {
                     params = params + $.datepicker.formatDate('yy-mm-dd', date);
                }
                var date = $('#formSearch #date_fin').datepicker('getDate');
                params = params + '&date_fin=';
                if (date) {
                    params = params + $.datepicker.formatDate('yy-mm-dd', date);
                }
                //alert(params);
                $('#ifrReport').attr('src', '@Url.Content("~/Report/Report.aspx")?type=rpt&' + params)
                $('#myModal').modal('show');
                return false;
            });

            $('#lnkMail').on('click', function () {
                var date = $('#formSearch #date_debut').datepicker('getDate');
                var params = 'id=' + $('#formSearch #sal').val();
                params = params + '&date_debut=';
                if (date) {
                    params = params + $.datepicker.formatDate('yy-mm-dd', date);
                }
                var date = $('#formSearch #date_fin').datepicker('getDate');
                params = params + '&date_fin=';
                if (date) {
                    params = params + $.datepicker.formatDate('yy-mm-dd', date);
                }
                //alert(params);
                $('#ifrReport').attr('src', '@Url.Content("~/Report/Report.aspx")?type=mail&' + params)
                $('#myModal').modal('show');
                return false;
            });

            // Pour enlever la partie heure du datepicker date_debut", ViewBag.date_debut
            $("#date_debut").datepicker('setDate', '@ViewBag.date_debut_iso');
            $("#date_fin").datepicker('setDate', '@ViewBag.date_fin_iso');
        });
    </script>
End Section


