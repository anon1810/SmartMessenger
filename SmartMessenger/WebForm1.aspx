<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SmartMessenger.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chart.js</title>
    <script src="https://code.jquery.com/jquery-1.11.3.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js" type="text/javascript"></script>
    <script src="scripts/chart-helper.js" type="text/javascript"></script>
    <link href="scripts/bootstrap-grid.css" rel="stylesheet" type="text/css" />
    <link href="scripts/chart.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $.ajax({
            type: "POST",
            url: "DemoService.asmx/MethodName",
            data: { param:data1 }
            }).done(function (json) {
                CreateBarChart(json, "reportBar", "chartLabelBar");
                CreateLineChart(json, "reportLine", "chartLabelLine");
                CreatePieChart(json, "reportPie", "chartLabelPie");
            }).fail(function (jqXHR, textStatus) {
                alert(textStatus);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-xs-8">
                <canvas id="reportBar" width="2" height="1"></canvas>
            </div>
            <div class="col-xs-4">
                <ul id="chartLabelBar" class="chart-label-list"></ul>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <canvas id="reportLine" width="2" height="1"></canvas>
            </div>
            <div class="col-xs-4">
                <ul id="chartLabelLine" class="chart-label-list"></ul>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-8">
                <canvas id="reportPie" width="2" height="1"></canvas>
            </div>
            <div class="col-xs-4">
                <ul id="chartLabelPie" class="chart-label-list"></ul>
            </div>
        </div>
    </form>
</body>
</html>
