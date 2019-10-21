<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreateMessengerPage.aspx.cs" Inherits="SmartMessenger.CreateMessengerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="w3-row">
        <div class="w3-col" style="width:20%">
            &nbsp;
        </div>
        <div class="w3-col w3-light-greya" style="width:60%">
            <div class="w3-cell-row">
              <div class="w3-cell">
                <label>ผู้ขอรับบริการ</label>
                <asp:TextBox CssClass="w3-input w3-border" Enabled="false" runat="server" id="txtByCreateP" OnTextChanged="txtByCreateP_TextChanged" AutoPostBack="true"/>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <label>แผนก</label>
                <input class="w3-input w3-border" disabled="disabled" type="text" value="-" runat="server" id="txtSectionCreateP"/>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <label>เบอร์ติดต่อ</label>
                <input class="w3-input w3-border" type="text" runat="server" id="txtByPhoneCreateP"/>
              </div>
            </div>
            <p></p>
            <label>ประเภทเอกสารที่จะส่ง</label>
            <div class="w3-cell-row">
              <div class="w3-cell">
                <select class="w3-select w3-border" name="option" runat="server" id="opSendCreateP" onchange="selectSend()">
                  <option value="0" selected="selected">&nbsp;-</option>
                  <option value="สัญญา">สัญญา</option>
                  <option value="ใบเสนอราคา">ใบเสนอราคา</option>
                  <option value="เช็ค">เช็ค</option>
                  <option value="ตั๋วอาวัล">ตั๋วอาวัล</option>
                  <option value="อื่นๆ">อื่นๆ</option>
                </select>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtSendCreateP" disabled="disabled"/>
              </div>
            </div>
            <p></p>
            <label>ประเภทเอกสารที่จะรับ</label>
            <div class="w3-cell-row">
              <div class="w3-cell">
                <select class="w3-select w3-border" name="option" runat="server" id="opReceiveCreateP" onchange="selectRecieve()">
                  <option value="0" selected="selected">&nbsp;-</option>
                  <option value="สัญญา">สัญญา</option>
                  <option value="ใบเสนอราคา">ใบเสนอราคา</option>
                  <option value="เช็ค">เช็ค</option>
                  <option value="ตั๋วอาวัล">ตั๋วอาวัล</option>
                  <option value="อื่นๆ">อื่นๆ</option>
                </select>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtReceiveCreateP" disabled="disabled"/>
              </div>
            </div>

            <p></p>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="nmCreateP" value="1" checked/>
              <label>ปกติ</label>
            </div>
            <div class="w3-cell">&nbsp;&nbsp;</div>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="urCreateP" value="2"/>
              <label>ด่วน</label>
            </div>
            <p></p>
           
            <div class="w3-cell-row">
              <div class="w3-cell">
                  <label>ชื่อผู้ติดต่อ</label>
                  <input class="w3-input w3-border"  type="text" runat="server" id="txtContratNameCreateP" required="required"/>
              </div>
              <div class="w3-cell">
                 &nbsp;
              </div>
              <div class="w3-cell">
                <label>เบอร์ติดต่อ</label>
                <input class="w3-input w3-border" type="text" runat="server" id="txtContratPhoneCreateP" required="required"/>
              </div>
            </div>
            <p>
              <label>ที่อยู่ผู้ติดต่อ</label>
              <textarea class = "w3-input  w3-border"  runat="server" id="txtContratAddrCreateP" required="required"></textarea>
            </p>
            <p>
              <label>แนบไฟล์แผนที่</label><br/>
              <asp:FileUpload id="FileUploadMap" runat="server" CssClass = "w3-input w3-border" />             
            </p>
            <p>
              <label>ส่งภายในวันที่</label>
              <input class="w3-input w3-border" type="date" runat="server" id="dateContratCreatePage" required="required"/>
            </p>
            <p>
              <label>ชื่อพนักงานส่งเอกสาร</label>
              <input class="w3-input w3-border" type="text" runat="server" id="txtMesNameCreatePage"/>
            </p>
            <p>
              <label>หมายเหตุ</label>
              <textarea class = "w3-input  w3-border" runat="server" id="txtRemarkCreatPage"></textarea>
            </p>
            <asp:Button Text="สร้างใบสั่งงาน" runat="server" CssClass="w3-button w3-blue" ID="btnCreate" OnClick="btnCreate_Click" OnClientClick="return validate();" />
        </div>
        <div class="w3-col" style="width:20%">
            &nbsp;
        </div>
    </div>

      <script type="text/javascript">

            $(function () {
              $('#<%=txtByCreateP.ClientID%>').autocomplete({
                  minLength: 1,
                  source: function (request, response) {
                      $.ajax({
                          url: "CreateMessengerPage.aspx/AutoComBy",
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

          
          var userName = '<%= Session["Username"].ToString() %>';
 
          var date = new Date();
          var Hours = date.getHours();

          if (Hours > 8 && Hours <= 23) {
              date.setDate(date.getDate() + 1);
          }


          var day = date.getDate();
          var month = date.getMonth() + 1;
          var year = date.getFullYear();
          if (month < 10) month = "0" + month;
          if (day < 10) day = "0" + day;
          var today = year + "-" + month + "-" + day;
          document.getElementById("<%=dateContratCreatePage.ClientID %>").setAttribute("min", today);
          document.getElementById("<%=dateContratCreatePage.ClientID %>").setAttribute("value", today);

          function selectSend() {
              if (document.getElementById("<%=opSendCreateP.ClientID %>").value == "อื่นๆ") {
                  document.getElementById("<%=txtSendCreateP.ClientID %>").disabled = false;
              } else {
                  document.getElementById("<%=txtSendCreateP.ClientID %>").disabled = true;
              }
          }

          function selectRecieve() {
              if (document.getElementById("<%=opReceiveCreateP.ClientID %>").value == "อื่นๆ") {
                  document.getElementById("<%=txtReceiveCreateP.ClientID %>").disabled = false;
              } else {
                  document.getElementById("<%=txtReceiveCreateP.ClientID %>").disabled = true;
              }
          }

          function validate() {
              var Send = document.getElementById("<%=opSendCreateP.ClientID %>").value;
              var Recieve = document.getElementById("<%=opReceiveCreateP.ClientID %>").value;
              if (Send == 0 && Recieve == 0) {
                  alert("กรุณาเลือกประเภทเอกสาร");
<%--                  document.getElementById("<%=opSendCreateP.ClientID %>").focus();--%>
                  return false;
              }

            return true;
        }
      </script>
</asp:Content>
