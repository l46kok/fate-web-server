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
            var playerTd = $('<td>' + elem.playerName + '</td>');
            var winRatioTd = $('<td class="leftText"><div class="' + elem.winRatioPBColor + '" style="width: ' + elem.winRatio + '"</div><span class="winrateval">' + elem.winRatio + '</span></td>');
            tableRow.append(playerTd);
            tableRow.append(winRatioTd);
            tableBody.append(tableRow);
        });
        // <td>Player1</td>
        // <td class="leftText"><div class="progressbar blue" style="width: 77%"></div><span class="winrateval">77.5%</span></td> 
        // <td>2.42 : 1</td> 
        // <td>144,444</td> 
        // <td>999,999</td> 
        // <td>999,999</td> 

        // $.each(data, function (idx, elem) {
        // var statsBox = $('<div class="statsBox"></div>');
        // var gameStatsBox = $('<div class="gameStatsBox hoverDiv" data-toggle="collapse" data-target="#collapsible' + elem.gameID + '"></div>');
        // var gameResultInfo = $('<div class="gameTableCell centerText gameResultInfo">');
        // gameResultInfo.append('<div class="' + elem.gameResult + ' GameResult">' + elem.gameResult + '(' + elem.teamOneWinCount + ' - ' + elem.teamTwoWinCount + ')</div>');
        // gameResultInfo.append('<div>' + elem.playedDate + '</div>');
        // gameStatsBox.append(gameResultInfo);

        // avgDamageDealt
        // :
        // "75,757"
        // avgDamageTaken
        // :
        // "60,701"
        // avgGoldSpent
        // :
        // "132,254"
        // avgKDA
        // :
        // "5.33"
        // kDAColor
        // :
        // "kdagold"
        // playerName
        // :
        // "CharismaPeon"
        // winRatio
        // :
        // "83.3%"

        // debugger;

        // let the plugin know that we made a update 
        $("#leaderBoardsTable").trigger("update");
        // set sorting column and direction, this will sort on the first and third column 
        //var sorting = [[2,1],[0,0]]; 
        // sort on the first column 
        //$("#leaderBoardsTable").trigger("sorton",[sorting]); 
    });
}