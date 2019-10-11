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
            ShowData();
        }

        private string CreateBarChart(string chart) {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_date != null).OrderByDescending(a => a.msg_date).GroupBy(c => c.msg_date.Value.Date).ToList().Take(20);

            chart += "var barcoloR = [];";
            chart += "var barlabelData = [];";
            chart += "var barvalueOfData = [];";

            foreach (var r in result)
            {
                //chart += "barcoloR.push(\"#5B90BF\");"; //สีเดียว
                chart += "barcoloR.push(dynamicColors());";
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
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_section != null && a.msg_doctype != null).ToList();
            var result2 = mesRes.GetMessagerList().Where(a => a.msg_section != null).GroupBy(c => c.msg_section).ToList();
            var result3 = mesRes.GetMessagerList().Where(a => a.msg_section != null).OrderByDescending(a => a.msg_date).GroupBy(c => c.msg_date.Value.Date).ToList().Take(20);

            //DateTime dt = DateTime.Parse("24/9/2562");
            //var result4 = mesRes.GetMessagerList().Where(a => a.msg_date != null).Where(a => a.msg_date.Value.Date == dt).GroupBy(c => c.msg_date.Value.Date).ToList();

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
        }

        public void GenPDF(DateTime dateTime)
        {
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfBold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew Bold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fnt = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
            Font fntNormal = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);
            Font fntBold = new Font(bfBold, 14, Font.NORMAL, BaseColor.BLACK);

            Document pdfDoc = new Document(PageSize.A4, 30, 30, 20, 20);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            pdfWriter.PageEvent = new PDFFooter();

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

            string dateNow = dateTime.ToShortDateString();
            Phrase p = new Phrase("รายการรับส่งเอกสารโดย Messenger ประจำวันที่ " + dateNow, fntBold);

            PdfPCell cell = new PdfPCell(p);
            cell.Colspan = 14;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 10;
            table.AddCell(cell);


            string[] arrHeadField = { "No.", "โดย", "แผนก", "เอกสาร", "ประเภท", "สำคัญ", "ชื่อผู้ติดต่อ", "ที่อยู่ผู้ติดต่อ", "เบอร์ติดต่อ", "แผนที่", "ภายในวันที่", "เซ็นปิดงาน", "วัน/เวลา", "หมายเหตุ" };

            for (int i = 0; i < arrHeadField.Length; i++)
            {
                cell = new PdfPCell(new Phrase(arrHeadField[i], fnt));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }

            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().OrderByDescending(a => a.msg_id).Take(30);
            int count = 1;
            int countSend = 0;
            int countReceive = 0;
            int countSendReceive = 0;
            foreach (var m in result)
            {

                string onDate = m.msg_on_date.ToString() == "" ? "" : m.msg_on_date.Value.ToShortDateString();
                string docType = m.msg_doctype;

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

                string[] arrData = { count.ToString(), m.msg_by, m.msg_section, docType.Trim(), sendreceive, priority, m.msg_contact_name, m.msg_address, m.msg_telephone, m.msg_map, onDate, "", "", "" };

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
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }


        protected void genReport_Click(object sender, EventArgs e)
        {
            if (opSelect.Value == "รายงานวันนี้") {
                GenPDF(DateTime.Now);
            }
        }
    }
}