
var colors = ["#5B90BF", "#BF616A", "#A3BE8C", "#EBCB8B", "#D08770",
                "#B48EAD", "#96B5B4", "#8FA1B3", "#B48EAD", "#AB7967",
                "#B9E3AE", "#FDB4BF", "#A3E7D8", "#F4EFAF", "#BACAB3", "#FF9797",
                "#C0C0C0", "#B3B3D9", "#D0B3E1", "#FFBE7D", "#EFA694", "#F9A484"];

var lightColors = ["#7CA6CC", "#CC8188", "#B7CCA5", "#EFD6A3", "#DAA08E",
                "#C4A6BE", "#AAC3C2", "#A7B5C3", "#C4A6BE", "#BD9688",
                "#C8E9BF", "#FDBFC8", "#B5ECE0", "#F6F2BE", "#C9D6C4", "#FFAEAE",
                "#CBCBCB", "#C2C2E1", "#DAC3E7", "#FFCC99", "#F2B6A8", "#FAB89F"];

function GetKeyArray(jsonString) {
    var key_array = [];
    var jsonString_split = jsonString.split('}')[0].replace("[{", "").split(',');

    for (var i = 0; i < jsonString_split.length; i++) {
        key_array.push(eval(jsonString_split[i].split(':')[0]));
    }
    return key_array;
}

function CreateBarChart(jsonString, canvasID, ulID) {
    //clear old chart
    if (typeof barChart != 'undefined') {
        barChart.destroy();
    }
    //clear all li
    $("#" + ulID).empty();

    if (jsonString == "[]") {
        return;
    }
    //new chart data structure
    var dataChart = {
        labels: [],
        datasets: []
    };

    //ceate dataset with first record key array not use first key
    var key_array = GetKeyArray(jsonString);
    for (var i = 1; i < key_array.length; i++) {
        var dataSet = {
            label: key_array[i],
            fillColor: colors[i - 1],
            highlightFill: lightColors[i - 1],
            data: []
        };
        dataChart.datasets.push(dataSet);
        $("#" + ulID).append('<li><span style="background-color: ' + colors[i - 1] + ' !important;"></span>&nbsp;' + key_array[i] + '</li>');
    }

    //loop records for create labels and push data value
    var dataJSON = JSON.parse(jsonString);
    $.each(dataJSON, function (rowIndex, rowData) {
        dataChart.labels.push(rowData[key_array[0]]);
        for (var i = 1; i < key_array.length; i++) {
            dataChart.datasets[i - 1].data.push(rowData[key_array[i]]);
        }
    });

    Chart.defaults.global.responsive = true;

    var ctx = $("#" + canvasID).get(0).getContext("2d");
    var chart = new Chart(ctx).Bar(dataChart, {
        bezierCurve: false
    });
    window.barChart = chart;
}

function CreateLineChart(jsonString, canvasID, ulID) {
    //clear old chart
    if (typeof lineChart != 'undefined') {
        lineChart.destroy();
    }

    //clear all li
    $("#" + ulID).empty();

    if (jsonString == "[]") {
        return;
    }

    //new chart data structure
    var dataChart = {
        labels: [],
        datasets: []
    };

    //ceate dataset with first record key array not use first key
    var key_array = GetKeyArray(jsonString);
    for (var i = 1; i < key_array.length; i++) {
        var dataSet = {
            label: key_array[i],
            strokeColor: colors[i - 1],
            pointColor: colors[i - 1],
            pointHighlightFill: lightColors[i - 1],
            data: []
        };
        dataChart.datasets.push(dataSet);
        $("#" + ulID).append('<li><span style="background-color: ' + colors[i - 1] + ' !important;"></span>&nbsp;' + key_array[i] + '</li>');
    }

    //loop records for create labels and push data value
    var dataJSON = JSON.parse(jsonString);
    $.each(dataJSON, function (rowIndex, rowData) {
        dataChart.labels.push(rowData[key_array[0]]);
        for (var i = 1; i < key_array.length; i++) {
            dataChart.datasets[i - 1].data.push(rowData[key_array[i]]);
        }
    });

    Chart.defaults.global.responsive = true;

    var ctx = $("#" + canvasID).get(0).getContext("2d");
    var chart = new Chart(ctx).Line(dataChart, {
        bezierCurve: false,
        datasetFill: false,
        pointDot: true
    });
    window.lineChart = chart;
}

function CreatePieChart(jsonString, canvasID, ulID) {
    //clear old chart
    /*if (typeof pieChart != 'undefined') {
    pieChart.destroy();
    }*/

    //clear all li
    $("#" + ulID).empty();

    if (jsonString == "[]") {
        return;
    }

    //new chart data structure
    var dataChart = [];

    //ceate dataset with first record key array not use first key
    var key_array = GetKeyArray(jsonString);
    for (var i = 1; i < key_array.length; i++) {
        var dataSet = {
            value: "",
            color: colors[i - 1],
            highlight: lightColors[i - 1],
            label: key_array[i]
        };
        dataChart.push(dataSet);
    }

    //loop records for create labels and push data value
    var dataJSON = JSON.parse(jsonString);
    $.each(dataJSON, function (rowIndex, rowData) {
        //dataChart.labels.push(rowData[key_array[0]]);
        for (var i = 1; i < key_array.length; i++) {
            dataChart[i - 1].value = rowData[key_array[i]];
            if (rowData[key_array[i]] != "0") {
                $("#" + ulID).append('<li><span style="background-color: ' + colors[i - 1] + ' !important;"></span>&nbsp;' + key_array[i] + '</li>');
            }
        }
    });

    Chart.defaults.global.responsive = true;

    var ctx = $("#" + canvasID).get(0).getContext("2d");
    var chart = new Chart(ctx).Pie(dataChart, {});
    //window.pieChart = chart;
}