
@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Heure</title>
    <script src="~/Scripts/jquery-1.10.2.min.js"></script>
    <script src="~/Scripts/clock/moment.min.js"></script>

    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            width: 250px;
        }

        .heure {
            width: 100%;
            text-align: center;
            font-weight: bold;
            margin-top: 10px;
            padding: 0;
            font-size: 40px;
            font-family: Cambria;
            color: #000000;
        }

        .no_network {
            border: 2px solid orange;
        }
    </style>
</head>

<body>
        @*http://keith-wood.name/countdown.html
        layout: '{hnn}{sep}{mnn}{sep}{snn}'
    *@
    <div class="heure" id="heure">
        @Now.ToString("ddd dd/MM") <br />
        <span id="clock">@Now.ToString("HH:mm")</span>
    </div>

    <script>
        var date = new Date(@Now.ToString("yyyy, M - 1, dd, H, m, s"));
        var start = new Date();

        function updateTime () {
            setTimeout(function () {
                var now = moment(date).add(new Date() - start).format('HH:mm');
                $('#clock').text(now);
                updateTime();
            }, 1000);
        };

        /* Lancer la montre */
        updateTime();

        function reload() {

            $.ajax({
                url: "",
                context: document.body,
                success: function (s, x) {
                    $(this).html(s);
                },

                error: function (resultat, statut, erreur) {
                    $('#heure').addClass('no_network');
                    // réessayer dans x ms
                    window.setTimeout(reload, 60000);
                }
            });
        };

        /* recharger après une minute */
        window.setTimeout(reload, 60000);
    </script>
</body>
</html>
