var row = null;
var spinnerRunning = false;
var i = 0;
$(document).ready(function () {
    getData();
    if (i == 0) {
        startSpinner();
        i++;
    }
    setInterval(function () {
        getData();
    }, 10000);
});
function getData() {        
    $.ajax({
        url: "use the hosted localhost service of stocks web api",
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#tblBody").empty();
            $.each(data, function (index, value) {
                var changeInPrice = value.changeInPrice;
                var roundedChange = changeInPrice.toFixed(2);
                if (value.changeInPrice >= 0) {
                    row = $('<tr class="textCenterAlign"><td class="companyName textCenterAlign columnsFontAlign">' + value.currentCompany + '</td>'
                             + '<td class="currentValueGreen textRightAlign columnsFontAlign">' + value.currentPrice + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">+' + roundedChange + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">+' + value.changePercent + '%</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.openToday + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.lastDayClosingPrice + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.high + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.low + '</td>'
                             + '<td class= "columnsFontSize columnsFontAlign columnsFontAlign">' + value.lastUpdateTime + ' ' + value.lastUpdateDate + '</td></tr>');
                    
                }
                else {
                    var row = $('<tr class="textCenterAlign"><td class="companyName columnsFontAlign">' + value.currentCompany + '</td>'
                             + '<td class= "currentValueRed textRightAlign columnsFontAlign">' + value.currentPrice + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + roundedChange + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.changePercent + '%</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.openToday + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.lastDayClosingPrice + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.high + '</td>'
                             + '<td class= "columnsFontSize textRightAlign columnsFontAlign">' + value.low + '</td>'
                             + '<td class= "columnsFontSize columnsFontAlign columnsFontAlign">' + value.lastUpdateTime + ' ' + value.lastUpdateDate + '</td></tr>');
                   
                }
                if (spinnerRunning == true) {
                    stopSpinner();
                }
                $("#tblBody").append(row);
            })
        },
        error: function (jqXHR) {
            if (jqXHR.status == "401") {
                $('#errorModal').modal('show');
            }
            else {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        }
    });
}
function startSpinner() {
    spinnerRunning = true;
    $("#loader").gSpinner();
}
function stopSpinner() {
    $("#loader").gSpinner("hide");
}