@ModelType BUREAUVM
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.bur_CreateTitle
End Code

<h2>@Resource.bur_CreateTitle</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.IDSERVICE, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.DropDownListFor(Function(model) model.IDSERVICE,
						New SelectList(model.IDs, "ID_SERVICE", "Libelle"), Resource.bur_serv_select, New With {.class = "form-control"})
					 @Html.ValidationMessageFor(Function(model) model.IDSERVICE)
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.LIBELLE, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.LIBELLE)
					 @Html.ValidationMessageFor(Function(model) model.LIBELLE)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.EMPLACEMENT, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.EMPLACEMENT)
					 @Html.ValidationMessageFor(Function(model) model.EMPLACEMENT)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.DETAILS, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.DETAILS)
					 @Html.ValidationMessageFor(Function(model) model.DETAILS)
				 </div>
			</div>
			 

         
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
				<input type="submit" value="@Resource.Save" class="btn btn-primary btn-sm" />
				@Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using


@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section

