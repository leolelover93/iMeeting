<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EcranPlanning.aspx.vb" Inherits="iMeeting.EcranPlanning" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Refresh" content="127" />
<link rel="stylesheet" type="text/css" href="../Content/bootstrap.css" />
<script src="js/jquery-1.7.1.min.js"></script>
</head>
    <script type="text/javascript" language="javascript">

        function hide_table() {
            $('div#T1').hide(1000);
        }

        function show_table() {
            $('div#T1').show(900);
        }
        function show_table2() {
            $('div#T2').slideDown(1500);
        }
        function hide_table2() {
            $('div#T2').slideUp(1000);
        }
        function show_table3() {
            $('div#T3').fadeIn(1500);
        }
        function hide_table3() {
            $('div#T3').fadeOut(1000);
        }
        function show_table4() {
            $('div#T4').slideDown(1500);
        }
        function hide_table4() {
            $('div#T4').slideUp(1000);
        }
        function transition() {
            $("#bdy").css({ background: "url('video.jpg') no-repeat" });
        }

</script>
<style>
    #T1 {
        display: none;
    }
    #T2 {
        display: none;
    }

    #T3 {
        display: none;
    }
    #T4 {
        display: none;
    }
</style>
<body style=" background-image:url(bg.jpg); background-repeat:no-repeat ">
     <script type='text/javascript' language='javascript'>
         setTimeout('show_table()', '1');
         setTimeout('hide_table()', '30000');
         setTimeout('show_table2()', '31000');
         setTimeout('hide_table2()', '62000');
         setTimeout('show_table3()', '63000');
         setTimeout('hide_table3()', '94000');
         setTimeout('show_table4()', '95000');
         setTimeout('hide_table4()', '126000');
         //setTimeout('transition()', '29000');
        </script>
    <form id="form1" runat="server">
    <div id="T1" style="background-image :url(bg.png);background-repeat :no-repeat ; height:720px; width :100%"><br />
    
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label  style="font-size:50px; font-family :Cambria; color:#c83b05 "><b> Planning Prévisionnel </b></label>
       </div>
       <div class="col-md-8"></div>
       <div class="col-md-4">
        <label " style="margin-top: -25px; font-size: 50px; font-family: Cambria; color: #c83b05;"><b> Salle 219 </b></label>
       </div>
         <div class="col-md-7"></div>
       <div class="col-md-5">
            <%  Dim Datelundi = Now.Date.AddDays(1 - Now.DayOfWeek)
                Dim Datevendredi = Datelundi.AddDays(4)
            %>
        <label " style="margin-top: -25px; font-size: 25px; font-family: Cambria; color: #000000;"><b> DU&nbsp;&nbsp; <%  Response.Write(Datelundi.Date.Day)%>-<%  Response.Write(Datelundi.Date.Month)%>-<%  Response.Write(Datelundi.Date.Year)%>&nbsp;&nbsp; AU &nbsp;&nbsp;    <% Response.Write(Datevendredi.Date.Day)%>-<% Response.Write(Datevendredi.Date.Month)%>-<% Response.Write(Datevendredi.Date.Year)%> </b></label>
       </div>
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [DATE], [HEURE], [Objet], [PresidentSeance] FROM [v_Planning] WHERE (([libelle_lieu] = @libelle_lieu) AND ([DATE] >= @Datelundi) AND ([DATE] <= @Datevendredi)) ORDER BY [DATE_DEBUT], [HEURE_DEBUT]">
              <SelectParameters>
                  <asp:Parameter  DefaultValue="29/09/2014"  Name="Datelundi" Type="Datetime"  />
                  <asp:Parameter  DefaultValue="04/10/2014"  Name="Datevendredi" Type="Datetime"  />
                  <asp:Parameter DefaultValue="SALLE 219" Name="libelle_lieu" Type="String" />
              </SelectParameters>
        </asp:SqlDataSource>
          <asp:GridView ID="GridView1" runat="server" BorderColor="White" BorderStyle="Solid" CellPadding="7" Width="1300px" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="Vertical"  ShowHeaderWhenEmpty="True" BorderWidth="1px" Height="301px"  AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#FFCCCC" ForeColor="#000000" Font-Size="16pt" />
              <Columns>
                  <asp:BoundField HeaderText="Date"  DataField="DATE" SortExpression="DATE" ReadOnly="True" />
                  <asp:BoundField HeaderText="Heure" DataField="HEURE" SortExpression="HEURE" ReadOnly="True" />
                  <asp:BoundField HeaderText="Objet" DataField="Objet" SortExpression="Objet" />
                  <asp:BoundField HeaderText="President de Séance" DataField="PresidentSeance" SortExpression="PresidentSeance" />
              </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#CC3300" Font-Bold="True" ForeColor="White" Font-Names="Calibri Light" Font-Size="18pt" BorderColor="White" BorderStyle="None" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" Font-Names="Calibri Light" Font-Size="16pt" />
        </asp:GridView>
        <%--<asp:SqlDataSource ID="ConsultePlanning" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ID_RESERVATION,DATE_DEBUT, HEURE_DEBUT, HEURE_FIN, OBJET, PresidentSeance FROM RESERVATION, LIEUX WHERE RESERVATION.ID_LIEU = LIEUX.ID_LIEU AND LIEUX.LIBELLE ='SALLE 219'" UpdateCommandType="Text">
        </asp:SqlDataSource>--%>
    </div>
        

       <%-- ############################################# 117 ############################################--%>

        <div id="T2" style="background-image :url(bg.png);background-repeat :no-repeat ; height:720px;" onload ="PeriodeWeek()"><br /><br />
    
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label  style="font-size:50px; font-family :Cambria; color:#c83b05 "><b> Planning Prévisionnel </b></label>
       </div>
       <div class="col-md-8"></div>
       <div class="col-md-4">
        <label " style="margin-top: -25px; font-size: 50px; font-family: Cambria; color: #c83b05;"><b> Salle 117 </b></label>
       </div>

          <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [DATE_DEBUT], [HEURE_DEBUT], [Objet], [PresidentSeance] FROM [v_Reservation] WHERE (([libelle_lieu] = @libelle_lieu) AND ([ETAT] = @ETAT)) ORDER BY [DATE_DEBUT], [HEURE_DEBUT]">
              <SelectParameters>
                  <asp:Parameter DefaultValue="SALLE 117" Name="libelle_lieu" Type="String" />
                  <asp:Parameter DefaultValue="2" Name="ETAT" Type="Int32" />
              </SelectParameters>
        </asp:SqlDataSource>
          <asp:GridView ID="GridView2" runat="server" BorderColor="White" BorderStyle="Solid" CellPadding="7" Width="1300px" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="Vertical"  ShowHeaderWhenEmpty="True" BorderWidth="1px" Height="301px"  AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#FFCCCC" ForeColor="#000000" Font-Size="16pt" />
              <Columns>
                  <asp:BoundField HeaderText="DATE_DEBUT"  DataField="DATE_DEBUT" SortExpression="DATE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="HEURE_DEBUT" DataField="HEURE_DEBUT" SortExpression="HEURE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="Objet" DataField="Objet" SortExpression="Objet" />
                  <asp:BoundField HeaderText="PresidentSeance" DataField="PresidentSeance" SortExpression="PresidentSeance" />
              </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#CC3300" Font-Bold="True" ForeColor="White" Font-Names="Calibri Light" Font-Size="18pt" BorderColor="White" BorderStyle="None" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" Font-Names="Calibri Light" Font-Size="16pt" />
        </asp:GridView>
        <%--<asp:SqlDataSource ID="ConsultePlanning" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ID_RESERVATION,DATE_DEBUT, HEURE_DEBUT, HEURE_FIN, OBJET, PresidentSeance FROM RESERVATION, LIEUX WHERE RESERVATION.ID_LIEU = LIEUX.ID_LIEU AND LIEUX.LIBELLE ='SALLE 219'" UpdateCommandType="Text">
        </asp:SqlDataSource>--%>
    </div>

         <%-- ############################################# rdc ############################################--%>

        <div id="T3" style="background-image :url(bg.png);background-repeat :no-repeat ; height:720px;" onload ="PeriodeWeek()"><br /><br />
    
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label  style="font-size:50px; font-family :Cambria; color:#c83b05 "><b> Planning Prévisionnel </b></label>
       </div>
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label " style="margin-top: -25px; font-size: 50px; font-family: Cambria; color: #c83b05;"><b> Salle Rez-de-Chaussée </b></label>
       </div>

          <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [DATE_DEBUT], [HEURE_DEBUT], [Objet], [PresidentSeance] FROM [v_Reservation] WHERE (([libelle_lieu] = @libelle_lieu) AND ([ETAT] = @ETAT)) ORDER BY [DATE_DEBUT], [HEURE_DEBUT]">
              <SelectParameters>
                  <asp:Parameter DefaultValue="Salle Rez-de-Chaussée" Name="libelle_lieu" Type="String" />
                  <asp:Parameter DefaultValue="2" Name="ETAT" Type="Int32" />
              </SelectParameters>
        </asp:SqlDataSource>
          <asp:GridView ID="GridView3" runat="server" BorderColor="White" BorderStyle="Solid" CellPadding="7" Width="1300px" DataSourceID="SqlDataSource3" ForeColor="#333333" GridLines="Vertical"  ShowHeaderWhenEmpty="True" BorderWidth="1px" Height="301px"  AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#FFCCCC" ForeColor="#000000" Font-Size="16pt" />
              <Columns>
                  <asp:BoundField HeaderText="DATE"  DataField="DATE_DEBUT" SortExpression="DATE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="HEURE" DataField="HEURE_DEBUT" SortExpression="HEURE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="Objet" DataField="Objet" SortExpression="Objet" />
                  <asp:BoundField HeaderText="President de Seance" DataField="PresidentSeance" SortExpression="PresidentSeance" />
              </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#CC3300" Font-Bold="True" ForeColor="White" Font-Names="Calibri Light" Font-Size="18pt" BorderColor="White" BorderStyle="None" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" Font-Names="Calibri Light" Font-Size="16pt" />
        </asp:GridView>
        <%--<asp:SqlDataSource ID="ConsultePlanning" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ID_RESERVATION,DATE_DEBUT, HEURE_DEBUT, HEURE_FIN, OBJET, PresidentSeance FROM RESERVATION, LIEUX WHERE RESERVATION.ID_LIEU = LIEUX.ID_LIEU AND LIEUX.LIBELLE ='SALLE 219'" UpdateCommandType="Text">
        </asp:SqlDataSource>--%>
    </div>

         <%-- ############################################# conseils ############################################--%>

        <div id="T4" style="background-image :url(video.jpg);background-repeat :no-repeat ; height:720px;" onload ="PeriodeWeek()"><br /><br />
    
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label  style="font-size:50px; font-family :Cambria; color:#05999d "><b> Planning Prévisionnel </b></label>
       </div>
       <div class="col-md-6"></div>
       <div class="col-md-6">
        <label " style="margin-top: -25px; font-size: 50px; font-family: Cambria; color:#05999d;"><b> Salle des Conseils </b></label>
       </div>

          <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [DATE_DEBUT], [HEURE_DEBUT], [Objet], [PresidentSeance] FROM [v_Reservation] WHERE (([libelle_lieu] = @libelle_lieu) AND ([ETAT] = @ETAT)) ORDER BY [DATE_DEBUT], [HEURE_DEBUT]">
              <SelectParameters>
                  <asp:Parameter DefaultValue="SALLE DES CONSEILS" Name="libelle_lieu" Type="String" />
                  <asp:Parameter DefaultValue="2" Name="ETAT" Type="Int32" />
              </SelectParameters>
        </asp:SqlDataSource>
          <asp:GridView ID="GridView4" runat="server" BorderColor="White" BorderStyle="Solid" CellPadding="7" Width="1300px" DataSourceID="SqlDataSource4" ForeColor="#333333" GridLines="Vertical"  ShowHeaderWhenEmpty="True" BorderWidth="1px" Height="301px"  AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="#c3f4f8" ForeColor="#000000" Font-Size="16pt" />
              <Columns>
                  <asp:BoundField HeaderText="DATE"  DataField="DATE_DEBUT" SortExpression="DATE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="HEURE" DataField="HEURE_DEBUT" SortExpression="HEURE_DEBUT" ReadOnly="True" />
                  <asp:BoundField HeaderText="Objet" DataField="Objet" SortExpression="Objet" />
                  <asp:BoundField HeaderText="President de Seance" DataField="PresidentSeance" SortExpression="PresidentSeance" />
              </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#05999d" Font-Bold="True" ForeColor="White" Font-Names="Calibri Light" Font-Size="18pt" BorderColor="White" BorderStyle="None" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" Font-Names="Calibri Light" Font-Size="16pt" />
        </asp:GridView>
        <%--<asp:SqlDataSource ID="ConsultePlanning" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ID_RESERVATION,DATE_DEBUT, HEURE_DEBUT, HEURE_FIN, OBJET, PresidentSeance FROM RESERVATION, LIEUX WHERE RESERVATION.ID_LIEU = LIEUX.ID_LIEU AND LIEUX.LIBELLE ='SALLE 219'" UpdateCommandType="Text">
        </asp:SqlDataSource>--%>
    </div>
    </form>
</body>
</html>
