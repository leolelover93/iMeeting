@ModelType Theme
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.theme_DeleteTitle
End Code

<h2>@Resource.theme_DeleteTitle</h2>

<h3>@Resource.ConfirmDelete</h3>

<div>
    <hr />
    <dl class="dl-horizontal">
				<dt>
				@Html.DisplayNameFor(Function(model) model.Libelle)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.Libelle)
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
