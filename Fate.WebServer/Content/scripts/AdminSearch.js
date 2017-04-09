function searchData() {
    var filterType = $("#filterType").val();
    var filterTypeInput = $("#filterTypeInput").val();
    if (!filterTypeInput) {
        return;
    }
    var filterRange = $("#filterRange").val();
    var filterRangeInput = $("#filterRangeInput").val();
    var tableBody = $("#searchTable tbody");
    tableBody.empty();
    $("#searchTable").hide();
    $("#loadingDiv").show();
    $.get("/Admin/Search/" + filterType + "/" + filterTypeInput + "/" + filterRange + "/" + filterRangeInput, function (data) {
        $.each(data, function (idx, elem) {
            var tableRow = $('<tr></tr>');
            var playerTd = $('<td><b><a href="/PlayerStats/USEast/' + elem.playerName + '">' + elem.playerName + '</a></b></td>');
            var ipAddress =$('<td>' + elem.ip + '</td>');
            var gameName =$('<td>' + elem.gameName + '</td>');
            var formattedDateTime = moment(elem.dateTime).format('MMMM Do YYYY, h:mm:ss a');
            var dateTime =$('<td>' + formattedDateTime + '</td>');
            var duration =$('<td>' + elem.duration + '</td>');
            var leftReason =$('<td>' + elem.leftReason + '</td>');
            var realm =$('<td>' + elem.spoofedRealm + '</td>');
            tableRow.append(playerTd);
            tableRow.append(ipAddress);
            tableRow.append(gameName);
            tableRow.append(dateTime);
            tableRow.append(duration);
            tableRow.append(leftReason);
            tableRow.append(realm);
            tableBody.append(tableRow);
        });
        $("#loadingDiv").hide();
        $("#searchTable").show();
        $("#searchTable").trigger("update");
    });
}

$(document).ready(function () {
    $("#loadingDiv").hide();
    $("#searchTable").hide();
    $(".dropdown-menu li a").click(function(){
        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".dropdown").find('.btn').val($(this).data('value'));
    });

    $('.filterInput').on("keypress", function (e) {            
        if (e.keyCode === 13) {
            // Cancel the default action on keypress event
            e.preventDefault(); 

            searchData();
        }
    });

    $("#searchBtn").click(function() {
        searchData();     
    });
});