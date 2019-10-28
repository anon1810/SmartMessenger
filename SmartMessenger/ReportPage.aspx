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
                      <input class="w3-input w3-border" type="date"  placeholder="โปรดระบุ.." runat="server" id="dtSelect" disabled="disabled"/>
                    </div>
                  </div>
                  <p></p>
                  <div class="w3-bar">
                    <asp:Button CssClass="w3-button w3-dark-grey" runat="server" id="genReport" formtarget="_blank" OnClick="genReport_Click" Text="สร้างใบรับส่งรวม"/>
                    <asp:Button CssClass="w3-button w3-dark-grey" runat="server" id="genReportIndivi" formtarget="_blank" OnClick="genReportIndivi_Click" Text="สร้างใบงาน" disabled/>
                  </div>
              </div>
              <p class="w3-border-bottom w3-padding-16"></p>
                <div class="w3-cell-row">
                    <div class="w3-cell">
                        <label>ข้อมูลสรุปประจำวัน</label>
                        <p id="dayReportAll" runat="server">จำนวนงาน x งาน</p>
                        <p id="dayReportSuc" runat="server">รายการที่สำเร็จ x งาน</p>
                        <p id="dayReportErr" runat="server">รายการที่ไม่สำเร็จ x งาน</p>
                    </div>
                    <div class="w3-cell">
                        <label>ข้อมูลสรุปประจำเดือน</label>
                        <p id="mounthReportAll" runat="server">จำนวนงาน x งาน</p>
                        <p id="mounthReportSuc" runat="server">รายการที่สำเร็จ x งาน</p>
                        <p id="mounthReportErr" runat="server">รายการที่ไม่สำเร็จ x งาน</p>
                    </div>
                </div>
                <br>               
                <label>ข้อมูลสรุปประจำปี</label>
                <p id="yearReportAll" runat="server">จำนวนงาน x งาน</p>
                <p id="yearReportSuc" runat="server">รายการที่สำเร็จ x งาน</p>
                <p id="yearReportErr" runat="server">รายการที่ไม่สำเร็จ x งาน</p>
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

    <div id="waringModel" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-card-4 w3-animate-zoom">
        <div class="w3-container w3-padding w3-red">
           <span  onclick="CloswaringModel()" class="w3-button w3-red w3-right"><i class="fa fa-remove"></i></span>
          <h4>คำเตือน</h4>
        </div>
          <div class="w3-container w3-padding-16">        
              <label>ไม่มีรายการที่เลือก</label>
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

        function CloswaringModel() {
           document.getElementById("<%=waringModel.ClientID %>").style.display = 'none';
        }

    </script>

</asp:Content>
