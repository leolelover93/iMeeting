@ModelType TexteDefilantsVM
@Imports imeeting.My.Resources
@Code
    ViewData("Title") = Resource.txtd_CreateTitle
End Code

<h2>@Resource.txtd_CreateTitle</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.Message, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.Message)
					 @Html.ValidationMessageFor(Function(model) model.Message)
				 </div>
			</div>
			 
			<div class="form-group">
				 @Html.LabelFor(Function(model) model.EstPublie, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.EstPublie)
					 @Html.ValidationMessageFor(Function(model) model.EstPublie)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.DateDebut, New With {.class = "control-label col-md-2"})
				 <div class="col-md-2">
					 @Html.EditorFor(Function(model) model.DateDebut)
					 @Html.ValidationMessageFor(Function(model) model.DateDebut)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.DateFin, New With {.class = "control-label col-md-2"})
				 <div class="col-md-2">
					 @Html.EditorFor(Function(model) model.DateFin)
					 @Html.ValidationMessageFor(Function(model) model.DateFin)
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.PointAffichageId, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.DropDownListFor(Function(model) model.PointAffichageId,
						New SelectList(model.PointAffichageIds, "Value", "Text"), Resource.txtd_affi_select, New With {.class = "form-control"})
					 @Html.ValidationMessageFor(Function(model) model.PointAffichageId)
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

