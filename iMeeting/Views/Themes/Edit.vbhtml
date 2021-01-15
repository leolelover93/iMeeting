@ModelType ThemesVM
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.theme_EditTitle
End Code

<h2>@Resource.theme_EditTitle</h2>
           
@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
			@Html.HiddenFor(Function(model) model.Id)

			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.Libelle, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.Libelle)
					 @Html.ValidationMessageFor(Function(model) model.Libelle)
				 </div>
			</div>
			 
			@Html.HiddenFor(Function(model) model.DateCreation)


         
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
				<input type="submit" value="@Resource.Edit" class="btn btn-primary btn-sm" />
				@Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using


@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section

