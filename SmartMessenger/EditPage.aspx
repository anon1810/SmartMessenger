﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditPage.aspx.cs" Inherits="SmartMessenger.EditPage" %>
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
                <input class="w3-input w3-border" disabled type="text" value="ดึงจาก Database" runat="server" id="txtByCreateP"/>
              </div>
              <div class="w3-cell">
                &nbsp;
              </div>
              <div class="w3-cell">
                <label>แผนก</label>
                <input class="w3-input w3-border" disabled type="text" value="ดึงจาก Database" runat="server" id="txtSectionCreateP"/>
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
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtSendCreateP" disabled="disabled" required="required"/>
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
                <input class="w3-input w3-border" type="text" placeholder="กรณีอื่นๆ โปรดระบุ.." runat="server" id="txtReceiveCreateP" disabled="disabled" required="required"/>
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
            <p></p>
              <label>ที่อยู่ผู้ติดต่อ</label>
              <textarea class = "w3-input  w3-border"  runat="server" id="txtContratAddrCreateP" required="required"></textarea>
            <p></p>
            <p></p>
              <label>แผนที่</label>
              <div class="w3-cell-row">
                <div class="w3-cell">
                  <select class="w3-select w3-border" name="option" runat="server" id="opMapSelect" onchange="selectMap()">
                    <option value="" selected="selected">ไม่มี</option>
                    <option value="แนบไปพร้อมกับเอกสารแล้ว"></option>
                    <option value="อัปโหลดไฟล์แผนที่">อัปโหลดไฟล์แผนที่</option>
                  </select>
                </div>
                <div class="w3-cell">
                  &nbsp;
                </div>
                <div class="w3-cell">
                  <asp:FileUpload id="FileUploadMap" runat="server" CssClass = "w3-input w3-border" disabled="disabled"/>
                </div>
              </div>
              <p id="txtOldfile" runat="server" class="w3-small">ไฟล์ปัจจุบัน :</p>
            <p></p>
            <p>
              <label>ส่งภายในวันที่</label>
              <input class="w3-input w3-border" type="date" runat="server" id="dateContratCreatePage" required/>
            </p>
            <p>
              <label>ชื่อพนักงานส่งเอกสาร</label>
              <select class="w3-select w3-border" name="option" runat="server" id="sleMes">
                <option value="-" selected="selected">-</option>
                <option value="อุทัย">อุทัย</option>
                <option value="อิทธิพล">อิทธิพล</option>
                <option value="พนักงานทดแทน">พนักงานทดแทน</option>
              </select>
<%--          <input class="w3-input w3-border" type="text" runat="server" id="txtMesNameCreatePage"/> --%>
            </p>
            <p>
              <label>หมายเหตุ</label>
              <textarea class = "w3-input  w3-border" runat="server" id="txtRemarkCreatPage"></textarea>
            </p>
            <div class="w3-bar">
                <asp:Button Text="อัพเดท" runat="server" class="w3-button w3-teal w3-round-xlarge" ID="btnUpdate" OnClick="btnUpdate_Click" OnClientClick="return validate();" />
                <asp:Button Text="ยกเลิกงานนี้" runat="server" class="w3-button w3-red w3-round-xlarge" ID="btnCancel" OnClick="btnCancel_Click"/>
            </div>
        </div>
        <div class="w3-col" style="width:20%">
            &nbsp;
        </div>
    </div>

    <div id="id01" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom" style="max-width:600px">
            <div class="w3-container w3-padding w3-red">
              <span  onclick="CloswaringModelCancel()" class="w3-button w3-red w3-right"><i class="fa fa-remove"></i></span>
              <h4>กรุณาระบุสาเหตุที่ยกเลิก</h4>
            </div>
            <div class="w3-container w3-padding">
                <textarea class = "w3-input  w3-border" runat="server" id="Textarea1"></textarea>
                <p></p>
                <div class="w3-bar">
                    <asp:Button Text="ยืนยัน" runat="server" class="w3-button w3-blue w3-round-xlarge" ID="btnCancelRemark" OnClick="btnCancelRemark_Click"/>
<%--                <button onclick="Closemodal()" type="button" class="w3-button w3-red w3-round-xlarge">กลับหน้าหลัก</button>--%>
                </div>
            </div>
            
        </div>
    </div>


      <script type="text/javascript" language="javascript">
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
<%--          document.getElementById("<%=dateContratCreatePage.ClientID %>").setAttribute("min", today);
          document.getElementById("<%=dateContratCreatePage.ClientID %>").setAttribute("value", today);--%>

          if (document.getElementById("<%=opMapSelect.ClientID %>").value == "อัปโหลดไฟล์แผนที่") {
              document.getElementById("<%=FileUploadMap.ClientID %>").disabled = false;
          }

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

          function selectMap() {
              if (document.getElementById("<%=opMapSelect.ClientID %>").value == "อัปโหลดไฟล์แผนที่") {
                  document.getElementById("<%=FileUploadMap.ClientID %>").disabled = false;
              } else {
                  document.getElementById("<%=FileUploadMap.ClientID %>").disabled = true;
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

          function CloswaringModelCancel() {
              document.getElementById("<%=id01.ClientID %>").style.display = 'none'
          }

      </script>
</asp:Content>
