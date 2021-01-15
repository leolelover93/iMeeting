@ModelType RESERVATION
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.rese_DetailTitle
End Code

<h2>@Resource.rese_DetailTitle</h2>

<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ID_LIEU)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LIEUX.LIBELLE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Objet)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Objet)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DATE_DEBUT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DATE_DEBUT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DATE_FIN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DATE_FIN)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HEURE_DEBUT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HEURE_DEBUT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.HEURE_FIN)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.HEURE_FIN)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PresidentSeance)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PresidentSeance)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TelPresiSce)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TelPresiSce)
        </dd>
        <dt>
            @Html.DisplayNameFor(Function(model) model.ThemeId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Theme.Libelle)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NBRE_PERS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NBRE_PERS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DETAILS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DETAILS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ETAT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ETAT)
        </dd>
    </dl>
</div>
<p>
    @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.ID_RESERVATION}, New With {.class = "btn btn-primary btn-sm"})
    @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>
