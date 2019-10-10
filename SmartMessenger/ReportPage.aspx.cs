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
    }
}