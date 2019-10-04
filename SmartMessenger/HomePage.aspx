<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SmartMessenger.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="w3-container w3-row w3-small">
      <div class="w3-container w3-col m9">
            <button class="w3-button w3-blue" id="Create" runat="server" onserverclick="Create_ServerClick" type="button">Create</button> 
      </div>
      <div class="w3-container w3-col m3 w3-center">
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
                    <asp:BoundField DataField="msg_id" HeaderText="รหัส"/>
                    <asp:BoundField DataField="msg_date" HeaderText="วันที่สร้าง" dataformatstring="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="msg_by" HeaderText="ผู้ขอรับบริการ"/>
                    <asp:BoundField DataField="msg_section" HeaderText="แผนก"/>
<%--                    <asp:BoundField DataField="msg_phone" HeaderText="เบอร์โทร"  ItemStyle-Width="100px"/>--%>
                    <asp:BoundField DataField="msg_doctype" HeaderText="เอกสาร" />

                    <asp:BoundField DataField="msg_send" HeaderText="ประเภท" />
<%--                    <asp:BoundField DataField="msg_priority_normal" HeaderText="ความสำคัญ" />--%>

                    <asp:BoundField DataField="msg_contact_name" HeaderText="ชื่อผู้ติดต่อ" />
                    <asp:BoundField DataField="msg_address" HeaderText="ที่อยู่ผู้ติดต่อ" />
                    <asp:BoundField DataField="msg_telephone" HeaderText="เบอร์ติดต่อ" />
                    <asp:TemplateField HeaderText="แผนที่">  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkDownload" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_map") %>'  
                                CommandName="lnkDownload"  
                                CssClass="glyphicon glyphicon-file w3-large" 
                                Visible='<%# (Convert.ToString(Eval("msg_map")) == "-" || Convert.ToString(Eval("msg_map")) == "แนบแผนทีี่"|| Convert.ToString(Eval("msg_map")) == "") ? Convert.ToBoolean("false"):Convert.ToBoolean("true") %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                                                        
                    <asp:BoundField DataField="msg_on_date" HeaderText="ส่งภายในวันที่"  dataformatstring="{0:dd/MM/yyyy}"  />
                    <asp:BoundField DataField="msg_msg_name" HeaderText="Messenger" />                   
                    <asp:BoundField DataField="msg_close_status" HeaderText="สถานะ" />
<%--                    <asp:BoundField DataField="msg_remark" HeaderText="หมายเหตุ" ItemStyle-Width="100px"/>--%>
                    <asp:TemplateField HeaderText="" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkView" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkView"  
                                CssClass="fa fa-search w3-large"
                                />
                        </ItemTemplate>  
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="lnkEdit"
                                runat="server" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkEdit"  
                                CssClass="glyphicon glyphicon-edit w3-text-deep-orange w3-large"      
                                Visible='<%# (Convert.ToString(Eval("msg_close_status")) == "รอปล่อยงาน" || Convert.ToString(Eval("msg_close_status")) == "ดำเนินการ") ? Convert.ToBoolean("true"):Convert.ToBoolean("false") %>'                      
                                />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acc" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkOutJob" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkOutJob"  
                                CssClass="w3-button w3-green w3-padding-small"
                                Text="ปล่อย"
                                Visible='<%# ChkShowAcceptBtn() %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Close" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkCloseJob" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkCloseJob"  
                                CssClass="w3-button w3-red w3-padding-small"
                                Text="ปิดงาน"
                                Visible='<%# ChkShowCloseBtn() %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                </Columns>
            </asp:GridView>
    </div>

    <div class="w3-container">
      <div id="id01" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom">
             <div class="w3-container">  
          <span onclick="Closemodal()" class="w3-button w3-large w3-display-topright">&times;</span>
                 </div>
          <div class="w3-container w3-padding-32">        
            <table class="w3-table-all">
              <tr>
              <td  runat="server">รหัส</td>
              <td id="modaltd1" runat="server">รหัส</td>
              </tr><tr>
              <td  runat="server">วันที่ขอรับบริการ</td>
              <td id="modaltd2" runat="server">วันที่ขอรับบริการ</td>
              </tr><tr>
              <td  runat="server">ผู้ขอรับบริการ</td>
              <td id="modaltd3" runat="server">ผู้ขอรับบริการ</td>
              </tr><tr>
              <td  runat="server">แผนก</td>
              <td id="modaltd4" runat="server">แผนก</td>
              </tr><tr>
              <td  runat="server">เบอร์โทร</td>
              <td id="modaltd5" runat="server">เบอร์โทร</td>
              </tr><tr>
              <td  runat="server">เอกสาร</td>
              <td id="modaltd6" runat="server">เอกสาร</td>
              </tr><tr>
              <td  runat="server">ประเภท</td>
              <td id="modaltd7" runat="server">ประเภท</td>
              </tr><tr>
              <td  runat="server">ความสำคัญ</td>
              <td id="modaltd8" runat="server">ความสำคัญ</td>
              </tr><tr>
              <td  runat="server">ชื่อผู้ติดต่อ</td>
              <td id="modaltd9" runat="server">ชื่อผู้ติดต่อ</td>
              </tr><tr>
              <td  runat="server">ที่อยู่อผู้ติดต่อ</td>
              <td id="modaltd10" runat="server">ที่อยู่อผู้ติดต่อ</td>
              </tr><tr>
              <td  runat="server">เบอร์ติดต่อ</td>
              <td id="modaltd11" runat="server">เบอร์ติดต่อ</td>
              </tr><tr>
              <td  runat="server">แผนที่</td>
              <td id="modaltd12" runat="server">แผนที่</td>
              </tr><tr>
              <td  runat="server">ส่งภายในวันที่</td>
              <td id="modaltd13" runat="server">ส่งภายในวันที่</td>
              </tr><tr>
              <td  runat="server">Remark</td>
              <td id="modaltd14" runat="server">Remark</td>
              </tr><tr>
              <td  runat="server">Messenger</td>
              <td id="modaltd15" runat="server">Messenger</td>
              </tr><tr>
              <td  runat="server">สถานะ</td>
              <td id="modaltd16" runat="server">สถานะ</td>
              </tr><tr>
              <td  runat="server">Admin</td>
              <td id="modaltd17" runat="server">Admin</td>
              </tr><tr>
              <td  runat="server">วันที่ปล่อยงาน</td>
              <td id="modaltd18" runat="server">วันที่ปล่อยงาน</td>
              </tr><tr>
              <td  runat="server">วันที่ปิดงาน</td>
              <td id="modaltd19" runat="server">วันที่ปิดงาน</td>
              </tr><tr>
              <td  runat="server">วันที่แก้ไขล่าสุด</td>
              <td id="modaltd20" runat="server">วันที่แก้ไขล่าสุด</td>
              </tr>
            </table>
          </div>

        </div>
      </div>
    </div>

    <script type="text/javascript" language="javascript">
        function Closemodal() {
              document.getElementById("<%=id01.ClientID %>").style.display = 'none'
        }
        function CallingServerSideFunction() {
            PageMethods.GetData();
        }
   </script>


</asp:Content>
