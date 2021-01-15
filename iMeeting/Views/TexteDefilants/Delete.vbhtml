@ModelType TexteDefilantsVM
@Imports imeeting.My.Resources
@Code
    ViewData("Title") = Resource.txtd_DeleteTitle
End Code

<h2>@Resource.txtd_DeleteTitle</h2>

<h3>@Resource.ConfirmDelete</h3>

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

	@Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
        
			<input type="submit" value="@Resource.Delete" class="btn btn-primary btn-sm" />
			@Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
		</div>
    End Using
</div>
