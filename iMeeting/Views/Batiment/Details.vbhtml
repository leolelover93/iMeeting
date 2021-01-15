@ModelType BATIMENT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.bati_DetailTitle
End Code

<h2>@Resource.bati_DetailTitle</h2>


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
            @Html.DisplayNameFor(Function(model) model.POSITION)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.POSITION, "Position")
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NBRE_PIECE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NBRE_PIECE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DETAILS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DETAILS)
        </dd>

    </dl>
</div>
<p>
   @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.ID_BAT}, New With {.class = "btn btn-primary btn-sm"})
    @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>
