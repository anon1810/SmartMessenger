<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmartMessenger.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>

<script src="jquery-3.3.1.min.js">
</script>
<script language="javascript" type="text/javascript">
    $.expr[":"].containsNoCase = function (el, i, m) {
        var search = m[3];
        if (!search) return false;
        return eval("/" + search + "/i").test($(el).text());
    };

    $(document).ready(function () {
        $('#txtSearch').keyup(function () {
            if ($('#txtSearch').val().length > 1) {
                $('#searchGridView tr').hide();
                $('#searchGridView tr:first').show();
                $('#searchGridView tr td:containsNoCase(\'' + $('#txtSearch').val() + '\')').parent().show();
            } else if ($('#txtSearch').val().length == 0) {
                resetSearchValue();
            }
        });

        $('#txtSearch').keyup(function (event) {
            if (event.keyCode == 27) {
                resetSearchValue();
            }
        });
    });

    function resetSearchValue() {
        $('#txtSearch').val('');
        $('#searchGridView tr').show();
        $('#txtSearch').focus();
    }

</script>
<html>
<body>
    <input class="w3-input w3-border w3-round"  runat="server"  id="txtSearch" type="text" placeholder="Search.."></p>
    <form id="form1" runat="server" class="w3-padding-16">
        <div class="w3-container">
            <asp:GridView ID="searchGridView" BorderWidth="0" GridLines="None" runat="server" AutoGenerateColumns="false" CssClass="w3-table-all w3-small" PageSize="15" AllowPaging="true" OnPageIndexChanging="gvMessager_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="msg_id" HeaderText="ID" />
                    <asp:BoundField DataField="msg_date" HeaderText="Date" />
                    <asp:BoundField DataField="msg_by" HeaderText="By" />
                    <asp:BoundField DataField="msg_section" HeaderText="Section" />
                    <asp:BoundField DataField="msg_phone" HeaderText="Phone" />
                    <asp:BoundField DataField="msg_contact_name" HeaderText="Contract Name" />
                    <asp:BoundField DataField="msg_company" HeaderText="Company" />
                    <asp:BoundField DataField="msg_address" HeaderText="Address" />
                    <asp:BoundField DataField="msg_telephone" HeaderText="Telephone" />
                    <asp:BoundField DataField="msg_send" HeaderText="Send" />
                    <asp:BoundField DataField="msg_receive" HeaderText="Receive" />
                    <asp:BoundField DataField="msg_doctype" HeaderText="DocType" />
                    <asp:BoundField DataField="msg_priority_normal" HeaderText="Normal" />
                    <asp:BoundField DataField="msg_priority_urgent" HeaderText="Urgent" />
                    <asp:BoundField DataField="msg_on_date" HeaderText="OnDate" />
                    <asp:BoundField DataField="msg_msg_name" HeaderText="Messenger Name" />
                    <asp:BoundField DataField="msg_close_status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
