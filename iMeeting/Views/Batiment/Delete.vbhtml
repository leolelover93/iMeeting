@ModelType BATIMENT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.bati_DeleteTitle
End Code

<h2>@Resource.bati_DeleteTitle</h2>

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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
             <input type="submit" value="@Resource.Delete" class="btn btn-primary btn-sm" />
             @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
        </div>
    End Using
</div>
