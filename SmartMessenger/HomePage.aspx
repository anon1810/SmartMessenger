<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SmartMessenger.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="w3-container w3-row w3-small">
      <div class="w3-container w3-col m8">
            <button class="w3-button w3-blue w3-round-xlarge" id="Create" runat="server" onserverclick="Create_ServerClick" type="button">สร้างใบสั่งงาน</button> 
      </div>
      <div class="w3-container w3-col m4 w3-right">
         <div class="w3-cell-row">
              <div class="w3-cell">
                  <div class="w3-cell-row">
                      <div class="w3-container w3-cell">
                          <select class="w3-select w3-border" name="option" runat="server" id="opSelectSearch">
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
            <asp:GridView ID="gvMessager" HeaderStyle-BackColor="#566573" HeaderStyle-ForeColor="White" BorderWidth="0" GridLines="None" runat="server" AutoGenerateColumns="false" CssClass="w3-table-all w3-small" PageSize="19" AllowPaging="true" OnPageIndexChanging="gvMessager_PageIndexChanging" OnRowCommand="gvMessager_RowCommand">
                <Columns>
                    <asp:BoundField DataField="msg_id" HeaderText="รหัส"/>
<%--                    <asp:BoundField DataField="msg_date" HeaderText="วันที่สร้าง" dataformatstring="{0:dd/MM/yyyy}" />--%>
                    <asp:BoundField DataField="msg_by" HeaderText="ผู้ขอรับบริการ"/>
                    <asp:BoundField DataField="msg_section" HeaderText="แผนก"/>
<%--                    <asp:BoundField DataField="msg_phone" HeaderText="เบอร์โทร"  ItemStyle-Width="100px"/>--%>
                    <asp:BoundField DataField="msg_doctype" HeaderText="เอกสาร" />

<%--                    <asp:BoundField DataField="msg_send" HeaderText="ประเภท" />--%>
<%--                    <asp:BoundField DataField="msg_priority_normal" HeaderText="ความสำคัญ" />--%>

                    <asp:BoundField DataField="msg_contact_name" HeaderText="ชื่อผู้ติดต่อ" />
                    <asp:BoundField DataField="msg_address" HeaderText="ที่อยู่ผู้ติดต่อ" />
                    <asp:BoundField DataField="msg_telephone" HeaderText="เบอร์ติดต่อ" />
                    <%--<asp:TemplateField HeaderText="แผนที่">  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkDownload" 
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_map") %>'  
                                CommandName="lnkDownload"  
                                CssClass="glyphicon glyphicon-file w3-large" 
                                Visible='<%# (Convert.ToString(Eval("msg_map")) == "-" || Convert.ToString(Eval("msg_map")) == "แนบแผนทีี่" || Convert.ToString(Eval("msg_map")) == "แนบแผนที่" || Convert.ToString(Eval("msg_map")) == "No" || Convert.ToString(Eval("msg_map")) == "") ? Convert.ToBoolean("false"):Convert.ToBoolean("true") %>'/>
                            <asp:Label 
                                ID="LinkButton1" 
                                runat="server" 
                                Text="แนบแผนที่"
                                Visible='<%# (Convert.ToString(Eval("msg_map")) == "แนบแผนที่"||Convert.ToString(Eval("msg_map")) == "แนบแผนทีี่") ? Convert.ToBoolean("true"):Convert.ToBoolean("false") %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> --%>
                                                        
                    <asp:BoundField DataField="msg_on_date" HeaderText="ส่งภายในวันที่"  dataformatstring="{0:dd/MM/yyyy}"  />
                    <%--<asp:BoundField DataField="msg_msg_name" HeaderText="Messenger" />   --%>                
                    <asp:BoundField DataField="msg_close_status" HeaderText="สถานะ" />
