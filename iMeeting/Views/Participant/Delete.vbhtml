@ModelType PARTICIPANT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.part_DeleteTitle
End Code

<h2>@Resource.part_DeleteTitle</h2>

<h3>@Resource.ConfirmDelete</h3>

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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
             <input type="submit" value="@Resource.Delete" class="btn btn-primary btn-sm" />
             @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
        </div>
    End Using
</div>
