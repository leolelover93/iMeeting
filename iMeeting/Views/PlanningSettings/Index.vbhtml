@ModelType PagedList.IPagedList(Of PlanningSettings)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources

@Code
    ViewData("Title") = Resource.ppla_ListTitle
	
				
End Code

			

<h2>@Resource.ppla_ListTitle</h2>

@Using (Html.BeginForm("Index", "PlanningSettings", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
    @<a class="btn btn-primary" title="@Resource.app_create" href="@Url.Action("Create")">
        <span class="glyphicon glyphicon-plus"></span>
        @Resource.app_create
    </a>
    @<div class="form-group">
        <div class="input-group">
            <div class="input-group-addon hidden-xs">@Resource.app_find_criteria</div>
            @Html.TextBox("SearchString", CStr(ViewBag.CurrentFilter), New With {.class = "form-control", .placeholder = Resource.app_find, .style="max-width:100%"})
            <span class="input-group-btn">
                <button class="btn btn-primary" type="submit">@Resource.app_find</button>
            </span>
        </div>
    </div>
End Using
<br/>
		 
<table class="table table-responsive table-bordered table-striped table-condensed">
<thead>
    <tr>
        
			<th>
				@Html.ActionLink(Resource.ppla_PointAffichageId, "Index", New With {.sortOrder = ViewBag.PointAffichageIdSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs">
				@Html.ActionLink(Resource.ppla_LieuId, "Index", New With {.sortOrder = ViewBag.LieuIdSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs">
				@Html.ActionLink(Resource.ppla_Afficher, "Index", New With {.sortOrder = ViewBag.AfficherSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm text-right">
				@Html.ActionLink(Resource.ppla_ViewType, "Index", New With {.sortOrder = ViewBag.ViewTypeSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm text-right">
				@Html.ActionLink(Resource.ppla_MaxCharsPage, "Index", New With {.sortOrder = ViewBag.MaxCharsPageSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm text-right">
				@Html.ActionLink(Resource.ppla_MaxPageElements, "Index", New With {.sortOrder = ViewBag.MaxPageElementsSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			@*<th class ="hidden-xs hidden-sm">
				@Html.ActionLink(Resource.ppla_LibelleVue, "Index", New With {.sortOrder = ViewBag.LibelleVueSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm">
				@Html.ActionLink(Resource.ppla_Animation, "Index", New With {.sortOrder = ViewBag.AnimationSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm text-right">
				@Html.ActionLink(Resource.ppla_AnimationTime, "Index", New With {.sortOrder = ViewBag.AnimationTimeSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>*@
			<th class ="hidden-xs hidden-sm text-right">
				@Html.ActionLink(Resource.ppla_ViewShowTime, "Index", New With {.sortOrder = ViewBag.ViewShowTimeSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			@*<th class ="hidden-xs hidden-sm">
				@Html.ActionLink(Resource.ppla_LibellePeriode, "Index", New With {.sortOrder = ViewBag.LibellePeriodeSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm">
				@Html.ActionLink(Resource.ppla_BackgoundImage, "Index", New With {.sortOrder = ViewBag.BackgoundImageSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>
			<th class ="hidden-xs hidden-sm">
				@Html.ActionLink(Resource.ppla_DivClassName, "Index", New With {.sortOrder = ViewBag.DivClassNameSortParm, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab})
			</th>*@
	
        <th></th>
    </tr>
</thead>
<tbody>
@For Each item In Model
    @<tr>
        
			<td>
				@Html.DisplayFor(Function(modelItem) item.PointAffichage.LIBELLE)
			</td>

			<td class ="hidden-xs">
				@Html.DisplayFor(Function(modelItem) item.Lieu.LIBELLE)
			</td>

			<td class ="hidden-xs">
				@Html.DisplayFor(Function(modelItem) item.Afficher)
			</td>
			<td class ="hidden-xs hidden-sm text-right">
				@Html.DisplayFor(Function(modelItem) item.ViewType)
			</td>
			<td class ="hidden-xs hidden-sm text-right">
				@Html.DisplayFor(Function(modelItem) item.MaxCharsPage)
			</td>
			<td class ="hidden-xs hidden-sm text-right">
				@Html.DisplayFor(Function(modelItem) item.MaxPageElements)
			</td>
			@*<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.LibelleVue)
			</td>
			<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.Animation)
			</td>
			<td class ="hidden-xs hidden-sm text-right">
				@Html.DisplayFor(Function(modelItem) item.AnimationTime)
			</td>*@
			<td class ="hidden-xs hidden-sm text-right">
				@Html.DisplayFor(Function(modelItem) item.ViewShowTime)
			</td>
			@*<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.LibellePeriode)
			</td>
			<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.BackgoundImage)
			</td>
			<td class ="hidden-xs hidden-sm">
				@Html.DisplayFor(Function(modelItem) item.DivClassName)
			</td>*@
	
	    
        <td>
			<a class="btn btn-primary btn-sm" title="@Resource.app_edit" href="@Url.Action("Edit", New With {.id = item.Id})">
				<span class="glyphicon glyphicon-pencil"></span>
				<span class="sr-only">@Resource.app_edit</span>
			</a>
			<a class="btn btn-primary btn-sm" title="@Resource.app_details" href="@Url.Action("Details", New With {.id = item.Id})">
				<span class="glyphicon glyphicon-list-alt"></span>
				<span class="sr-only">@Resource.app_details</span>
			</a>
            @If item.LieuId <> 0 Or item.PointAffichageId <> 0 Then
                 @<a class="btn btn-primary btn-sm" title="@Resource.app_delete" href="@Url.Action("Delete", New With {.id = item.Id})">
				<span class="glyphicon glyphicon-remove"></span>
				<span class="sr-only">@Resource.app_delete</span>
			</a>   
                End If
			
		</td>
    </tr>
Next
</tbody>
</table>
<br />
Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount (@ViewBag.EnregCount Enregistrement(s))

@Html.PagedListPager(Model, Function(page) Url.Action("Index",
    New With {.page = page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter, .tab = ViewBag.activeTab}))
   
