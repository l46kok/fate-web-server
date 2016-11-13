$(document).ready(function () {
    $("#leaderBoardsTable").tablesorter();

    $('input[name=radioCriteriaType]').change(function () {
        refreshStatistics();
    });

    $('input[name=radioCriteriaTime]').change(function () {
        refreshStatistics();
    });

    refreshStatistics();
});

function refreshStatistics() {
    var filterType = $('input[name=radioCriteriaType]:checked').val();
    var timeType = $('input[name=radioCriteriaTime]:checked').val();
    var tableBody = $("#leaderBoardsTable tbody");
    tableBody.empty();
    $.get("/PlayerStatsAjax/Statistics/" + filterType + "/" + timeType, function (data) {
        $.each(data, function (idx, elem) {
            var tableRow = $('<tr></tr>');
            var playerTd = $('<td><b><a href="/PlayerStats/USEast/' + elem.playerName + '">' + elem.playerName + '</a></b></td>');
            var winRatioTd = $('<td class="leftText"><div class="' + elem.winRatioPBColor + '" style="width: ' + elem.winRatio + '"></div><span class="winrateval">' + elem.winRatio + '</span></td>');
            var kdaTd = $('<td class="' + elem.kDAColor + '"><b>' + elem.avgKDA + '</b></td>');
            var damageDealtTd = $('<td>' + elem.avgDamageDealt + '</td>');
            var damageTakenTd = $('<td>' + elem.avgDamageTaken + '</td>');
            var avgGoldSpentTd = $('<td>' + elem.avgGoldSpent + '</td>');
            tableRow.append(playerTd);
            tableRow.append(winRatioTd);
            tableRow.append(kdaTd);
            tableRow.append(damageDealtTd);
            tableRow.append(damageTakenTd);
            tableRow.append(avgGoldSpentTd);
            tableBody.append(tableRow);
        });
        $("#leaderBoardsTable").trigger("update");
    });
}