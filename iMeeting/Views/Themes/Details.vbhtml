@ModelType Theme
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.theme_DetailTitle
End Code

<h2>@Resource.theme_DetailTitle</h2>
 
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
</div>
<p>
 @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.Id}, New With {.class = "btn btn-primary btn-sm"})
 @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>

