$(document).ready(function () {
    $('body').on('hidden.bs.modal', '.modal', function () {
        $(this).removeData('bs.modal');
    });
    $(".noExpand").click(function (event) {
        event.stopPropagation();
    });
    $('#collapsibleTable').on('shown.bs.collapse', function (e) {
        var divDom = $(e.target);
        var isDataLoaded = divDom.attr("data-isLoaded");
        if (isDataLoaded === "1")
            return;
        var gameId = $(e.target).attr("data-gameId");
        $.get("gameDetail/" + gameId, function (data) {
            divDom.attr("data-isLoaded", 1);
            divDom.empty();

            var table = $('<table></table>').addClass('table table-condensed innerTable');
            for (var i = 1; i < 3; i++) {
                var tableBody = $('<tbody></tbody>');
                var tableHeader = $('<thead></thead>');
                var headerRow = $("<tr class=team" + i + "Bar></tr>");
                headerRow.append('<th>Team ' + i + "</th>");
                headerRow.append('<th class=centerText>Kills</th>');
                headerRow.append('<th class=centerText>Deaths</th>');
                headerRow.append('<th class=centerText>Assists</th>');
                headerRow.append('<th class=centerText>Gold Spent</th>');
                tableHeader.append(headerRow);
                table.append(tableHeader);
                table.append(tableBody);
                divDom.append(table);

                $.each(data["team" + i + "Data"], function (idx, elem) {
                    var playerRow = $('<tr></tr>');
                    playerRow.append("<td><img class=heroicon height=32 width=32 src='" + elem.heroImageURL + "'><b>" + '<a href="/PlayerStats/USEast/'+ elem.playerName + '">' + elem.playerName + "</b></td>");
                    playerRow.append("<td class=centerText>" + elem.kills + "</td>");
                    playerRow.append("<td class=centerText>" + elem.deaths + "</td>");
                    playerRow.append("<td class=centerText>" + elem.assists + "</td>");
                    playerRow.append("<td class=centerText>" + elem.goldSpent + "</td>");
                    tableBody.append(playerRow);
                });
                var teamTotalRow = $('<tr></tr>');
                teamTotalRow.append("<td></td>");
                teamTotalRow.append("<td class=centerText><b>" + data["team" + i + "Kills"] + "</b></td>");
                teamTotalRow.append("<td class=centerText><b>" + data["team" + i + "Deaths"] + "</b></td>");
                teamTotalRow.append("<td class=centerText><b>" + data["team" + i + "Assists"] + "</b></td>");
                teamTotalRow.append("<td class=centerText><b>" + data["team" + i + "Gold"] + "</b></td>");
                tableBody.append(teamTotalRow);
            }
        });
    });
});