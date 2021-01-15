@ModelType PARTICIPANT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.part_DetailTitle
End Code

<h2>@Resource.part_DetailTitle</h2>

<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.NOM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NOM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PRENOM)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PRENOM)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.FONCTION)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.FONCTION)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EMAIL)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EMAIL)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TELEPHONE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TELEPHONE)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.ID_PARTICIPANT}, New With {.class = "btn btn-primary btn-sm"})
    @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>
