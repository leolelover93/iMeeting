﻿@ModelType LIEUX
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.lieu_EditTitle
End Code

<h2>@Resource.lieu_EditTitle</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
        @Html.HiddenFor(Function(model) model.ID_LIEU)
        @Html.HiddenFor(Function(model) model.TYPE_LIEU)
        @Html.HiddenFor(Function(model) model.DATE_CREATION)
        @Html.HiddenFor(Function(model) model.Etat)

         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.ID_BAT, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.DropDownListFor(Function(m) m.ID_BAT, DirectCast(ViewBag.BATIMENT, SelectList), Resource.bati_select, New With {.class = "form-control"})
                 @Html.ValidationMessageFor(Function(model) model.ID_BAT)
             </div>
         </div>

         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.LIBELLE, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.LIBELLE)
                 @Html.ValidationMessageFor(Function(model) model.LIBELLE)
             </div>
         </div>


        <div class="form-group">
            @Html.LabelFor(Function(model) model.EMPLACEMENT, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.EMPLACEMENT)
                @Html.ValidationMessageFor(Function(model) model.EMPLACEMENT)
            </div>
        </div>

         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.CAPACITE, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.CAPACITE)
                 @Html.ValidationMessageFor(Function(model) model.CAPACITE)
             </div>
         </div>


        <div class="form-group">
            @Html.LabelFor(Function(model) model.DETAILS, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.DETAILS)
                @Html.ValidationMessageFor(Function(model) model.DETAILS)
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" name="btnEdit" value="@Resource.Edit" class="btn btn-primary btn-sm" />
                <input type="submit" name="btnChangeStatus" value="@IIf(Model.Etat = 0, Resource.Disable, Resource.Enable)" class="btn btn-warning btn-sm" />
                @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
