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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenPDF();
        }

        public void GenPDF()
        {
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfBold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew Bold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fnt = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
            Font fntNormal = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);
            Font fntBold = new Font(bfBold, 14, Font.NORMAL, BaseColor.BLACK);

            Font fntHead = new Font(bf, 14, Font.NORMAL, BaseColor.BLACK);

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

            string dateNow = DateTime.Now.ToShortDateString();
            Phrase p = new Phrase("รายการรับส่งเอกสารโดย Messenger ประจำวันที่ "+ dateNow, fntHead);

            PdfPCell cell = new PdfPCell(p);
            cell.Colspan = 14;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 10;
            table.AddCell(cell);


            string[] arrHeadField = { "No.", "โดย", "แผนก", "เอกสาร", "ประเภท", "สำคัญ", "ชื่อผู้ติดต่อ", "ที่อยู่ผู้ติดต่อ", "เบอร์ติดต่อ", "แผนที่", "ภายในวันที่", "เซ็นปิดงาน", "วัน/เวลา", "หมายเหตุ" };

            for (int i = 0; i < arrHeadField.Length; i++) {
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
                    countSendReceive++;
                } else if (m.msg_send == "Yes") {
                    sendreceive = "ส่ง";
                    countSend++;
                } else if (m.msg_receive == "Yes") {
                    sendreceive = "รับ";
                    countReceive++;
                }

                string priority = m.msg_priority_normal == "Yes" ? "ปกติ" : "ด่วน";

                string[] arrData = { count.ToString(),m.msg_by,m.msg_section, docType.Trim(), sendreceive, priority, m.msg_contact_name,m.msg_address,m.msg_telephone,m.msg_map, onDate,"","",""};

                for (int i = 0; i < arrData.Length; i++) {
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
            float[] widths = new float[] { 420f, 40f, 40f};
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

        public void ShowData() {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_section != null && a.msg_doctype!=null).ToList();
            var result2 = mesRes.GetMessagerList().Where(a => a.msg_section != null).GroupBy(c => c.msg_section).ToList();
            var result3 = mesRes.GetMessagerList().Where(a => a.msg_section != null).OrderByDescending(a=>a.msg_date).GroupBy(c => c.msg_date.Value.Date).ToList().Take(20);

            string chart = "<script type=>$(function() { ";
#region Bar
            chart += "var data1 = { labels: [";
            foreach (var r in result3) {
                chart += "\"" + r.Key.ToShortDateString() + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "], datasets: [{ label: \"2557\",fillColor: \"#5B90BF\",data: [";
            foreach (var r in result3) {
                chart += "\"" + r.Count() + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "]}]}; var ctx = $(\"#report1\").get(0).getContext(\"2d\");var chart = new Chart(ctx).Bar(data1, { bezierCurve: false });";
#endregion
#region Line
            chart += "var data2 = { labels: [";
            foreach (var r in result2) {
                chart += "\"" + r.Key + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "], datasets: [{ label: \"2557\",fillColor: \"#5B90BF\",data: [";
            foreach (var r in result2) {
                chart += "\"" + r.Count() + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "]}]}; var ctx = $(\"#report2\").get(0).getContext(\"2d\");var chart = new Chart(ctx).Line(data2, { bezierCurve: false });";
#endregion
#region Pie
            chart += "var data3 = { labels: [";
            foreach (var r in result2) {
                chart += "\"" + r.Key + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "], datasets: [{ label: \"2557\",fillColor: \"#5B90BF\",data: [";
            foreach (var r in result2) {
                chart += "\"" + r.Count() + "\"" + ",";
            }
            chart = chart.Substring(0, chart.Length - 1);
            chart += "]}]}; var ctx = $(\"#report3\").get(0).getContext(\"2d\");var chart = new Chart(ctx).Pie(data3, { bezierCurve: false });";
#endregion

            chart += "});</script>";
            //ltChart.Text = chart;
        }

        public void ShowData2()
        {
            MessengerRepository mesRes = new MessengerRepository();
            var result = mesRes.GetMessagerList().Where(a => a.msg_section != null && a.msg_doctype != null).ToList();
            var result2 = mesRes.GetMessagerList().Where(a => a.msg_section != null).GroupBy(c => c.msg_section).ToList();
            var result3 = mesRes.GetMessagerList().Where(a => a.msg_section != null).OrderByDescending(a => a.msg_date).GroupBy(c => c.msg_date.Value.Date).ToList().Take(20);

            string chart = "<script type=>$(function() { ";
            chart += "var coloR = [];";
            chart += "var dynamicColors = function() {";
            chart += "var r = Math.floor(Math.random() * 255);";
            chart += "var g = Math.floor(Math.random() * 255);";
            chart += "var b = Math.floor(Math.random() * 255);";
            chart += "return \"rgb(\" + r + \",\" + g + \",\" + b + \")\";};";

            foreach (var r in result3) {
                chart += "coloR.push(dynamicColors());";
                //chart += "coloR.push(\"#5B90BF\");"; สีเดียว
            }

            chart += "var labelData = [];";
            foreach (var r in result3) {
                chart += "labelData.push(\""+ r.Key.ToShortDateString() + "\");";
            }

            chart += "var valueOfData = [];";
            foreach (var r in result3) {
                chart += "valueOfData.push(\""+ r.Count() + "\");";
            }

            chart += "var barLabel = \"Bar Label\";";
            chart += "var barTitle = \"Bar Title\";";
            chart += "var typeChart = \"bar\";";
            chart += "var canvasID = \"report1\";";

            chart += "new Chart(document.getElementById(canvasID), {";
            chart += "type: typeChart,";
            chart += "data: { labels: labelData, datasets: [ { label: barLabel, backgroundColor: coloR, data: valueOfData }]},";
            chart += "options: { legend: { display: false },title: { display: true,text: barTitle} }});";

            chart += "});";
            chart += "</script>";

            //ltChart.Text = chart;
        }

    }
    public class PDFFooter : PdfPageEventHelper
    {

        // หัวข้อเฉพาะหน้าแรก
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            /*---- set font ----*/
            BaseFont bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Resource/Fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font fnt = new Font(bf, 11, Font.NORMAL, BaseColor.BLACK);

            /*---- set page number ----*/
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.TotalWidth = 300F;
            int pageN = writer.PageNumber;
            String text = "หน้าที่ : " + pageN;

            PdfPCell cell = new PdfPCell(new Phrase(text, fnt));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Rectangle pageSize = document.PageSize;
            tabFot.DefaultCell.Border = 0;
            tabFot.AddCell(cell);
            document.SetMargins(25f, 25f, 35f, 15f);
            tabFot.WriteSelectedRows(0, -1, pageSize.GetLeft(280), pageSize.GetBottom(25), writer.DirectContent);


            /*---- set footer----*/


        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }

}