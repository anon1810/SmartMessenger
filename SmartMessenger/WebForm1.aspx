<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmartMessenger.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart.js</title>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
    <script src="https://code.jquery.com/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" type="text/javascript"></script>
    <script type="text/javascript">


     </script>
</head>
<body>
<asp:Literal ID="ltChart" runat="server"></asp:Literal>
    <form id="form1" runat="server">   
    <div class="w3-row">
      <div class="w3-col w3-container" style="width:20%">
          <p>20%</p>
      </div>
      <div class="w3-col w3-container" style="width:60%">
          <canvas id="report1"></canvas>
      </div>
      <div class="w3-col  w3-container" style="width:20%">
          <p>20%</p>
      </div>
    </div>

    <div>
        <canvas id="report2" width="600" height="300"></canvas>
    </div>
    <div>
        <canvas id="report3" width="600" height="300"></canvas>
    </div>

    </form>
</body>
</html>
