@ModelType LoginViewModel
@imports iMeeting.My.Resources
@Code
    ViewBag.Title = ""
End Code

<h2>@ViewBag.Title</h2><br /><br /><br /><br />
<div class="row">
    <div class="col-md-5">
        <section>
            <img class="img-thumbnail" src="~/Content/Images/log.png " />
        </section>
    </div>
    <div class="col-md-5">
        <section id="loginForm">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title"> <font><span class=" glyphicon glyphicon-info-sign"></span> <b> @Resource.login_form_Title</b> </font> </h3>
                </div>
                <div class="panel-body">
                    @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken()
                        @<text>

                            @Html.ValidationSummary(True)
                            <div class="form-group">
                                <label for="inputLogin" class="col-md-4 control-label">
                                    <font><span class=" glyphicon glyphicon-user"></span> <font>@Resource.login_Libelle_login  </font> </font>
                                </label>
                                @*@Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-2 control-label"})*@
                                <div class="col-md-8">
                                    @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
                                    @Html.ValidationMessageFor(Function(m) m.UserName)
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputPassword" class="col-md-4 control-label">
                                    <font><span class=" glyphicon glyphicon-eye-open"></span> <font>@Resource.login_Libelle_motdepass </font> </font>
                                </label>
                                @*@Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})*@
                                <div class="col-md-8">
                                    @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                                    @Html.ValidationMessageFor(Function(m) m.Password)
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-offset-4 col-md-8">
                                    <div class="checkbox">
                                        @Html.CheckBoxFor(Function(m) m.RememberMe)
                                        @*@Html.LabelFor(Function(m) m.RememberMe)*@
                                        <label><font>@Resource.login_libelle_checkbox </font></label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-offset-4 col-md-8">
                                    <input type="submit" value="@Resource.login_libelle_submit" class="btn btn-primary" />
                                </div>
                            </div>
                            @*<p>
                                    @Html.ActionLink("Enregistrez-vous", "Register") si vous n'avez pas de compte local.
                                </p>*@
                        </text>
                    End Using
                </div>
            </div>
        </section>
    </div>
</div>

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
