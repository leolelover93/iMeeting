@ModelType SelectUserRolesViewModel
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = "UserRoles"
End Code

<h2>Roles for user @Html.DisplayFor(Function(m) m.UserName)</h2>
<hr />

@Using (Html.BeginForm("UserRoles", "Account", FormMethod.Post, New With {.encType = "multipart/form-data", .name = "myform"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
    @Html.ValidationSummary(True)
        <div class="form-group">
            <div class="col-md-10">
                @Html.HiddenFor(Function(m) m.Id)
                @Html.HiddenFor(Function(m) m.UserName)
            </div>
        </div>

        <h4>Select Role Assignments</h4>
        <hr />

        <table>
            @*<tr>
                <th>
                    Select
                </th>
                <th>
                    Role
                </th>
            </tr>*@
                @Html.EditorFor(Function(Model) Model.Roles)
        </table>
        <br />
        <hr />

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-primary btn-sm" />
                @Html.ActionLink(Resource.BackToList, "Index", Nothing, New With {.class = "btn btn-default btn-sm"})
            </div>
        </div>
    </div>
End Using
