<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SmartMessenger.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
    <style>

    .bgimg { 
      background: url('/Resource/BG.jpg') no-repeat center center fixed; 
      -webkit-background-size: cover;
      -moz-background-size: cover;
      -o-background-size: cover;
      background-size: cover;
    }

    .con {
        background: rgba(255, 255, 255,0.7); 
    }


    </style>

</head>
<body class="bgimg">
  <div class="w3-container"> 
      <div class="w3-container w3-content w3-card-4 w3-padding-32 w3-display-middle con">
        <h1 class="w3-center w3-wide">Smart Messenger</h1>
        <p class="w3-opacity w3-center"><i>ระบบจัดการรับส่งเอกสาร</i></p>
        <form id="form2" runat="server">
          <div class="w3-section">
            <label><b>Username</b></label>
            <input class="w3-input w3-border w3-margin-bottom w3-text-black" runat="server" id="txtUsername" type="text" placeholder="Enter Username" name="usrname"/>
            <label><b>Password</b></label>
            <asp:TextBox CssClass="w3-input w3-border w3-text-black" ID="txtPsw" runat="server" placeholder="Enter Password" TextMode="Password"/>
            <asp:Button CssClass="w3-button w3-block w3-amber w3-section" Text="Login" id="btnLogin" OnClick="btnLogin_Click" runat="server" />
            <input class="w3-check" type="checkbox" id="chkbRemem" runat="server" checked="checked"/>Remember me            
          </div>
        </form>  
      </div>
<%--      <img src="Resource/footer-Logo.jpg" class="w3-display-bottommiddle" style="width:10%">--%>
  </div>

</body>
</html>
