@ModelType PlanningSettingsVM
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.ppla_EditTitle
End Code

<h2>@Resource.ppla_EditTitle</h2>
           
@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal  well bs-component">
        @Html.ValidationSummary(true)
			@Html.HiddenFor(Function(model) model.Id)

			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.PointAffichageId, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
                 @If Model.LieuId = 0 AndAlso Model.PointAffichageId = 0 Then
                     @Html.HiddenFor(Function(model) model.PointAffichageId)
                     @<span class="form-control-static">@Model.PointAffichage.LIBELLE</span>
                 Else
					 @Html.DropDownListFor(Function(model) model.PointAffichageId,
                        New SelectList(Model.PointAffichageIds, "Value", "Text"), New With {.class = "form-control"})
					 @Html.ValidationMessageFor(Function(model) model.PointAffichageId)
                 End If
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.LieuId, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
                 @If Model.LieuId = 0 AndAlso Model.PointAffichageId = 0 Then
                     @Html.HiddenFor(Function(model) model.LieuId)
                     @<span class="form-control-static">@Model.Lieu.LIBELLE</span>
                 Else
					 @Html.DropDownListFor(Function(model) model.LieuId,
                        New SelectList(Model.LieuIds, "Value", "Text"), New With {.class = "form-control"})
					 @Html.ValidationMessageFor(Function(model) model.LieuId)
                 End If
				 </div>
			</div>
			 
                @If Model.LieuId = 0 AndAlso Model.PointAffichageId = 0 Then
                        @Html.HiddenFor(Function(model) model.Afficher)
                 Else
                    @<div class="form-group">
				             @Html.LabelFor(Function(model) model.Afficher, New With {.class = "control-label col-md-2"})
				             <div class="col-md-10">
					             @Html.EditorFor(Function(model) model.Afficher)
					             @Html.ValidationMessageFor(Function(model) model.Afficher)
				             </div>
			            </div>
                End If
			
			 
			<div class="form-group">
				 @Html.LabelFor(Function(model) model.ViewType, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
                    @If Model.LieuId = 0 AndAlso Model.PointAffichageId = 0 Then
					 @Html.HiddenFor(Function(model) model.ViewType)
					 @Html.DisplayFor(Function(model) model.ViewType)
                    Else
					 @Html.EditorFor(Function(model) model.ViewType)
					 @Html.ValidationMessageFor(Function(model) model.ViewType)
                        End If
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.MaxCharsPage, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.MaxCharsPage)
					 @Html.ValidationMessageFor(Function(model) model.MaxCharsPage)
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.MaxPageElements, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.MaxPageElements)
					 @Html.ValidationMessageFor(Function(model) model.MaxPageElements)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.LibelleVue, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.LibelleVue)
					 @Html.ValidationMessageFor(Function(model) model.LibelleVue)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.Animation, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.Animation)
					 @Html.ValidationMessageFor(Function(model) model.Animation)
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.AnimationTime, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.AnimationTime)
					 @Html.ValidationMessageFor(Function(model) model.AnimationTime)
				 </div>
			</div>
			 
			<div class="form-group required-field">
				 @Html.LabelFor(Function(model) model.ViewShowTime, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.ViewShowTime)
					 @Html.ValidationMessageFor(Function(model) model.ViewShowTime)
				 </div>
			</div>
			 
         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.LibellePeriode, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.LibellePeriode)
                 @Html.ValidationMessageFor(Function(model) model.LibellePeriode)
             </div>
         </div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.BackgoundImage, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.BackgoundImage)
					 @Html.ValidationMessageFor(Function(model) model.BackgoundImage)
				 </div>
			</div>
			 
			<div class="form-group ">
				 @Html.LabelFor(Function(model) model.DivClassName, New With {.class = "control-label col-md-2"})
				 <div class="col-md-10">
					 @Html.EditorFor(Function(model) model.DivClassName)
					 @Html.ValidationMessageFor(Function(model) model.DivClassName)
				 </div>
			</div>
			 
			@Html.HiddenFor(Function(model) model.UserId)

			@Html.HiddenFor(Function(model) model.DateCreation)


         
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
				<input type="submit" value="@Resource.app_edit" class="btn btn-primary btn-sm" />
				@Html.ActionLink(Resource.app_back_to_list, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using


@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section

