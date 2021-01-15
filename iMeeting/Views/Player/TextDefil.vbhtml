@ModelType list(of String)
@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="Cache-Control" content="no-store" />
    <meta name="viewport" content="width=device-width" />
    <title>Text Defilant</title>
    <script type="text/javascript" src="@Url.Content("~/Scripts/jquery-1.10.2.min.js")"></script>
    <script type="text/javascript" src="@Url.Content("~/Scripts/player/jquery.marquee.js")"></script>

    <style type="text/css">
        .marquee {
            /*width: 300px;  the plugin works for responsive layouts so width is not necessary */
            overflow: hidden;
            /*border: 1px solid #ccc;*/
            font-size: 80px;
            color: orange;
        }
    </style>
</head>
<body>
    <div>
        @*Temps par caractère : 185 ms*@
        <div id="output"></div>
        <div class='marquee'>
        </div>
    </div>

            @If Model.Count > 0 Then
@<text>
    <script type="text/javascript">
        $(document).ready(function () {
            var $mq = $('.marquee');

            var textarray = [
                @For Each item In Model
                    @:'@Html.Raw(item.Replace("'", "\'"))',
                Next
            ];

            function reload() {
                //alert('test reload !');

                $.ajax({
                    url: "",
                    context: document.body,
                    success: function (s, x) {
                        $(this).html(s);
                        //alert('Est-ce qu\'on peut recommencer ?');
                    },

                    error: function (resultat, statut, erreur) {
                        // en principe on ne fait rien
                        //$mq.marquee('resume'); // sinon on relance le marquee
                    }
                });
            };

            var count = 0;
            function showRandomMarquee() {
                if (count >= textarray.length) {
                    // Arrêter le marquee pour éviter l'effet de coupure au milieu
                    $mq.marquee('pause');

                    //location.reload();
                    reload();
                    count = 0;
                }
                var rannum = count++ % textarray.length;
                var texte = textarray[rannum];
                var duree = 15000;
                $mq
                  .marquee('destroy')
                  .bind('finished', showRandomMarquee)
                  .html(texte)
                  .marquee({ duration: duree });
            }

            //Start
            showRandomMarquee();
        });
    </script>
        </text>
                Else
                ' Aucune donnée disponible
        @<script>
            function reload() {
                //alert('test reload no data !');

                $.ajax({
                    url: "",
                    context: document.body,
                    success: function (s, x) {
                        $(this).html(s);
                    },

                    error: function (resultat, statut, erreur) {
                        // réessayer dans x ms
                        window.setTimeout(reload, 60000);
                    }
                });
            };

            window.setTimeout(reload, 60000);
    </script>
        End If


</body>
</html>
