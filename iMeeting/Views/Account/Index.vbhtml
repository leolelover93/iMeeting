@ModelType PagedList.IPagedList(Of ApplicationUser)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.acc_listTitle
End Code

<h2>@Resource.acc_listTitle</h2>

@Using (Html.BeginForm("Index", "Account", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
    @<a class="btn btn-primary" title="@Resource.Create" href="@Url.Action("Register")">
        <span class="glyphicon glyphicon-plus"></span>
        @Resource.Create
    </a>
    @<div class="form-group">
        <div class="input-group">
            <div class="input-group-addon hidden-xs">@Resource.FindCriteria</div>
            @Html.TextBox("SearchString", CStr(ViewBag.CurrentFilter), New With {.class = "form-control", .placeholder = Resource.Find, .style = "max-width:100%"})
            <span class="input-group-btn">
                <button class="btn btn-primary" type="submit">@Resource.Find</button>
            </span>
        </div>
    </div>
End Using
<br />
<table class="table">
    <tr>
        <th>
            @Html.ActionLink(Resource.user_UserName, "Index", New With {.sortOrder = ViewBag.UserNameSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm">
            @Html.ActionLink(Resource.user_NOMS, "Index", New With {.sortOrder = ViewBag.NOMSSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm">
            @Html.ActionLink(Resource.user_PRENOMS, "Index", New With {.sortOrder = ViewBag.PRENOMSSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm">
            @Html.ActionLink(Resource.user_TEL, "Index", New With {.sortOrder = ViewBag.TELSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm">
            @Html.ActionLink(Resource.user_EMAIL, "Index", New With {.sortOrder = ViewBag.EMAILSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm">
            @Html.ActionLink(Resource.user_IDSERVICE, "Index", New With {.sortOrder = ViewBag.IDSERVICESortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>

        <th></th>
    </tr>

@For Each item In Model
    @<tr>
         <td>
             @Html.DisplayFor(Function(modelItem) item.UserName)
         </td>
<td>
            @Html.DisplayFor(Function(modelItem) item.NOMS)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.PRENOMS)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.TEL)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.EMAIL)
        </td>
         <td>
             @Html.DisplayFor(Function(modelItem) item.Service.LIBELLE)
        </td>
         <td>
             <a class="btn btn-primary btn-sm" title="@Resource.Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
                 <span class="glyphicon glyphicon-pencil"></span>
                 <span class="sr-only">@Resource.Edit</span>
             </a>
             <a class="btn btn-primary btn-sm" title="@Resource.Details" href="@Url.Action("UserRoles", New With {.id = item.Id})">
                 <span class="glyphicon glyphicon-list-alt"></span>
                 <span class="sr-only">@Resource.Details</span>
             </a>
             <a class="btn btn-primary btn-sm" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.Id})">
                 <span class="glyphicon glyphicon-remove"></span>
                 <span class="sr-only">@Resource.Delete</span>
             </a>
         </td>
    </tr>
Next

</table>
<br />
Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount (@ViewBag.EnregCount Enregistrement(s))

@Html.PagedListPager(Model, Function(page) Url.Action("Index",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
