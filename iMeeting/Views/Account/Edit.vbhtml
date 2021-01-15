@ModelType EditUserViewModel
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = "Edit User"
End Code

<h2>Edit</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(model) model.Id)
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
         <div class="form-group">
             @Html.LabelFor(Function(model) model.UserName, New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.TextBoxFor(Function(model) model.UserName, New With {.class = "form-control"})
                 @Html.ValidationMessageFor(Function(model) model.UserName)
             </div>
         </div>
         <div class="form-group">
             @Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})
             <div class="col-md-10">
                 @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
             </div>
         </div>
         <div class="form-group">
             @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "col-md-2 control-label"})
             <div class="col-md-10">
                 @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control"})
             </div>
         </div>
         <div class="form-group">
            @Html.LabelFor(Function(model) model.ID_SERVICE, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.DropDownListFor(Function(model) model.ID_SERVICE,
                        New SelectList(Model.IDs, "ID_SERVICE", "Libelle"), Resource.bur_serv_select, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.ID_SERVICE)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.NOMS, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.NOMS)
                @Html.ValidationMessageFor(Function(model) model.NOMS)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.PRENOMS, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.PRENOMS)
                @Html.ValidationMessageFor(Function(model) model.PRENOMS)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TEL, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.TEL)
                @Html.ValidationMessageFor(Function(model) model.TEL)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.EMAIL, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.EMAIL)
                @Html.ValidationMessageFor(Function(model) model.EMAIL)
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
