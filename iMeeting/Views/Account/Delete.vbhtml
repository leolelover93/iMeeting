@ModelType EditUserViewModel
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ID_SERVICE)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SERVICE.LIBELLE)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.NOMS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.NOMS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PRENOMS)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PRENOMS)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TEL)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TEL)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.EMAIL)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.EMAIL)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.UserName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.UserName)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
