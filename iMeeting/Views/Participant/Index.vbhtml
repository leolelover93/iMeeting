@ModelType PagedList.IPagedList(Of PARTICIPANT)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.part_ListTitle
End Code

<h2>@Resource.part_ListTitle</h2>

@Using (Html.BeginForm("Index", "Participant", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
    @<a class="btn btn-primary" title="@Resource.Create" href="@Url.Action("Create")">
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

<table class="table table-responsive table-bordered table-striped table-condensed">
    <thead>
        <tr>

            <th>
                @Html.ActionLink(Resource.part_Nom, "Index", New With {.sortOrder = ViewBag.NomSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs">
                @Html.ActionLink(Resource.part_Prenom, "Index", New With {.sortOrder = ViewBag.PrenomSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs">
                @Html.ActionLink(Resource.part_Fonction, "Index", New With {.sortOrder = ViewBag.FonctionSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.part_Email, "Index", New With {.sortOrder = ViewBag.EmailSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.part_Telephone, "Index", New With {.sortOrder = ViewBag.TelephoneSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.NOM)
                </td>
                <td class="hidden-xs">
                    @Html.DisplayFor(Function(modelItem) item.PRENOM)
                </td>
                <td class="hidden-xs">
                    @Html.DisplayFor(Function(modelItem) item.FONCTION)
                </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.EMAIL)
                </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.TELEPHONE)
                </td>
                <td>
                    <a class="btn btn-primary btn-sm" title="@Resource.Edit" href="@Url.Action("Edit", New With {.id = item.ID_PARTICIPANT})">
                        <span class="glyphicon glyphicon-pencil"></span>
                        <span class="sr-only">@Resource.Edit</span>
                    </a>
                    <a class="btn btn-primary btn-sm" title="@Resource.Details" href="@Url.Action("Details", New With {.id = item.ID_PARTICIPANT})">
                        <span class="glyphicon glyphicon-list-alt"></span>
                        <span class="sr-only">@Resource.Details</span>
                    </a>
                    <a class="btn btn-primary btn-sm" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.ID_PARTICIPANT})">
                        <span class="glyphicon glyphicon-remove"></span>
                        <span class="sr-only">@Resource.Delete</span>
                    </a>
                </td>
            </tr>
        Next
    </tbody>
</table>
<br />
           Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount (@ViewBag.EnregCount Enregistrement(s))

        @Html.PagedListPager(Model, Function(page) Url.Action("Index",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter}))
