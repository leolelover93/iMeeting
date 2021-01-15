@ModelType BATIMENT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.bati_EditTitle
End Code

<h2>@Resource.bati_EditTitle</h2>


@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
        @Html.HiddenFor(Function(model) model.ID_BAT)
        @Html.HiddenFor(Function(model) model.DATE_CREATION)

         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.LIBELLE, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.LIBELLE)
                 @Html.ValidationMessageFor(Function(model) model.LIBELLE)
             </div>
         </div>


         <div class="form-group required-field">
             @Html.LabelFor(Function(model) model.POSITION, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.POSITION, "Position")
                 @Html.ValidationMessageFor(Function(model) model.POSITION)
             </div>
         </div>


        <div class="form-group">
            @Html.LabelFor(Function(model) model.NBRE_PIECE, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.NBRE_PIECE)
                @Html.ValidationMessageFor(Function(model) model.NBRE_PIECE)
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
                <input type="submit" value="@Resource.Edit" class="btn btn-primary btn-sm" />
                @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
