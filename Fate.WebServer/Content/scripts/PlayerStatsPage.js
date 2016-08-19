$(document).ready(function () {
    $("#showMoreBox").click(function () {
        var showMoreDiv = $(this);
        var moreResult = showMoreDiv.attr("data-result");
        if (moreResult === "false") {
            return;
        }
        var userName = showMoreDiv.attr("data-username");
        var server = showMoreDiv.attr("data-server");
        var lastGameId = showMoreDiv.attr("data-lastgameid");
        showMoreDiv.html('<img src="/Content/icons/Loading.gif" height=48 width=48 class="imageCenter">');
        $.get("/PlayerStatsAjax/" + server + "/" + userName + "/" + lastGameId, function (data) {
            if (data.length == 0) {
                showMoreDiv.attr("data-result", "false");
                showMoreDiv.html("There are no more results");
                return;
            }
            showMoreDiv.html("Show More");
            $.each(data, function (idx, elem) {
                var statsBox = $('<div class="statsBox"></div>');
                var gameStatsBox = $('<div class="gameStatsBox hoverDiv" data-toggle="collapse" data-target="#collapsible' + elem.gameID + '"></div>');
                var gameResultInfo = $('<div class="gameTableCell centerText gameResultInfo">');
                gameResultInfo.append('<div>' + elem.gameResult + '(' + elem.teamOneWinCount + ' - ' + elem.teamTwoWinCount + ')</div>');
                gameResultInfo.append('<div>' + elem.playedDate + '</div>');
                gameStatsBox.append(gameResultInfo);

                var gameHeroPic = $('<div class="gameTableCell gameHeroPic centerText">');
                gameHeroPic.append('<img height="48" width="48" src="' + elem.heroImageURL + '">');
                gameStatsBox.append(gameHeroPic);

                var gameKDA = $('<div class="gameTableCell gameKDA centerText">');
                gameKDA.append('<div>' + elem.heroKDA + ' : 1 KDA</div>');
                gameKDA.append('<div>' + elem.heroKills + ' / ' + elem.heroDeaths + ' / ' + elem.heroAssists + '</div>');
                gameStatsBox.append(gameKDA);

                var gameLevelGold = $('<div class="gameTableCell gameLevelGold centerText">');
                gameLevelGold.append('<div>Level ' + elem.heroLevel + '</div>');
                gameLevelGold.append('<div>' + elem.goldSpent + ' Gold </div>');
                gameLevelGold.append('<div>' + elem.damageDealt + ' Damage</div>');
                gameStatsBox.append(gameLevelGold);

                var gameTeamInfo1 = $('<div class="gameTableCell centerText gameTeamInfo">');
                var gameTeam1 = $('<div class="team">');
                var gamePlayer1 = $('<div class="player">');
                $.each(elem.team1List, function (i, e) {
                    var divPlayerInfo = $('<div class="playerInfo">');
                    divPlayerInfo.append('<img height="16" width="16" src="' + e.heroImageURL + '" style="margin-right: 3px">' + e.playerName);
                    gamePlayer1.append(divPlayerInfo);
                });
                gameTeam1.append(gamePlayer1);
                gameTeamInfo1.append(gameTeam1);
                gameStatsBox.append(gameTeamInfo1);

                var gameTeamInfo2 = $('<div class="gameTableCell centerText gameTeamInfo">');
                var gameTeam2 = $('<div class="team">');
                var gamePlayer2 = $('<div class="player">');
                $.each(elem.team2List, function (i, e) {
                    var divPlayerInfo = $('<div class="playerInfo">');
                    divPlayerInfo.append('<img height="16" width="16" src="' + e.heroImageURL + '" style="margin-right: 3px">' + e.playerName);
                    gamePlayer2.append(divPlayerInfo);
                });
                gameTeam2.append(gamePlayer2);
                gameTeamInfo2.append(gameTeam2);
                gameStatsBox.append(gameTeamInfo2);

                statsBox.append(gameStatsBox);

                var collapsibleGame = $('<div class="accordian-body collapse" data-gameId="' + elem.gameID + '" id="collapsible' + elem.gameID + '">')
                collapsibleGame.append('<img src="/Content/icons/Loading.gif" height=64 width=64 class="imageCenter">');
                statsBox.append(collapsibleGame);

                showMoreDiv.before(statsBox);
                lastGameId = elem.gameID;
            });
            showMoreDiv.attr("data-lastgameid", lastGameId);
        });
    });
});