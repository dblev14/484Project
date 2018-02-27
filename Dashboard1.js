// this is the client-side script for the dashboard

// load visualization classes
google.load('visualization', '1', { packages: ['corechart', 'table'] });

// this gets it all started
google.setOnLoadCallback(drawWebPage);

// visualization charts
var chart1;
var chart2;
var chart3;
var chart4;
var chart5;
var chart6;
var chart7;
var chart9;
var chart9;
var chart10;
var chart11;
var chart12;

// data tables (contain results of queries)
var dataTable1;
var dataTable2;
var dataTable3;
var dataTable4;
var dataTable5;
var dataTable6;
var dataTable7;
var dataTable8;
var dataTable9;
var dataTable10;
var dataTable11;
var dataTable12;

function drawWebPage() {

    // call server-side WebMethod via AJAX
    PageMethods.DoQuery1(onQuery1Response);
    PageMethods.DoQuery2(onQuery2Response);
    PageMethods.DoQuery3(onQuery3Response);
    PageMethods.DoQuery4(onQuery4Response);
    PageMethods.DoQuery3(onQuery7Response);
    PageMethods.DoQuery3(onQuery10Response);
}

function onQuery1Response(response) {
    // create data table from query results
    dataTable1 = eval(response);
    dataTable1 = google.visualization.arrayToDataTable(dataTable1);

    // make a chart
    chart1 = new google.visualization.ColumnChart(document.getElementById('container1'));
    
    var options = {
        title: "Money Rewarded to each employee",
        width: 500,
        height: 400,
        bar: { groupWidth: "50%" },
        legend: { position: "none" },

    }

    chart1.draw(dataTable1, options);
    // specify the event handler method for user selection
    //google.visualization.events.addListener(chart1, 'select', selectHandler);





}

function onQuery2Response(response) {
    // create data table from query results
    dataTable2 = eval(response);
    dataTable2 = google.visualization.arrayToDataTable(dataTable2);

    // make a chart
    chart2 = new google.visualization.PieChart(document.getElementById('container2'));
    var options = {
        title: 'Proportion of Rewards for each Category',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }




    chart2.draw(dataTable2, options);

    // specify the event handler method for user selection
    //google.visualization.events.addListener(chart2, 'select', selectHandler2);





}

function onQuery3Response(response){
    dataTable3 = eval(response);
    dataTable3 = google.visualization.arrayToDataTable(dataTable3);

    chart3 = new google.visualization.Table(document.getElementById('container3'));

    var options = {
        title: 'Employees',
        chartArea: '100%',
        height: '100%',
        width: '100%'

    }

    chart3.draw(dataTable3, options);
    google.visualization.events.addListener(chart3, 'select', selectHandler);
}

function onQuery4Response(response) {
    // create data table from query results
    dataTable4 = eval(response);
    dataTable4 = google.visualization.arrayToDataTable(dataTable4);

    // make a chart
    chart4 = new google.visualization.PieChart(document.getElementById('container4'));
    var options = {
        title: 'Proportion of Values',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }




    chart4.draw(dataTable4, options);

    // specify the event handler method for user selection
   





}

function onQuery5Response(response) {
        // create data table from query results
        dataTable5 = eval(response);
        dataTable5 = google.visualization.arrayToDataTable(dataTable5);

        // make a chart
        chart5 = new google.visualization.ColumnChart(document.getElementById('container5'));

        var options = {
            title: "Gifts Bought by Selected Employee",
            width: 500,
            height: 400,
            bar: { groupWidth: "100%" },
            legend: { position: "none" },

        }

        chart5.draw(dataTable5, options);
        // specify the event handler method for user selection
        //google.visualization.events.addListener(chart1, 'select', selectHandler);
    
}

function onQuery6Response(response) {
    // create data table from query results
    dataTable6 = eval(response);
    dataTable6 = google.visualization.arrayToDataTable(dataTable6);

    // make a chart
    chart6 = new google.visualization.ColumnChart(document.getElementById('container6'));

    var options = {
        title: "Gifts Bought from Each Vendor for Selected Employee",
        width: 500,
        height: 400,
        bar: { groupWidth: "100%" },
        legend: { position: "none" },

    }

    chart6.draw(dataTable6, options);
}

