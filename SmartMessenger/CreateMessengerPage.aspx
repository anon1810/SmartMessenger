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
                <input class="w3-input w3-border" disabled type="text" value="ดึงจาก Database" runat="server" id="txtByCreateP">
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <label>แผนก</label>
                <input class="w3-input w3-border" disabled type="text" value="ดึงจาก Database" runat="server" id="txtSectionCreateP">
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <label>เบอร์ติดต่อ</label>
                <input class="w3-input w3-border" type="text" runat="server" id="txtByPhoneCreateP">
              </div>
            </div>
            <p></p>
            <label>ประเภทเอกสารที่จะส่ง</label>
            <div class="w3-cell-row">
              <div class="w3-cell">
                <select class="w3-select w3-border" name="option" runat="server" id="opSendCreateP" onchange="selectFunction()">
                  <option value="0" selected>&nbsp;-</option>
                  <option value="สัญญา">สัญญา</option>
                  <option value="ใบเสนอราคา">ใบเสนอราคา</option>
                  <option value="เช็ค">เช็ค</option>
                  <option value="ตั๋วอาวัล">ตั๋วอาวัล</option>
                  <option value="อื่นๆ โปรดระบุ">อื่นๆ โปรดระบุ</option>
                </select>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtSendCreateP" disabled>
              </div>
            </div>
            <p></p>
            <label>ประเภทเอกสารที่จะรับ</label>
            <div class="w3-cell-row">
              <div class="w3-cell">
                <select class="w3-select w3-border" name="option" runat="server" id="opReceiveCreateP">
                  <option value="0" selected>&nbsp;-</option>
                  <option value="สัญญา">สัญญา</option>
                  <option value="ใบเสนอราคา">ใบเสนอราคา</option>
                  <option value="เช็ค">เช็ค</option>
                  <option value="ตั๋วอาวัล">ตั๋วอาวัล</option>
                  <option value="อื่นๆ โปรดระบุ">อื่นๆ โปรดระบุ</option>
                </select>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtReceiveCreateP" disabled>
              </div>
            </div>

            <p></p>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="nmCreateP" value="1" checked>
              <label>ส่งแบบปกติ</label>
            </div>
            <div class="w3-cell">&nbsp;&nbsp;</div>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="urCreateP" value="2">
              <label>ส่งแบบด่วน</label>
            </div>
            <p></p>
           
            <div class="w3-cell-row">
              <div class="w3-cell">
                  <label>ชื่อผู้ติดต่อ</label>
                  <input class="w3-input w3-border"  type="text" runat="server" id="txtContratNameCreateP" required>
              </div>
              <div class="w3-cell">
                 &nbsp;
              </div>
              <div class="w3-cell">
                <label>เบอร์ติดต่อ</label>
                <input class="w3-input w3-border" type="text" runat="server" id="txtContratPhoneCreateP" required>
              </div>
            </div>
            <p>
              <label>ที่อยู่ผู้ติดต่อ</label>
              <textarea class = "w3-input  w3-border"  runat="server" id="txtContratAddrCreateP" required></textarea>
            </p>
            <p>
              <label>แนบไฟล์แผนที่</label>
              <input class="w3-input w3-border" type="file"  runat="server" id="fileContratMapCreateP">
            </p>
            <p>
              <label>ส่งภายในวันที่</label>
              <input class="w3-input w3-border" type="date" runat="server" id="dateContratCreatePage" required>
            </p>
            <p>
              <label>ชื่อพนักงานส่งเอกสาร</label>
              <input class="w3-input w3-border" type="text" runat="server" id="txtMesNameCreatePage">
            </p>
            <p>
              <label>หมายเหตุ</label>
              <textarea class = "w3-input  w3-border" runat="server" id="txtRemarkCreatPage"></textarea>
            </p>
            <asp:Button Text="สร้างใบสั่งงาน" runat="server" class="w3-button w3-blue" ID="btnCreate" OnClick="btnCreate_Click" OnClientClick="return validate();" />
        </div>
        <div class="w3-col" style="width:20%">
            &nbsp;
        </div>
    </div>


      <script type="text/javascript" language="javascript">
          function selectSend() {
              if (document.getElementById("<%=opSendCreateP.ClientID %>").value == 5) {
                  document.getElementById("<%=txtSendCreateP.ClientID %>").disabled = false;
              } else {
                  document.getElementById("<%=txtSendCreateP.ClientID %>").disabled = true;
              }
          }

          function selectRecieve() {
              if (document.getElementById("<%=opReceiveCreateP.ClientID %>").value == 5) {
                  document.getElementById("<%=txtReceiveCreateP.ClientID %>").disabled = false;
              } else {
                  document.getElementById("<%=txtReceiveCreateP.ClientID %>").disabled = true;
              }
          }

          function validate() {
              var Send = document.getElementById("<%=opSendCreateP.ClientID %>").value;
              var Recieve = document.getElementById("<%=opSendCreateP.ClientID %>").value;
              if (Send == 0 && Recieve == 0) {
                  alert("กรุณาเลือกประเภทเอกสาร");
                  document.getElementById("<%=opSendCreateP.ClientID %>").focus();
                  return false;
              }

            return true;
        }
      </script>
</asp:Content>
