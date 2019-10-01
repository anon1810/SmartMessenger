<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SmartMessenger.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="w3-container w3-row w3-small">
      <div class="w3-col m9">
            <button class="w3-button w3-blue" id="Create" runat="server" onserverclick="Create_ServerClick">Create</button> 
      </div>
      <div class="w3-col m3 w3-center">
         <div class="w3-cell-row">
              <div class="w3-cell">
                  <div class="w3-cell-row">
                      <div class="w3-container w3-cell">
                          <select class="w3-select" name="option" runat="server" id="opSelectSearch">
                            <option value="ID">ค้นหา รหัส</option>
                            <option value="Date">ค้นหา วันที่สร้าง</option>
                            <option value="By">ค้นหา ผู้ขอรับบริการ</option>
                            <option value="Section">ค้นหา แผนก</option>

                            <option value="ContractName">ค้นหา ชื่อผู้ติดต่อ</option>
                            <option value="Address">ค้นหา ที่อยู่ผู้ติดต่อ</option>
                            <option value="Telephone">ค้นหา เบอร์ติดต่อ</option>
                           
                            <option value="OnDate">ค้นหา ส่งภายในวันที่</option>
                            <option value="MessengerName">ค้นหา Messenger</option>
                            <option value="Status">ค้นหา สถานะ</option>
                          </select>
                      </div>
                      <div class="w3-cell">
                          <div class="w3-row">
                              <div class="w3-col" style="width:80%">
                                    <input class="w3-input w3-border"  runat="server"  id="txtSearch" type="text" placeholder="Search.."/>
                              </div>
                              <div class="w3-col" style="width:20%">
                                    <button class="w3-btn w3-border" id="btnSearch" runat="server"  onserverclick="btnSearch_Click"><i class="fa fa-search"></i></button>
                              </div>
                          </div>
                      </div>

                  </div>
              </div>
            </div>
      </div>
    </div>
     

   



    <div class="w3-container w3-padding-16">
            <asp:GridView ID="gvMessager" HeaderStyle-BackColor="#566573" HeaderStyle-ForeColor="White" BorderWidth="0" GridLines="None" runat="server" AutoGenerateColumns="false" CssClass="w3-table-all w3-small" PageSize="14" AllowPaging="true" OnPageIndexChanging="gvMessager_PageIndexChanging" OnRowCommand="gvMessager_RowCommand">
                <Columns>
                    <asp:BoundField DataField="msg_id" HeaderText="รหัส" ItemStyle-Width="20px"/>
                    <asp:BoundField DataField="msg_date" HeaderText="วันที่สร้าง" dataformatstring="{0:dd/MM/yyyy}" ItemStyle-Width="50px"/>
                    <asp:BoundField DataField="msg_by" HeaderText="ผู้ขอรับบริการ" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="msg_section" HeaderText="แผนก" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="msg_phone" HeaderText="เบอร์โทร"  ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="msg_doctype" HeaderText="เอกสาร"  ItemStyle-Width="150px"/>

                    <asp:BoundField DataField="msg_send" HeaderText="ประเภท" ItemStyle-Width="50px"/>
                    <asp:BoundField DataField="msg_priority_normal" HeaderText="ความสำคัญ"  ItemStyle-Width="70px"/>

                    <asp:BoundField DataField="msg_contact_name" HeaderText="ชื่อผู้ติดต่อ"  ItemStyle-Width="70px"/>
                    <asp:BoundField DataField="msg_address" HeaderText="ที่อยู่ผู้ติดต่อ" ItemStyle-Width="200px"/>
                    <asp:BoundField DataField="msg_telephone" HeaderText="เบอร์ติดต่อ" ItemStyle-Width="100px"/>
                    <asp:TemplateField HeaderText="แผนที่" ItemStyle-Width="50px">  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkDownload" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_map") %>'  
                                CommandName="Download"  
                                CssClass="glyphicon glyphicon-file" 
                                Visible='<%# (Convert.ToString(Eval("msg_map")) == "-" || Convert.ToString(Eval("msg_map")) == "แนบแผนทีี่"|| Convert.ToString(Eval("msg_map")) == "") ? Convert.ToBoolean("false"):Convert.ToBoolean("true") %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                                                        
                    <asp:BoundField DataField="msg_on_date" HeaderText="ส่งภายในวันที่"  dataformatstring="{0:dd/MM/yyyy}"  ItemStyle-Width="50px"/>
                    <asp:BoundField DataField="msg_msg_name" HeaderText="Messenger" ItemStyle-Width="50px"/>                   
                    <asp:BoundField DataField="msg_close_status" HeaderText="สถานะ" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="msg_remark" HeaderText="หมายเหตุ" ItemStyle-Width="100px"/>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="10px" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="OutJob" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="OutJob"  
                                CssClass="w3-button w3-round-large w3-border w3-yellow"
                                Text="ปล่อย"
                                Visible='<%# DisplayListOfDevelopers() %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
    </div>
</asp:Content>
