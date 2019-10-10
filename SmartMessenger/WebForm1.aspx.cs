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
            //ShowData2();
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
            ltChart.Text = chart;
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

            ltChart.Text = chart;
        }

    }
}