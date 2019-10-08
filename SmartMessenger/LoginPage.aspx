<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SmartMessenger.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
</head>
<body class="w3-black">
  <div class="w3-container w3-padding-64" id="tour">
    <div class="w3-container w3-content w3-padding-64" style="max-width:700px">


      <h1 class="w3-center wide">Smart Messenger</h1>
      <p class="w3-opacity w3-center"><i>ระบบจัดการเรื่องส่งเอกสาร</i></p><br>

      <form id="form2" runat="server" class="w3-container">
        <div class="w3-section">
          <label><b>Username</b></label>
          <input class="w3-input w3-border w3-margin-bottom w3-text-black" runat="server" id="txtUsername" type="text" placeholder="Enter Username" name="usrname" required>
          <label><b>Password</b></label>
          <asp:TextBox CssClass="w3-input w3-border w3-text-black" ID="txtPsw" runat="server" placeholder="Enter Password" TextMode="Password" required/>
          <asp:Button CssClass="w3-button w3-block w3-amber w3-section w3-padding" Text="Login" id="btnLogin" OnClick="btnLogin_Click" runat="server" />
          <input class="w3-check w3-margin-top" type="checkbox" id="chkbRemem" runat="server" checked="checked"> Remember me
        </div>
      </form>

      <div class="w3-container w3-border-top w3-padding-16">
        <span class="w3-right w3-padding w3-hide-small">Forgot <a id="Forgot" href="#" onclick="myFunction();">password?</a></span>
      </div>    
    </div>

      <img src="Resource/footer-Logo.jpg" class="w3-display-bottommiddle" style="width:10%">
  </div>

    <script>
        function myFunction() {
            var x = document.getElementById("Forgot");
            alert("กรุณาติดต่อผู้ดูแลระบบ");
            return false;
        }
    </script>


</body>
</html>
