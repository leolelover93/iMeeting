@ModelType RegisterViewModel
@Imports iMeeting.My.Resources
@Code
    ViewBag.Title = Resource.acc_newuser
End Code

<h2>@ViewBag.Title.</h2>

@Using Html.BeginForm("Register", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})

    @Html.AntiForgeryToken()

    @<text>
    <hr />
    @Html.ValidationSummary()
    <div class="form-group required-field">
        @Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-2 control-label"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
        </div>
    </div>
    <div class="form-group required-field">
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
@*On ajoute nos propriétés*@
<div class="form-group required-field">
    @Html.LabelFor(Function(m) m.NOMS, New With {.class = "col-md-2 control-label"})
    <div class="col-md-10">
        @Html.TextBoxFor(Function(m) m.NOMS, New With {.class = "form-control"})
    </div>
</div>
<div class="form-group">
    @Html.LabelFor(Function(m) m.PRENOMS, New With {.class = "col-md-2 control-label"})
    <div class="col-md-10">
        @Html.TextBoxFor(Function(m) m.PRENOMS, New With {.class = "form-control"})
    </div>
</div>
<div class="form-group">
    @Html.LabelFor(Function(m) m.TEL, New With {.class = "col-md-2 control-label"})
    <div class="col-md-10">
        @Html.TextBoxFor(Function(m) m.TEL, New With {.class = "form-control"})
    </div>
</div>
<div class="form-group required-field">
    @Html.LabelFor(Function(m) m.EMAIL, New With {.class = "col-md-2 control-label"})
    <div class="col-md-10">
        @Html.TextBoxFor(Function(m) m.EMAIL, New With {.class = "form-control"})
    </div>
</div>
<div class="form-group required-field">
    @Html.LabelFor(Function(m) m.ID_SERVICE, New With {.class = "col-md-2 control-label"})
    <div class="col-md-10">
       @Html.DropDownListFor(Function(model) model.ID_SERVICE,
                        New SelectList(Model.IDs, "ID_SERVICE", "Libelle"), Resource.bur_serv_select, New With {.class = "form-control"})

    </div>
</div>

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input type="submit" class="btn btn-primary btn-sm" value="@Resource.Save" />
            @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
        </div>
    </div>
    </text>
End Using

@section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
