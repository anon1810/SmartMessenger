<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SmartMessenger.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w3-cell-row w3-container w3-padding-top">
      <div class="w3-container w3-cell w3-left">
        <select class="w3-select" name="option">
          <option value="1" selected>แสดงข้อมูลทั้งหมด</option>
          <option value="2">แสดงข้อมูลเฉพาะฉัน</option>
        </select>
      </div>
      <div class="w3-container w3-cell w3-right">
          <div class="w3-row">
              <div class="w3-col" style="width:70%">
                    <input class="w3-input w3-border"  runat="server"  id="txtSearch" type="text" placeholder="Search.."></p>
              </div>
              <div class="w3-col" style="width:30%">
                    <button class="w3-btn w3-border" ID="btnSearch" runat="server"  onserverclick="btnSearch_Click"><i class="fa fa-search"></i></button>
              </div>
          </div>
      </div>
    </div>

    <div class="w3-container">
            <asp:GridView ID="gvMessager" BorderWidth="0" GridLines="None" runat="server" AutoGenerateColumns="false" CssClass="w3-table-all w3-small" PageSize="20" AllowPaging="true" OnPageIndexChanging="gvMessager_PageIndexChanging">
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
</asp:Content>
