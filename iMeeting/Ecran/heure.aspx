<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="heure.aspx.vb" Inherits="iMeeting.heure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Heure</title>
<meta http-equiv="Refresh" content="20" />
<script src="js/jquery-1.7.1.min.js"></script>

    <style type="text/css">
        body {
            margin: 0; 
            padding:0;
            width:250px;
        }

        .heure {
            width:100%;
            text-align:center;
            font-weight:bold;
            margin-top: 10px; 
            padding:0;
            font-size: 40px; 
            font-family: Cambria; 
            color: #000000;
        }
    </style>
</head>

<body>
            <div class="heure"> <%  Response.Write(Now.ToString("ddd dd/MM"))%> <br />
             <%  Response.Write(Now.ToString("HH:mm"))%> </div>
</body>
</html>
