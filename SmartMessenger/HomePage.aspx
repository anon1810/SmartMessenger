<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SmartMessenger.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="w3-container w3-row w3-small">
      <div class="w3-col m9">
            <button class="w3-button w3-blue" ID="Create" runat="server"  onserverclick="Create_ServerClick">Create</button> 
      </div>
      <div class="w3-col m3 w3-center">
         <div class="w3-cell-row">
              <div class="w3-cell">
                  <div class="w3-cell-row">
                      <div class="w3-container w3-cell">
                          <select class="w3-select" name="option">
                            <option value="ID" selected>Search ID</option>
                            <option value="Date" selected>Search Date</option>
                            <option value="By" selected>Search By</option>
                            <option value="Section" selected>Search Section</option>
                            <option value="Phone" selected>Search Phone</option>
                            <option value="Contract Name" selected>Search Contract</option>
                            <option value="Company" selected>Search Company</option>
                            <option value="Address" selected>Search Address</option>
                            <option value="Telephone" selected>Search Telephone</option>
                            <option value="Send" selected>Search Send</option>
                            <option value="Receive" selected>Search Receive</option>
                            <option value="DocType" selected>Search DocType</option>
                            <option value="Normal" selected>Search Normal</option>
                            <option value="Urgent" selected>Search Urgent</option>
                            <option value="OnDate" selected>Search OnDate</option>
                            <option value="Messenger Name" selected>Search Messenger Name</option>
                            <option value="Status" selected>Search Status</option>
                          </select>
                      </div>
                      <div class="w3-cell">
                          <div class="w3-row">
                              <div class="w3-col" style="width:80%">
                                    <input class="w3-input w3-border"  runat="server"  id="txtSearch" type="text" placeholder="Search..">
                              </div>
                              <div class="w3-col" style="width:20%">
                                    <button class="w3-btn w3-border" ID="btnSearch" runat="server"  onserverclick="btnSearch_Click"><i class="fa fa-search"></i></button>
                              </div>
                          </div>
                      </div>

                  </div>
              </div>
            </div>
      </div>
    </div>
     

   



    <div class="w3-container w3-padding-16">
            <asp:GridView ID="gvMessager" BorderWidth="0" GridLines="None" runat="server" AutoGenerateColumns="false" CssClass="w3-table-all w3-small" PageSize="14" AllowPaging="true" OnPageIndexChanging="gvMessager_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="msg_id" HeaderText="ID" />
                    <asp:BoundField DataField="msg_date" HeaderText="Date" dataformatstring="{0:dd/MM/yyyy}" />
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
                    <asp:BoundField DataField="msg_on_date" HeaderText="OnDate"  dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="msg_msg_name" HeaderText="Messenger Name" />
                    <asp:BoundField DataField="msg_close_status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
    </div>
</asp:Content>
