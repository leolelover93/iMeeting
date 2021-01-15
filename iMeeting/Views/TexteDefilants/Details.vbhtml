@ModelType TexteDefilantsVM
@Imports imeeting.My.Resources
@Code
    ViewData("Title") = Resource.txtd_DetailTitle
End Code

<h2>@Resource.txtd_DetailTitle</h2>
 
 <div>
    <hr />
    <dl class="dl-horizontal">
				<dt>
				@Html.DisplayNameFor(Function(model) model.Message)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.Message)
			</dd>
			<dt>
				@Html.DisplayNameFor(Function(model) model.EstPublie)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.EstPublie)
			</dd>
			<dt>
				@Html.DisplayNameFor(Function(model) model.DateDebut)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.DateDebut)
			</dd>
			<dt>
				@Html.DisplayNameFor(Function(model) model.DateFin)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.DateFin)
			</dd>
			<dt>
				@Html.DisplayNameFor(Function(model) model.PointAffichageId)
			</dt>

			<dd>
				@Html.DisplayFor(Function(model) model.PointAffichage.LIBELLE)
			</dd>

    </dl>
</div>
<p>
 @Html.ActionLink(Resource.Edit, "Edit", New With {.id = Model.Id}, New With {.class = "btn btn-primary btn-sm"})
 @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>

