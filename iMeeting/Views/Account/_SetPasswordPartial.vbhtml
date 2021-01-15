﻿@ModelType ManageUserViewModel

<p class="text-info">
    Vous n’avez pas de nom d’utilisateur/mot de passe local pour ce site. Ajoutez un compte
    local pour pouvoir vous connecter sans nom de connexion externe.
</p>

@Using Html.BeginForm("Manage", "Account", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
    @Html.AntiForgeryToken()

    @<text>
        <h4>Créer une connexion locale</h4>
        <hr />
         @Html.ValidationSummary()
         <div class="form-group">
            @Html.LabelFor(Function(m) m.NewPassword, New With {.class = "col-md-2 control-label"})
            <div class="controls">
                @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "form-control"})
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(m) m.ConfirmPassword, New With {.class = "col-md-2 control-label"})
            <div class="controls">
                @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control"})
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Définir le mot de passe" class="btn btn-default" />
            </div>
        </div>
    </text>
End Using
