@ModelType LIEUX
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.cere_DeleteTitle
End Code

<h2>@Resource.cere_DeleteTitle</h2>

<h3>@Resource.ConfirmDelete</h3>

<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.LIBELLE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LIBELLE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EMPLACEMENT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EMPLACEMENT)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.CAPACITE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CAPACITE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DETAILS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DETAILS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ID_BAT)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BATIMENT.LIBELLE)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
             <input type="submit" value="@Resource.Delete" class="btn btn-primary btn-sm" />
             @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
        </div>
    End Using
</div>
