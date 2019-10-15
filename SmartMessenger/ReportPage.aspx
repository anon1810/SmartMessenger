<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReportPage.aspx.cs" Inherits="SmartMessenger.ReportPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Literal ID="ltChart" runat="server"></asp:Literal>

    <div class="w3-container">
        <div class="w3-row w3-container">
          <div class="w3-col w3-container" style="width:50%">
              <div>
                  <p></p>
                  <label>ประเภทรายงานที่ต้องการ</label>
                  <div class="w3-cell-row">
                    <div class="w3-cell">
                      <select class="w3-select w3-border" name="option" runat="server" id="opSelect" onchange="selectSend()">
                        <option value="รายงานวันนี้">รายงานวันนี้</option>
                        <option value="รายงานเดือนนี้">รายงานเดือนนี้</option>
                        <option value="รายงานวันที่">รายงานวันที่ ระบุ..</option>
                        <option value="รายงานเดือนที่">รายงานเดือนที่ ระบุ..</option>
                      </select>
                    </div>
                    <div class="w3-cell">
                      &nbsp;
                    </div>
                    <div class="w3-cell">
                      <input class="w3-input w3-border" type="date"  placeholder="โปรดระบุ.." runat="server" id="dtSelect" disabled/>
                    </div>
                  </div>
                  <p></p>
                  <asp:Button CssClass="w3-button w3-red" runat="server" id="genReport" formtarget="_blank" OnClick="genReport_Click" Text="สร้างรายงาน"/>
              </div>
              <p class="w3-border-bottom w3-padding-16"></p>
                <div class="w3-cell-row">
                    <div class="w3-cell">
                        <label>ข้อมูลสรุปประจำวัน</label>
                        <p>จำนวนงาน x งาน</p>
                        <p>รายการที่สำเร็จ x งาน</p>
                        <p>รายการที่ไม่สำเร็จ x งาน</p>
                    </div>
                    <div class="w3-cell">
                        <label>ข้อมูลสรุปประจำเดือน</label>
                        <p>จำนวนงาน x งาน</p>
                        <p>รายการที่สำเร็จ x งาน</p>
                        <p>รายการที่ไม่สำเร็จ x งาน</p>
                    </div>
                </div>
                <br>
                
                <label>ข้อมูลสรุปประจำปี</label>
                <p>จำนวนงาน x งาน</p>
                <p>รายการที่สำเร็จ x งาน</p>
                <p>รายการที่ไม่สำเร็จ x งาน</p>
          </div>
          <div class="w3-col w3-container" style="width:50%">
            <canvas id="report3"></canvas>
          </div>
        </div>
        
        <div class="w3-row w3-container">
          <div class="w3-col w3-container" style="width:50%">
            <canvas id="report1"></canvas>
          </div>
          <div class="w3-col w3-container" style="width:50%">
            <canvas id="report2"></canvas>
          </div>
        </div>       
    </div>

    <script type="text/javascript">
          function selectSend() {
              if (document.getElementById("<%=opSelect.ClientID %>").value == "รายงานวันที่") {
                  document.getElementById("<%=dtSelect.ClientID %>").disabled = false;
                  document.getElementById("<%=dtSelect.ClientID %>").setAttribute('type', 'date');
              } else if (document.getElementById("<%=opSelect.ClientID %>").value == "รายงานเดือนที่") {
                  document.getElementById("<%=dtSelect.ClientID %>").disabled = false;
                  document.getElementById("<%=dtSelect.ClientID %>").setAttribute('type', 'month');
              } else {
                  document.getElementById("<%=dtSelect.ClientID %>").disabled = true;
              }
          }
    </script>

</asp:Content>
