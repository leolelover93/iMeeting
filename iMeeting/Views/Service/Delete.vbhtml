@ModelType SERVICE
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.serv_DeleteTitle
End Code

<h2>@Resource.serv_DeleteTitle</h2>

<h3>@Resource.ConfirmDelete</h3>


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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
             <input type="submit" value="@Resource.Delete" class="btn btn-primary btn-sm" />
             @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
        </div>
    End Using
</div>
