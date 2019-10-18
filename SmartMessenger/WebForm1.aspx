<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmartMessenger.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart.js</title>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css"/>
    <script src="https://code.jquery.com/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css"/>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <style>
        .ui-autocomplete  
            {
                font-size:11px;
                text-align:left;
            }
    </style>
    <script>
        $(function () {
            $('#<%=txtCompanyName.ClientID%>').autocomplete({
                minLength: 1,
                source: function (request, response) {
                    $.ajax({
                        url: "WebForm1.aspx/GetCompanyName",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d,function(item)
                            {
                                return {value : item}
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("error");
                        }
                    });
                }
            });
        });
    </script>
</head>
<body>

<form id="frm" runat="server">
    <asp:TextBox ID="txtCompanyName" runat="server" Width="350px" CssClass="textboxAuto"  Font-Size="12px" />
</form>

    
</body>
</html>