<%--                    <asp:BoundField DataField="msg_remark" HeaderText="หมายเหตุ" ItemStyle-Width="100px"/>--%>
                    <asp:TemplateField HeaderText="" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkView" 
                                style="text-decoration:none"
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
                                style="text-decoration:none"
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkEdit"  
                                CssClass="glyphicon glyphicon-edit w3-text-deep-orange w3-large"      
                                Visible='<%# (Convert.ToString(Eval("msg_close_status")) == "รอปล่อยงาน" || Convert.ToString(Eval("msg_close_status")) == "ดำเนินการ") ? Convert.ToBoolean("true"):Convert.ToBoolean("false") %>'                      
                                />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkOutJob" 
                                style="text-decoration:none"
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkOutJob"  
                                CssClass="w3-button w3-yellow w3-round-xlarge w3-padding-small"
                                Text="ปล่อย"
                                Visible='<%# ChkShowAcceptBtn() %>'/>
                        </ItemTemplate>  
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="" >  
                        <ItemTemplate>  
                            <asp:LinkButton 
                                ID="lnkCloseJob" 
                                style="text-decoration:none"
                                runat="server" 
                                CausesValidation="False" 
                                CommandArgument='<%# Eval("msg_id") %>' 
                                CommandName="lnkCloseJob"  
                                CssClass="w3-button w3-red w3-round-xlarge w3-padding-small"
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
        <div class="w3-container w3-padding w3-amber">
           <span  onclick="Closemodal()" class="w3-button w3-amber w3-right"><i class="fa fa-remove"></i></span>
          <h5>รายละเอียดเพิ่มเติม</h5>
        </div>
          <div class="w3-container w3-padding-16 w3-small">        
            <table class="w3-table-all">
              <tr>
              <td><label>รหัส :</label></td>
              <td id="modaltd1" runat="server"></td>
              </tr><tr>
              <td><label>วันที่ขอรับบริการ :</label></td>
              <td id="modaltd2" runat="server"></td>
              </tr><tr>                  
              <td><label>ผู้ขอรับบริการ :</label></td>
              <td id="modaltd3" runat="server"></td>
              </tr><tr>
              <td><label>แผนก :</label></td>
              <td id="modaltd4" runat="server"></td>
              </tr><tr>
              <td><label>เบอร์โทร :</label></td>
              <td id="modaltd5" runat="server"></td>
              </tr><tr>
              <td><label>เอกสาร :</label></td>
              <td id="modaltd6" runat="server"></td>
              </tr><tr>
              <td><label>ประเภท :</label></td>
              <td id="modaltd7" runat="server"></td>
              </tr><tr>
              <td><label>ความสำคัญ :</label></td>
              <td id="modaltd8" runat="server"></td>
              </tr><tr>
              <td><label>ชื่อผู้ติดต่อ :</label></td>
              <td id="modaltd9" runat="server"></td>
              </tr><tr>
              <td><label>ที่อยู่อผู้ติดต่อ :</label></td>
              <td id="modaltd10" runat="server"></td>
              </tr><tr>
              <td><label>เบอร์ติดต่อ :</label></td>
              <td id="modaltd11" runat="server"></td>
              </tr><tr>
              <td><label>แผนที่ :</label></td>
              <td id="modaltd12" runat="server">
                  <asp:LinkButton ID="lnkBtnloadMap" runat="server" OnClick="lnkBtnloadMap_Click"></asp:LinkButton>
                  <asp:Label ID="lblMapText" runat="server"></asp:Label>
              </td>
              </tr><tr>
              <td><label>ส่งภายในวันที่ :</label></td>
              <td id="modaltd13" runat="server"></td>
              </tr><tr>
              <td><label>หมายเหตุ :</label></td>
              <td id="modaltd14" runat="server"></td>
              </tr><tr>
              <td><label>พนักงานรับส่งเอกสาร :</label></td>
              <td id="modaltd15" runat="server"></td>
              </tr><tr>
              <td> <label>สถานะ :</label></td>
              <td id="modaltd16" runat="server"></td>
              </tr><tr>
              <td><label>ปล่อยงานโดย :</label></td>
              <td id="modaltd17" runat="server"></td>
              </tr><tr>
              <td><label>ปิดงานโดย :</label></td>
              <td id="modaltd19" runat="server"></td>
              </tr><tr>
              <td><label>แก้ไขล่าสุดโดย :</label></td>
              <td id="modaltd20" runat="server"></td>
              </tr><tr>
              <td><label>หลักฐานใบงาน :</label></td>
              <td id="modaltd21" runat="server">
                  <asp:LinkButton ID="lnkBtnloadReport" runat="server" OnClick="lnkBtnloadReport_Click"></asp:LinkButton>
              </td>
              </tr>
            </table>
          </div>
        </div>
      </div>
    </div>

    <div class="w3-container">
      <div id="modelUplodeReport" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom">
        <div class="w3-container w3-padding w3-amber">
           <span  onclick="ClosemodelUplodeReport()" class="w3-button w3-amber w3-right"><i class="fa fa-remove"></i></span>
          <h4>เอกสารหลักฐาน</h4>
        </div>
          <div class="w3-container w3-padding-16">        
              <label>กรุณาแนบใบงานเพื่อปิดงาน</label>
              <label>เลขที่อ้างอิง</label>
              <label id="lblRefnumber" runat="server">xxx</label>
              <br/>
              <asp:FileUpload id="FileUploadReport" runat="server" CssClass = "w3-input w3-border" />  
              <p>
              <input id="chkBoxClose" runat="server" class="w3-check" type="checkbox"/>
              <label>ปิดงานโดยไม่แนบใบงาน</label></p>
              <asp:Button Text="ตกลง" runat="server" CssClass="w3-button w3-blue w3-round-xlarge" ID="btnSubmit"  OnClick="btnSubmit_Click" OnClientClick="return validate();" />
          </div>
        </div>
      </div>
    </div>

    <div id="waringModeEdit" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom">
        <div class="w3-container w3-padding w3-red">
           <span  onclick="CloswaringModelEdit()" class="w3-button w3-red w3-right"><i class="fa fa-remove"></i></span>
          <h4>คำเตือน</h4>
        </div>
          <div class="w3-container w3-padding-16">        
              <p>ไม่สามารถแก้ไขได้เนื่องจากอยู่ในระหว่างดำเนินการ กรุณาติดต่อ Admin</p>
          </div>
        </div>
      </div>

    <script type="text/javascript" language="javascript">
        function Closemodal() {
              document.getElementById("<%=id01.ClientID %>").style.display = 'none'
        }
        function CloswaringModelEdit() {
              document.getElementById("<%=waringModeEdit.ClientID %>").style.display = 'none'
        }
        function ClosemodelUplodeReport() {
              document.getElementById("<%=modelUplodeReport.ClientID %>").style.display = 'none'
        }
        function CallingServerSideFunction() {
            PageMethods.GetData();
        }
        function validate() {
            var chkFileUploadReport = document.getElementById("<%=FileUploadReport.ClientID %>").value;
            var chkClose = document.getElementById("<%=chkBoxClose.ClientID %>").checked;;
            if (chkFileUploadReport == "" && chkClose == false) {
                  alert("กรุณาทำรายการ");
                  return false;
              }
            return true;
        }
   </script>


</asp:Content>
