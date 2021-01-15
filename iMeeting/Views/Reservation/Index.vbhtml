@ModelType PagedList.IPagedList(Of RESERVATION)
@Imports PagedList.Mvc
@Imports iMeeting.My.Resources
@Code
    ViewData("Title") = Resource.rese_ListTitle
    
    Dim activeTab As String = "tab" & ViewBag.activeTab
    If String.IsNullOrEmpty(ViewBag.activeTab) Then
        activeTab = "tab1"
    End If
				
End Code


@Helper selectedItem(menu As String, activeMenu As String)
    If Menu = activeMenu Then
	@:class="active""
    End If
End Helper

<h2>@Resource.rese_ListTitle</h2>

@Using (Html.BeginForm("Index", "Reservation", FormMethod.Get, New With {.class = "form-inline", .role = "form"}))
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

<ul class="nav nav-tabs">
    <li @selectedItem("tab1", activeTab)><a href="@Url.Action("Index", "Reservation", New With {.tab = 1})"><i class="fa fa-check-square-o"></i> Réservations à valider</a></li>
    <li @selectedItem("tab2", activeTab)><a href="@Url.Action("Index", "Reservation", New With {.tab = 2})"><i class="fa fa-bars"></i> Toutes les réservations</a></li>
</ul>

<table class="table table-responsive table-bordered table-striped table-condensed">
    <thead>
        <tr>

            <th>
                @Html.ActionLink(Resource.rese_IdLieu, "Index", New With {.sortOrder = ViewBag.IdLieuSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs">
                @Html.ActionLink(Resource.rese_Motif, "Index", New With {.sortOrder = ViewBag.LibelleSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm col-md-1">
                @Html.ActionLink(Resource.rese_DateDebut, "Index", New With {.sortOrder = ViewBag.DateDebutSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm col-md-1">
                @Html.ActionLink(Resource.rese_HeureDebut, "Index", New With {.sortOrder = ViewBag.HeureDebutSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.rese_CurrentState, "Index", New With {.sortOrder = ViewBag.EtatSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>

            <th class="hidden-xs hidden-sm">
                @Html.ActionLink(Resource.rese_president, "Index", New With {.sortOrder = ViewBag.NbrePersSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.LIEUX.LIBELLE)
                </td>
                <td class="hidden-xs">
                    @Html.DisplayFor(Function(modelItem) item.Objet)
                </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.DATE_DEBUT)
                </td>
                
    <td class="hidden-xs hidden-sm">        
        @Html.DisplayFor(Function(modelItem) item.HEURE_DEBUT) -
        @Html.DisplayFor(Function(modelItem) item.HEURE_FIN)
    </td>
    <td class="hidden-xs hidden-sm">
        @Code
            Dim actionName As String = "<Unknow State>"
            If item.CurrentState = EnumCurrentState.CanceledPending Then
                actionName = "Annulation en attente"
            ElseIf item.CurrentState = EnumCurrentState.ReportedPending Then
                actionName = "Report en attente"
            ElseIf item.CurrentState <> EnumCurrentState.Initial Then
                actionName = item.ETAT.ToString & " - " & item.CurrentState.ToString
            ElseIf item.ETAT <> EnumReservation.Confirmed Then
                actionName = "Validation en attente"
            Else
                actionName = "Validé"
            End If
        End Code

        @actionName
    </td>
                <td class="hidden-xs hidden-sm">
                    @Html.DisplayFor(Function(modelItem) item.PresidentSeance)
                </td>
                <td>
                    <a class="btn btn-primary btn-sm" title="@Resource.Edit" href="@Url.Action("Edit", New With {.id = item.ID_RESERVATION})">
                        <span class="glyphicon glyphicon-pencil"></span>
                        <span class="sr-only">@Resource.Edit</span>
                    </a>
                    <a class="btn btn-primary btn-sm" title="@Resource.Details" href="@Url.Action("Details", New With {.id = item.ID_RESERVATION})">
                        <span class="glyphicon glyphicon-list-alt"></span>
                        <span class="sr-only">@Resource.Details</span>
                    </a>
                    @If User.IsInRole("SCC") Then
                        @<a class="btn btn-primary btn-sm" title="@Resource.Delete" href="@Url.Action("Delete", New With {.id = item.ID_RESERVATION})">
                            <span class="glyphicon glyphicon-remove"></span>
                            <span class="sr-only">@Resource.Delete</span>
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
