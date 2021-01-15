@ModelType PagedList.IPagedList(Of LIEUX)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.cere_ListTitle
End Code

<h2>@Resource.cere_ListTitle</h2>

@Using (Html.BeginForm("Index", "Lieux", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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
                @Html.ActionLink(Resource.lieu_IdBat, "Index", New With {.sortOrder = ViewBag.IdBatSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs">
                @Html.ActionLink(Resource.lieu_Libelle, "Index", New With {.sortOrder = ViewBag.LibelleSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs">
                @Html.ActionLink(Resource.lieu_Emplacement, "Index", New With {.sortOrder = ViewBag.EmplacementSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.lieu_Capacite, "Index", New With {.sortOrder = ViewBag.CapaciteSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.lieu_Details, "Index", New With {.sortOrder = ViewBag.DetailsSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.BATIMENT.LIBELLE)
                </td>
                <td class="hidden-xs">
                                        @Html.DisplayFor(Function(modelItem) item.LIBELLE)
                                        @If item.Etat <> 0 Then
                                            @<Text><img src="@Url.Content("~/Content/Images/account_blocked.gif")" /></text>
                                        End If
                </td>
                <td class="hidden-xs">
                    @Html.DisplayFor(Function(modelItem) item.EMPLACEMENT)
                </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.CAPACITE)
                </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.DETAILS)
                </td>
                <td>
                    <a class="btn btn-primary btn-sm" title="@Resource.Edit" href="@Url.Action("Edit", New With {.id = item.ID_LIEU})">
                        <span class="glyphicon glyphicon-pencil"></span>
                        <span class="sr-only">@Resource.Edit</span>
                    </a>
                    <a class="btn btn-primary btn-sm" title="@Resource.Details" href="@Url.Action("Details", New With {.id = item.ID_LIEU})">
                        <span class="glyphicon glyphicon-list-alt"></span>
                        <span class="sr-only">@Resource.Details</span>
                    </a>
                    <a class="btn btn-primary btn-sm" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.ID_LIEU})">
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

