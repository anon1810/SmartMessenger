using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using SmartMessenger.Data;
using SmartMessenger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMessenger
{
    public partial class ReportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["Username"] == null) {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                ShowData();
            }
        }

        private string CreateBarChart(string chart) {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_date != null).OrderByDescending(a => a.msg_date).GroupBy(c => c.msg_date.Value.Date).ToList().Take(20);

            chart += "var barcoloR = [];";
            chart += "var barlabelData = [];";
            chart += "var barvalueOfData = [];";

            foreach (var r in result)
            {
                chart += "barcoloR.push(\"#5B90BF\");"; //สีเดียว
                //chart += "barcoloR.push(dynamicColors());";
            }

            foreach (var r in result)
            {
                chart += "barlabelData.push(\"" + r.Key.ToShortDateString() + "\");";
            }

            foreach (var r in result)
            {
                chart += "barvalueOfData.push(\"" + r.Count() + "\");";
            }


            chart += "var barLabel = \"Bar Label\";";
            chart += "var barTitle = \"ปริมาณงานในแต่ละวัน\";";
            chart += "var bartypeChart = \"bar\";";
            chart += "var barcanvasID = \"report1\";";

            chart += "new Chart(document.getElementById(barcanvasID), {";
            chart += "type: bartypeChart,";
            chart += "data: { labels: barlabelData, datasets: [ { label: barLabel, backgroundColor: barcoloR, data: barvalueOfData }]},";
            chart += "options: { legend: { display: false },title: { display: true,text: barTitle} }});";

            return chart;
        }

        private string CreateLineChart(string chart)
        {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_date != null).OrderByDescending(a => a.msg_date).GroupBy(c => c.msg_date.Value.Month+"/"+c.msg_date.Value.Year).ToList().Take(20);
            //var result = mesRes.GetMessagerList().Where(a => a.msg_section != null).GroupBy(c => c.msg_section).ToList();

            chart += "var linecoloR = [];";
            chart += "var linelabelData = [];";
            chart += "var linevalueOfData = [];";

            foreach (var r in result)
            {
                chart += "linecoloR.push(dynamicColors());";
            }

            foreach (var r in result)
            {
                chart += "linelabelData.push(\"" + r.Key+ "\");";
            }

            foreach (var r in result)
            {
                chart += "linevalueOfData.push(\"" + r.Count() + "\");";
            }


            chart += "var lineLabel = \"Line Label\";";
            chart += "var lineTitle = \"ปริมาณงานในแต่ละเดือน\";";
            chart += "var linetypeChart = \"line\";";
            chart += "var linecanvasID = \"report2\";";

            chart += "new Chart(document.getElementById(linecanvasID), {";
            chart += "type: linetypeChart,";
            chart += "data: { labels: linelabelData, datasets: [ { label: lineLabel, pointBackgroundColor: linecoloR, data: linevalueOfData }]},";
            chart += "options: { legend: { display: false },title: { display: true,text: lineTitle} }});";

            return chart;
        }

        private string CreatePieChart(string chart)
        {
            MessengerRepository mesRes = new MessengerRepository();

            //DateTime dt = DateTime.ParseExact("24/9/2019", "dd/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var result = mesRes.GetMessagerList().Where(a => a.msg_section != null && a.msg_section !="").Where(a => a.msg_date.Value.Month == DateTime.Now.Month).GroupBy(c => c.msg_section).ToList();

            chart += "var piecoloR = [];";
            chart += "var pielabelData = [];";
            chart += "var pievalueOfData = [];";

            foreach (var r in result)
            {
                chart += "piecoloR.push(dynamicColors());";
            }

            foreach (var r in result)
            {
                chart += "pielabelData.push(\"" + r.Key + "\");";
            }

            foreach (var r in result)
            {
                chart += "pievalueOfData.push(\"" + r.Count() + "\");";
            }


            chart += "var pieLabel = \"Pie Label\";";
            chart += "var pieTitle = \"ปริมาณงานแต่ละแผนกประจำเดือนนี้\";";
            chart += "var pietypeChart = \"doughnut\";";
            chart += "var piecanvasID = \"report3\";";

            chart += "new Chart(document.getElementById(piecanvasID), {";
            chart += "type: pietypeChart,";
            chart += "data: { labels: pielabelData, datasets: [ { label: pieLabel, backgroundColor: piecoloR, data: pievalueOfData }]},";
            chart += "options: { legend: { display: true },title: { display: true,text: pieTitle} }});";

            return chart;
        }

        public void ShowData() {

            string chart = "<script type=>$(function() { ";
            chart += "var dynamicColors = function() {";
            chart += "var r = Math.floor(Math.random() * 255);";
            chart += "var g = Math.floor(Math.random() * 255);";
            chart += "var b = Math.floor(Math.random() * 255);";
            chart += "return \"rgb(\" + r + \",\" + g + \",\" + b + \")\";};";

            chart = CreateBarChart(chart);
            chart = CreateLineChart(chart);
            chart = CreatePieChart(chart);

            chart += "});";
            chart += "</script>";

            ltChart.Text = chart;

            ShowSummaryReport();
        }

        public void ShowSummaryReport() {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList();
            DateTime dt = DateTime.Now;

            var dAll = result.Where(a => a.msg_on_date != null).Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();
            var dSuc = result.Where(a => a.msg_on_date != null && a.msg_close_status == "ดำเนินการ").Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();
            var dErr = result.Where(a => a.msg_on_date != null && a.msg_close_status == "ดำเนินการ").Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();

            dayReportAll.InnerText = "จำนวนงาน "+ dAll.Count + " งาน";
            dayReportSuc.InnerText = "รายการที่รอปล่อย "+ dSuc.Count + " งาน";
            dayReportErr.InnerText = "รายการที่ปล่อยงานแล้ว "+ dErr.Count + " งาน";

            var mAll = result.Where(a => a.msg_on_date != null).Where(a => a.msg_date.Value.Month + "/" + a.msg_date.Value.Year == dt.Month + "/" + dt.Year).ToList();
            var mSuc = result.Where(a => a.msg_on_date != null && (a.msg_close_status == "เสร็จสิ้น" || a.msg_close_status == "Yes")).Where(a => a.msg_date.Value.Month + "/" + a.msg_date.Value.Year == dt.Month + "/" + dt.Year).ToList();
            var mErr = result.Where(a => a.msg_on_date != null && (a.msg_close_status != "เสร็จสิ้น" && a.msg_close_status != "Yes")).Where(a => a.msg_date.Value.Month + "/" + a.msg_date.Value.Year == dt.Month + "/" + dt.Year).ToList();

            mounthReportAll.InnerText = "จำนวนงาน "+ mAll.Count + " งาน";
            mounthReportSuc.InnerText = "รายการที่เสร็จสิ้น "+ mSuc.Count + " งาน";
            mounthReportErr.InnerText = "รายการที่ยังไม่เสร็จสิ้น "+ mErr.Count + " งาน";

            var yAll = result.Where(a => a.msg_on_date != null).Where(a => a.msg_date.Value.Year == dt.Year).ToList();
            var ySuc = result.Where(a => a.msg_on_date != null && (a.msg_close_status == "เสร็จสิ้น" || a.msg_close_status == "Yes")).Where(a => a.msg_date.Value.Year == dt.Year).ToList();
            var yErr = result.Where(a => a.msg_on_date != null && (a.msg_close_status != "เสร็จสิ้น" && a.msg_close_status != "Yes")).Where(a => a.msg_date.Value.Year == dt.Year).ToList();

            yearReportAll.InnerText = "จำนวนงาน " + yAll.Count + " งาน";
            yearReportSuc.InnerText = "รายการที่เสร็จสิ้น " + ySuc.Count + " งาน";
            yearReportErr.InnerText = "รายการที่ยังไม่เสร็จสิ้น " + yErr.Count + " งาน";
        }

        public void GenPDF(List<msgctrlDev> result,string onDateReport)
        {
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfBold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew Bold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fnt = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
            Font fntNormal = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);
            Font fntBold = new Font(bfBold, 14, Font.NORMAL, BaseColor.BLACK);

            Document pdfDoc = new Document(PageSize.A4, 30, 30, 20, 20);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            //pdfWriter.PageEvent = new PDFFooter();
            pdfWriter.PageEvent = new PDFBackgroundHelper();

            PdfPTable table = new PdfPTable(1);
            table.HorizontalAlignment = 0;

            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resource/logo.jpg"));
            png.ScaleAbsolute(80, 30);

            PdfPCell headerTableCell_0 = new PdfPCell(png);
            headerTableCell_0.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTableCell_0.Border = Rectangle.NO_BORDER;
            table.AddCell(headerTableCell_0);


            //Cell no 2
            //Chunk chunk = new Chunk("ชื่อ: นายอานนท์ หงษ์กลิ่น ,\nAddress: Latham Village, Latham, New York, US, \nOccupation: Nurse, \nAge: 25 years", fnt);
            //cell = new PdfPCell();
            //cell.Border = 0;
            //cell.AddElement(chunk);
            //table.AddCell(cell);

            //Add table to document
            pdfDoc.Add(table);

            //Table
            table = new PdfPTable(14);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;

            Phrase p = new Phrase(onDateReport, fntBold);

            PdfPCell cell = new PdfPCell(p);
            cell.Colspan = 14;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 10;
            table.AddCell(cell);


            string[] arrHeadField = { "No.", "รหัสอ้างอิง", "โดย", "เอกสาร", "ประเภท", "สำคัญ", "ชื่อผู้ติดต่อ", "ที่อยู่ผู้ติดต่อ", "เบอร์ติดต่อ", "แผนที่", "ภายในวันที่", "Messenger", "วัน/เวลา", "หมายเหตุ" };

            for (int i = 0; i < arrHeadField.Length; i++)
            {
                cell = new PdfPCell(new Phrase(arrHeadField[i], fnt));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }

            int count = 1;
            int countSend = 0;
            int countReceive = 0;
            int countSendReceive = 0;
            foreach (var m in result) {
                string onDate = m.msg_on_date.ToString() == "" ? "" : m.msg_on_date.Value.ToShortDateString();
                string docType = m.msg_doctype;
                string isMap = m.msg_map;

                if (m.msg_map!="" && m.msg_map != "-") {
                    isMap = "มีแนบ";
                }

                if (docType != null)
                {
                    docType = docType.Replace("ส่ง|", " ส่ง:");
                    docType = docType.Replace("รับ|", " รับ:");
                    docType = docType.Replace("|", "");
                }

                string sendreceive = "";
                if (m.msg_send == "Yes" && m.msg_receive == "Yes")
                {
                    sendreceive = "ส่ง/รับ";
                    countSendReceive++;
                }
                else if (m.msg_send == "Yes")
                {
                    sendreceive = "ส่ง";
                    countSend++;
                }
                else if (m.msg_receive == "Yes")
                {
                    sendreceive = "รับ";
                    countReceive++;
                }

                string priority = m.msg_priority_normal == "Yes" ? "ปกติ" : "ด่วน";

                string[] arrData = { count.ToString(), m.msg_id.ToString(), m.msg_by, docType.Trim(), sendreceive, priority, m.msg_contact_name, m.msg_address.Trim(), m.msg_telephone, m.msg_map, onDate, "", "", m.msg_remark};

                for (int i = 0; i < arrData.Length; i++)
                {
                    cell = new PdfPCell(new Phrase(arrData[i], fnt));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                count++;
            }

            pdfDoc.Add(table);

            table = new PdfPTable(3);
            table.SpacingBefore = 10f;
            table.HorizontalAlignment = 2;
            table.TotalWidth = 500f;
            float[] widths = new float[] { 420f, 40f, 40f };
            table.SetWidths(widths);

            p = new Phrase("ส่ง", fntBold);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            p = new Phrase(countSend.ToString(), fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            p = new Phrase("รายการ", fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);

            p = new Phrase("รับ", fntBold);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            p = new Phrase(countReceive.ToString(), fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            p = new Phrase("รายการ", fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);


            p = new Phrase("ส่งและรับ", fntBold);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            p = new Phrase(countSendReceive.ToString(), fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            p = new Phrase("รายการ", fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);

            p = new Phrase("รวมทั้งหมด", fntBold);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);
            p = new Phrase(result.Count().ToString(), fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            p = new Phrase("รายการ", fntNormal);
            cell = new PdfPCell(p);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(cell);

            pdfDoc.Add(table);

            //Horizontal Line
            Paragraph line = new Paragraph(new Chunk(new LineSeparator(0.0F, 30.0F, BaseColor.BLACK, Element.ALIGN_RIGHT, 1)));
            pdfDoc.Add(line);

            //Paragraph para = new Paragraph();
            //para.Font = fnt;
            //para.Add("สวัสดี อานนท์,\n\nThank you for being our valuable customer. We hope our letter finds you in the best of health and wealth.\n\nYours Sincerely, \nBank of America");
            //pdfDoc.Add(para);

            ////Creating link
            //Chunk chunk = new Chunk("Sample Link Text");
            //chunk.Font = FontFactory.GetFont("tahoma", 18, Font.BOLD, BaseColor.RED);
            //chunk.SetAnchor("https://www.google.com/");
            //pdfDoc.Add(chunk);

            //Horizontal Line
            //line = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            //pdfDoc.Add(line);

            //table = new PdfPTable(10);
            //table.WidthPercentage = 100;
            //table.HorizontalAlignment = 0;
            //table.TotalWidth = 500f;
            //table.LockedWidth = true;
            //float[] widths = new float[] { 20f, 60f, 60f, 30f, 50f, 80f, 50f, 50f, 50f, 50f };
            //table.SetWidths(widths);

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Credit-Card-Report.pdf"); //ถ้าต้องการให้ dowload ไฟล์
            Response.AppendHeader("Content-Disposition", "inline; filename="+ onDateReport.Replace("/","-") + ".pdf");
            Response.Write(pdfDoc);
            Response.End();

            //แทนที่ Response.End(); เพราะมัน debug ต่อไม่ได้
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.SuppressContent = true;
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public void GenPDFIndividual(List<msgctrlDev> result,string onDateReport,DateTime dt) {
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfBold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew Bold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fntSmall = new Font(bf, 11, Font.NORMAL, BaseColor.BLACK);
            Font fntNormal = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);
            Font fntBold = new Font(bfBold, 14, Font.NORMAL, BaseColor.BLACK);
            Font fntBoldBig = new Font(bfBold, 18, Font.NORMAL, BaseColor.BLACK);

            Font fntHead = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);

            Document pdfDoc = new Document(PageSize.A4, 30, 30, 20, 20);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            pdfWriter.PageEvent = new PDFHalfLine();


            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resource/logo.jpg"));
            png.ScaleAbsolute(90, 40);

            //Table
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.TotalWidth = 500f;
            float[] widthsT = new float[] { 80f, 150f, 50f, 220f };
            table.SetWidths(widthsT);

            string dateNow = dt.ToShortDateString();
            PdfPCell cell = new PdfPCell();
            Phrase p = new Phrase();

            int count = 1;
            foreach (var m in result) {

                string onDate = m.msg_on_date.ToString() == "" ? "" : m.msg_on_date.Value.ToShortDateString();
                string docType = m.msg_doctype;

                if (docType != null) {
                    docType = docType.Replace("ส่ง|", " ส่ง:");
                    docType = docType.Replace("รับ|", " รับ:");
                    docType = docType.Replace("|", "");
                }

                string sendreceive = "";
                if (m.msg_send == "Yes" && m.msg_receive == "Yes") {
                    sendreceive = "ส่ง/รับ";
                } else if (m.msg_send == "Yes") {
                    sendreceive = "ส่ง";

                } else if (m.msg_receive == "Yes") {
                    sendreceive = "รับ";
                }

                string priority = m.msg_priority_normal == "Yes" ? "ปกติ" : "ด่วน";


                cell = new PdfPCell(png);
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 5;
                cell.PaddingTop = 5;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("บริษัท มิตซูบิชิ เอลเวเตอร์ (ประเทศไทย) จำกัด \nMITSUBISHI ELEVATOR (THAILAND) CO.LTD.\n2/3 หมู่ 14 อาคารบางนาทาวเวอร์ เอ ชั้น 9-10,12 ถนนเทพรัตน ตำบลบางแก้ว อำเภอบางพลี จังหวัดสมุทรปราการ 10540 Tel: 02-3120808,02-3120707, Fax: 02-3120800", fntSmall));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 5;
                cell.PaddingTop = 5;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ใบงาน", fntBoldBig));
                cell.Colspan = 4;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("วันที่ " + dateNow, fntNormal));
                cell.Colspan = 4;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.PaddingBottom = 5;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("รหัสอ้างอิง : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(m.msg_id.ToString(), fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ระดับความสำคัญ : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(priority, fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ประเภท : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(sendreceive, fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("รายละเอียด : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(docType.Trim().Replace(" รับ:", "\r\n" + "รับ:"), fntNormal));
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("หมายเหตุ : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(m.msg_remark == "" ? "-" : m.msg_remark, fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ชื่อผู้ติดต่อ : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(m.msg_contact_name, fntNormal));
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ที่อยู่ผู้ติดต่อ : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.FixedHeight = 35f;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(m.msg_address, fntNormal));
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.FixedHeight = 35f;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("เบอร์ติดต่อ : ", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(m.msg_telephone, fntNormal));
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("ผู้ฝากงาน :", fntBold));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase((m.msg_by + "    โทร. ") + (m.msg_phone == "" ? "-" : m.msg_phone), fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", fntNormal));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("พนักงานรับส่ง : ......................................................................", fntBold));
                cell.Colspan = 2;
                cell.PaddingTop = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("ผู้รับ : .................................................................................", fntBold));
                cell.Colspan = 2;
                cell.PaddingTop = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("วันที่ : ......................................................................................", fntBold));
                cell.Colspan = 2;
                cell.PaddingTop = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("วันที่ : .................................................................................", fntBold));
                cell.Colspan = 2;
                cell.PaddingTop = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                if (count % 2 == 0) {
                    cell = new PdfPCell(new Phrase(" ", fntBold));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    cell.PaddingBottom = 10;
                    table.AddCell(cell);
                } else {
                    cell = new PdfPCell(new Phrase(" ", fntBold));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    cell.PaddingBottom = 50;
                    table.AddCell(cell);
                }

                count++;
            }


            pdfDoc.Add(table);

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Credit-Card-Report.pdf"); //ถ้าต้องการให้ dowload ไฟล์
            Response.AppendHeader("Content-Disposition", "inline; filename=" + onDateReport.Replace("/", "-") + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void genReport_Click(object sender, EventArgs e) {
            MessengerRepository mesRes = new MessengerRepository();
            string onDateReport = "";
            List<msgctrlDev> result = null;

            if (opSelect.Value == "รายงานวันนี้") {
                DateTime dt = DateTime.Now;
                onDateReport = "รายการรับส่งเอกสารโดย Messenger ประจำวันที่ " + dt.ToShortDateString();
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && a.msg_close_status == "ดำเนินการ").Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();
            } else if (opSelect.Value == "รายงานเดือนนี้") {
                DateTime dt = DateTime.Now;
                onDateReport = "รายการรับส่งเอกสารโดย Messenger ประจำเดือนที่ " + dt.Month + "/" + dt.Year;
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && (a.msg_close_status != "ยกเลิก" && a.msg_close_status != "รอปล่อยงาน")).Where(a => a.msg_date.Value.Month + "/" + a.msg_date.Value.Year == dt.Month + "/" + dt.Year).ToList();
            } else if (opSelect.Value == "รายงานวันที่") {
                DateTime msg_on_date = DateTime.ParseExact(dtSelect.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                onDateReport = "รายการรับส่งเอกสารโดย Messenger ประจำวันที่ " + msg_on_date.ToShortDateString();
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && (a.msg_close_status != "ยกเลิก" && a.msg_close_status != "รอปล่อยงาน")).Where(a => a.msg_on_date.Value.Date == msg_on_date.Date).ToList();
            } else if (opSelect.Value == "รายงานเดือนที่") {
                DateTime msg_on_date = DateTime.ParseExact(dtSelect.Value, "yyyy-MM", System.Globalization.CultureInfo.InvariantCulture);
                onDateReport = "รายการรับส่งเอกสารโดย Messenger ประจำเดือนที่ " + msg_on_date.Month + "/" + msg_on_date.Year;
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && (a.msg_close_status != "ยกเลิก" && a.msg_close_status != "รอปล่อยงาน")).Where(a => a.msg_date.Value.Month + "/" + a.msg_date.Value.Year == msg_on_date.Month + "/" + msg_on_date.Year).ToList();
            }

            if (result.Count > 0) {
                GenPDF(result, onDateReport);
                GenPDF(result, onDateReport);
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertErr", "alert('ไม่มีรายการที่เลือก')", true);
            }
        }

        protected void genReportIndivi_Click(object sender, EventArgs e) {

            MessengerRepository mesRes = new MessengerRepository();
            string onDateReport = "";
            List<msgctrlDev> result = null;
            DateTime dt = new DateTime();

            if (opSelect.Value == "รายงานวันนี้") {
                dt = DateTime.Now;
                onDateReport = "ใบงาน ประจำวันที่ " + dt.ToShortDateString();
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && a.msg_close_status == "ดำเนินการ").Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();
            } else if (opSelect.Value == "รายงานวันที่") {
                dt = DateTime.ParseExact(dtSelect.Value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                onDateReport = "ใบงาน ประจำวันที่ " + dt.ToShortDateString();
                result = mesRes.GetMessagerList().Where(a => a.msg_on_date != null && (a.msg_close_status == "เสร็จสิ้น" || a.msg_close_status == "Yes")).Where(a => a.msg_on_date.Value.Date == dt.Date).ToList();
            }

            if (result.Count > 0) {
                GenPDFIndividual(result, onDateReport, dt);
            } else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertErr", "alert('ไม่มีรายการที่เลือก')", true);
            }

        }
    }
    public class PDFBackgroundHelper : PdfPageEventHelper
    {

        private PdfContentByte cb;
        private List<PdfTemplate> templates;
        //constructor
        public PDFBackgroundHelper()
        {
            this.templates = new List<PdfTemplate>();
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            cb = writer.DirectContentUnder;
            PdfTemplate templateM = cb.CreateTemplate(50, 50);
            templates.Add(templateM);

            int pageN = writer.CurrentPageNumber;
            String pageText = "หน้าที่ " + pageN.ToString() + "/";

            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            float len = bf.GetWidthPoint(pageText, 10);
            cb.BeginText();
            cb.SetFontAndSize(bf, 10);
            //cb.SetTextMatrix(document.LeftMargin, document.PageSize.GetBottom(document.BottomMargin));
            cb.SetTextMatrix(530, 10);
            cb.ShowText(pageText);
            cb.EndText();
            cb.AddTemplate(templateM, 530 + len, 10);
            //cb.AddTemplate(templateM, 555, 10);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            foreach (PdfTemplate item in templates)
            {
                item.BeginText();
                item.SetFontAndSize(bf, 10);
                item.SetTextMatrix(0, 0);
                item.ShowText("" + (writer.PageNumber));
                item.EndText();
            }

        }
    }

    public class PDFHalfLine : PdfPageEventHelper {

        // หัวข้อเฉพาะหน้าแรก
        public override void OnOpenDocument(PdfWriter writer, Document document) {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document) {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document) {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetLineWidth(1);
            contentByte.MoveTo(0, document.PageSize.Height / 2);
            contentByte.LineTo(document.PageSize.Width, document.PageSize.Height / 2);
            contentByte.Stroke();
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document) {
            base.OnCloseDocument(writer, document);
        }
    }
}