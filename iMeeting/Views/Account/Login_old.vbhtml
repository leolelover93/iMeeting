@ModelType LoginViewModel

@Code
    ViewBag.Title = "Se connecter"
End Code

<h2>@ViewBag.Title.</h2>
<div class="row">
    <div class="col-md-8">
        <section id="loginForm">
            @Using Html.BeginForm("Login", "Account", New With { .ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                @Html.AntiForgeryToken()
                @<text>
                <h4>Utilisez un compte local pour vous connecter.</h4>
                <hr />
                @Html.ValidationSummary(True)
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.UserName, New With {.class = "col-md-2 control-label"})
                    <div class="col-md-10">
                        @Html.TextBoxFor(Function(m) m.UserName, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.UserName)
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(m) m.Password, New With {.class = "col-md-2 control-label"})
                    <div class="col-md-10">
                        @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.Password)
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <div class="checkbox">
                            @Html.CheckBoxFor(Function(m) m.RememberMe)
                            @Html.LabelFor(Function(m) m.RememberMe)
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="Connexion" class="btn btn-default" />
                    </div>
                </div>
                <p>
                    @Html.ActionLink("Enregistrez-vous", "Register") si vous n'avez pas de compte local.
                </p>
                </text>
            End Using
        </section>
    </div>
</div>
@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