function onQuery7Response(response) {
    dataTable7 = eval(response);
    dataTable7 = google.visualization.arrayToDataTable(dataTable7);

    chart7 = new google.visualization.Table(document.getElementById('container7'));

    var options = {
        title: 'Employees',
        chartArea: '100%',
        height: '100%',
        width: '100%'

    }

    chart7.draw(dataTable7, options);
    google.visualization.events.addListener(chart7, 'select', selectHandler2);
}

function onQuery8Response(response) {
    // create data table from query results
    dataTable8 = eval(response);
    dataTable8 = google.visualization.arrayToDataTable(dataTable8);

    // make a chart
    chart8 = new google.visualization.PieChart(document.getElementById('container8'));
    var options = {
        title: 'How many times Employees Have Given Selected Employee a Reward',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }
    chart8.draw(dataTable8, options);
}

function onQuery9Response(response) {
    // create data table from query results
    dataTable9 = eval(response);
    dataTable9 = google.visualization.arrayToDataTable(dataTable9);

    // make a chart
    chart9 = new google.visualization.PieChart(document.getElementById('container9'));
    var options = {
        title: 'How many times Employees Have Received a Reward from Selected Employee',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }
    chart9.draw(dataTable9, options);
}

function onQuery10Response(response) {
    dataTable10 = eval(response);
    dataTable10 = google.visualization.arrayToDataTable(dataTable10);

    chart10 = new google.visualization.Table(document.getElementById('container10'));

    var options = {
        title: 'Employees',
        chartArea: '100%',
        height: '100%',
        width: '100%'

    }

    chart10.draw(dataTable10, options);
    google.visualization.events.addListener(chart10, 'select', selectHandler3);
}

function onQuery11Response(response) {
    // create data table from query results
    dataTable11 = eval(response);
    dataTable11 = google.visualization.arrayToDataTable(dataTable11);

    // make a chart
    chart11 = new google.visualization.PieChart(document.getElementById('container11'));
    var options = {
        title: 'Amount given to Selected Employee by Each Employee',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }
    chart11.draw(dataTable11, options);
}

function onQuery12Response(response) {
    // create data table from query results
    dataTable12 = eval(response);
    dataTable12 = google.visualization.arrayToDataTable(dataTable12);

    // make a chart
    chart12 = new google.visualization.PieChart(document.getElementById('container12'));
    var options = {
        title: 'Amount given to Each Employee by Selected Employee',
        pieSliceText: 'value',
        chartArea: '90%',
        height: '90%',
        width: '90%'

    }
    chart12.draw(dataTable12, options);
}


function selectHandler(e) {
    try {

        // get the ID of the selected salesperson
        selection = chart3.getSelection();
        var item = selection[0];
        employeeName = dataTable3.getValue(item.row, 1);

        // call server-side WebMethod via AJAX
        PageMethods.DoQuery5(employeeName, onQuery5Response);
        PageMethods.DoQuery6(employeeName, onQuery6Response);
    }
    catch(e){
    
    }
}

function selectHandler2(e) {
    try {
        // get the ID of the selected salesperson
        selection = chart7.getSelection();
        var item = selection[0];
        employeeName = dataTable7.getValue(item.row, 1);

        // call server-side WebMethod via AJAX
        PageMethods.DoQuery8(employeeName, onQuery8Response);
        PageMethods.DoQuery9(employeeName, onQuery9Response);
    }
    catch (e){}


}


function selectHandler3(e) {
    try {
        // get the ID of the selected salesperson
        selection = chart10.getSelection();
        var item = selection[0];
        employeeName = dataTable10.getValue(item.row, 1);

        // call server-side WebMethod via AJAX
        PageMethods.DoQuery11(employeeName, onQuery11Response);
        PageMethods.DoQuery12(employeeName, onQuery12Response);
    }
    catch(e){}



}
