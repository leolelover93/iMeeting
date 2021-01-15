@ModelType SERVICE
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.serv_DetailTitle
End Code

<h2>@Resource.serv_DetailTitle</h2>




<div>
    <h4>SERVICE</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.LIBELLE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.LIBELLE)
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
</div>
<p>
    @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.ID_SERVICE}, New With {.class = "btn btn-primary btn-sm"})
    @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>
