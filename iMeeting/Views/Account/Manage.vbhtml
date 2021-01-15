﻿@Imports Microsoft.AspNet.Identity
@Code
    ViewBag.Title = "Gérer le compte"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
<div class="row">
    <div class="col-md-12">
        @If ViewBag.HasLocalPassword Then
            @Html.Partial("_ChangePasswordPartial")
        Else
            @Html.Partial("_SetPasswordPartial")
        End If
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
