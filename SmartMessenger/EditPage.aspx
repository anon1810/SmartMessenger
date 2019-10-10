<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditPage.aspx.cs" Inherits="SmartMessenger.EditPage" %>
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
                <select class="w3-select w3-border" name="option" runat="server" id="opSendCreateP" onchange="selectSend()">
                  <option value="0" selected>&nbsp;-</option>
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
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtSendCreateP" disabled>
              </div>
            </div>
            <p></p>
            <label>ประเภทเอกสารที่จะรับ</label>
            <div class="w3-cell-row">
              <div class="w3-cell">
                <select class="w3-select w3-border" name="option" runat="server" id="opReceiveCreateP" onchange="selectRecieve()">
                  <option value="0" selected>&nbsp;-</option>
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
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtReceiveCreateP" disabled>
              </div>
            </div>

            <p></p>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="nmCreateP" value="1" checked>
              <label>ปกติ</label>
            </div>
            <div class="w3-cell">&nbsp;&nbsp;</div>
            <div class="w3-cell">
              <input class="w3-radio" type="radio" name="sendType" runat="server" id="urCreateP" value="2">
              <label>ด่วน</label>
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
              <label>แนบไฟล์แผนที่</label><br>
              <asp:FileUpload id="FileUploadMap" runat="server" CssClass = "w3-input w3-border" />
              <p id="txtOldfile" runat="server">ไฟล์ปัจจุบัน :</p>
            </p>
            <p>
              <label>ส่งภายในวันที่</label>
              <input class="w3-input w3-border" type="date" runat="server" id="dateContratCreatePage" required/>
            </p>
            <p>
              <label>ชื่อพนักงานส่งเอกสาร</label>
              <input class="w3-input w3-border" type="text" runat="server" id="txtMesNameCreatePage"/>
            </p>
            <p>
              <label>หมายเหตุ</label>
              <textarea class = "w3-input  w3-border" runat="server" id="txtRemarkCreatPage"></textarea>
            </p>
            <div class="w3-bar">
                <asp:Button Text="อัพเดท" runat="server" class="w3-button w3-teal" ID="btnUpdate" OnClick="btnUpdate_Click" OnClientClick="return validate();" />
                <asp:Button Text="ยกเลิกงานนี้" runat="server" class="w3-button w3-red" ID="btnCancel" OnClick="btnCancel_Click"/>
            </div>
        </div>
        <div class="w3-col" style="width:20%">
            &nbsp;
        </div>
    </div>

    <div id="id01" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom" style="max-width:600px">
            <div class="w3-container w3-padding w3-deep-orange">
              <h4>กรุณาระบุสาเหตุที่ยกเลิก</h4>
            </div>
            <div class="w3-container w3-padding">
                <textarea class = "w3-input  w3-border" runat="server" id="Textarea1"></textarea>
                <p></p>
                <div class="w3-bar">
                    <asp:Button Text="ยืนยัน" runat="server" class="w3-button w3-blue" ID="btnCancelRemark" OnClick="btnCancelRemark_Click"/>
                    <button onclick="Closemodal()" type="button" class="w3-button w3-red">กลับหน้าหลัก</button>
                </div>
            </div>
            
        </div>
    </div>


      <script type="text/javascript" language="javascript">
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
                  return false;
              }
            return true;
          }

          function Closemodal() {
              document.getElementById("<%=id01.ClientID %>").style.display = 'none'
          }

      </script>
</asp:Content>
