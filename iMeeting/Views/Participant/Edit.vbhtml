@ModelType PARTICIPANT
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.part_EditTitle
End Code

<h2>@Resource.part_EditTitle</h2>


@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(true)
        @Html.HiddenFor(Function(model) model.ID_PARTICIPANT)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.NOM, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.NOM)
                @Html.ValidationMessageFor(Function(model) model.NOM)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.PRENOM, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.PRENOM)
                @Html.ValidationMessageFor(Function(model) model.PRENOM)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.FONCTION, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.FONCTION)
                @Html.ValidationMessageFor(Function(model) model.FONCTION)
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
            @Html.LabelFor(Function(model) model.TELEPHONE, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.TELEPHONE)
                @Html.ValidationMessageFor(Function(model) model.TELEPHONE)
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

<div>
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
