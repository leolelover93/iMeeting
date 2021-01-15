@ModelType PagedList.IPagedList(Of TexteDefilant)
@Imports PagedList.Mvc
@Imports imeeting.My.Resources

@Code
    ViewData("Title") = Resource.txtd_ListTitle
End Code

<h2>@Resource.txtd_ListTitle</h2>

@Using (Html.BeginForm("Index", "TexteDefilants", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
    @<a class="btn btn-primary" title="@Resource.Create" href="@Url.Action("Create")">
        <span class="glyphicon glyphicon-plus"></span>
        @Resource.Create
    </a>
    @<div class="form-group">
        <div class="input-group">
            <div class="input-group-addon hidden-xs">@Resource.FindCriteria</div>
            @Html.TextBox("SearchString", CStr(ViewBag.CurrentFilter), New With {.class = "form-control", .placeholder = Resource.Find, .style="max-width:100%"})
            <span class="input-group-btn">
                <button class="btn btn-primary" type="submit">@Resource.Find</button>
            </span>
        </div>
    </div>
End Using
<br/>
		 
<table class="table table-responsive table-bordered table-striped table-condensed">
<thead>
    <tr>
        
        <th>
            @Html.ActionLink(Resource.txtd_Message, "Index", New With {.sortOrder = ViewBag.MessageSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs col-md-1">
            @Html.ActionLink(Resource.txtd_EstPublie, "Index", New With {.sortOrder = ViewBag.EstPublieSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs col-md-1">
            @Html.ActionLink(Resource.txtd_DateDebut, "Index", New With {.sortOrder = ViewBag.DateDebutSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm col-md-1">
            @Html.ActionLink(Resource.txtd_DateFin, "Index", New With {.sortOrder = ViewBag.DateFinSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
        <th class="hidden-xs hidden-sm col-md-2">
            @Html.ActionLink(Resource.txtd_PointAffichageId, "Index", New With {.sortOrder = ViewBag.PointAffichageIdSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
        </th>
	
        <th></th>
    </tr>
</thead>
<tbody>
@For Each item In Model
    @<tr>
        
			<td>
				@Html.DisplayFor(Function(modelItem) item.Message)
			</td>
			<td class ="hidden-xs">
				@Html.DisplayFor(Function(modelItem) item.EstPublie)
			</td>
			<td class ="hidden-xs">
				@String.Format("{0:d}", item.DateDebut)
			</td>
			<td class ="hidden-xs hidden-sm">
				@String.Format("{0:d}", item.DateFin)
			</td>
			<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.PointAffichage.LIBELLE)
			</td>

	
	    
        <td>
			<a class="btn btn-primary btn-sm" title="@Resource.Edit" href="@Url.Action("Edit", New With {.id = item.Id})">
				<span class="glyphicon glyphicon-pencil"></span>
				<span class="sr-only">@Resource.Edit</span>
			</a>
			<a class="btn btn-primary btn-sm" title="@Resource.Details" href="@Url.Action("Details", New With {.id = item.Id})">
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
</tbody>
</table>
<br />
Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount (@ViewBag.EnregCount Enregistrement(s))

@Html.PagedListPager(Model, Function(page) Url.Action("Index",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
   
