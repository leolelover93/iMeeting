@ModelType PlanningSettingsVM
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.ppla_DetailTitle
End Code

<h2>@Resource.ppla_DetailTitle</h2>
 
 <div>
    <dl class="dl-horizontal well bs-component">	
			<dt>
				@Html.DisplayNameFor(Function(model) model.PointAffichageId)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.PointAffichage.LIBELLE)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.LieuId)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.Lieu.LIBELLE)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.Afficher)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.Afficher)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.ViewType)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.ViewType)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.MaxCharsPage)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.MaxCharsPage)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.MaxPageElements)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.MaxPageElements)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.LibelleVue)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.LibelleVue)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.Animation)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.Animation)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.AnimationTime)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.AnimationTime)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.ViewShowTime)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.ViewShowTime)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.LibellePeriode)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.LibellePeriode)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.BackgoundImage)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.BackgoundImage)
			</dd>
			

			<dt>
				@Html.DisplayNameFor(Function(model) model.DivClassName)
			</dt>			
			<dd>
				@Html.DisplayFor(Function(model) model.DivClassName)
			</dd>
			

    </dl>
</div>
<p>
 @Html.ActionLink(Resource.app_edit, "Edit", New With {.id = Model.Id}, New With {.class = "btn btn-primary btn-sm"})
 @Html.ActionLink(Resource.app_back_to_list, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
</p>

