@Code
    Layout = Nothing
End Code

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" />
    <title>SOGIR</title>
    <style type="text/css">
        body, html {
            margin: 0px;
            padding: 0px;
        }

        .main {
            padding-left: 35px;
        }

        .defil {
            float: left;
        }

        .heure {
            margin-top: -15px;
            float: right;
            text-align: center;
        }
    </style>


</head>
<body>
    <div class="main">
        <iframe src="@ViewBag.PlanningUrl" width="1920" height="940" scrolling="no" style=" border :0px;"> </iframe>
    </div>
    <div class="defil">
        <img src="~/Content/Images/TEAMlogo.png" width="210" height="120" alt="Powered by TeamIS" style="margin-left:40px" />
        <iframe src="@ViewBag.TextDefilUrl" width="1350" height="120" scrolling="no" style=" border: 0px inset orange; "> </iframe>
    </div>
    <div class="heure">
        <iframe src="@ViewBag.HeureUrl" width="250" scrolling="no" style=" border :0px;"> </iframe>
    </div>
</body>
</html>
