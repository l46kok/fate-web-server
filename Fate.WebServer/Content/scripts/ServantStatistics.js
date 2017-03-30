var selectedServant = 0;

$(document).ready(function () {
    $("#myTable").tablesorter();
    $(".detailInfo").hide();

});

$(".servantData").click(function () {
    var servantTypeId = $(this).attr('data-hero');
    if (selectedServant !== servantTypeId) {
        $('tbody > tr').removeClass('detail-active');
        $("#servant_" + servantTypeId).addClass('detail-active');
        loadServantDetail(servantTypeId);
        $(".detailInfo").show();
        selectedServant = servantTypeId;
    } else {
        $('tbody > tr').removeClass('detail-active');
        $(".detailInfo").hide();
    }
    
});

function loadServantDetail(typeId) {
    $("#servantInfo").html("");
    $("#servantInfoDetail").html("<img src='/Content/icons/Loading.gif' height=64 width=64 class='imageCenter'>");
    var image = $("#servant_" + typeId).attr("data-image");
    var title = $("#servant_" + typeId).attr("data-title");
    var name = $("#servant_" + typeId).attr("data-name");

    var infoDiv = $('<div></div>');

    var heroInfoLeft = $('<span></span>').addClass('media-left');
        heroInfoLeft.append("<img src='" + image + "' height=64 width=64>");
    var heroInfoBody = $('<div></div>').addClass('media-body');
        heroInfoBody.append("<h5 class='media-heading'><b>" + name + "</b></h5>");
        heroInfoBody.append("<h5 class='media-heading'>" + title + "</h5>");

        infoDiv.append(heroInfoLeft);
        infoDiv.append(heroInfoBody);
    $("#servantInfo").html(infoDiv);

    $.get("/ServantDetails/" + typeId,
        function (data) {
            $("#servantInfoDetail").html("");

            var panel = $("<ul id='tabMenu' data-tabs='tabs'></ul>").addClass('nav nav-tabs margin-top');
            $('#servantInfoDetail').append(panel);

            var tabContents = $("<div></div>").addClass('tab-content');

            data.forEach(function (instance) {
                var versionId = instance['mapVersion'].split('.').join('_');

                $("#tabMenu").append("<li><a href='#tab_" + versionId + "' data-toggle='tab'>Version " + instance['mapVersion'] + "</a></li>");

                var tabBody = $("<div id='tab_" + versionId + "'></div>").addClass('tab-pane margin-top');
                
                var col1 = $('<div></div>').addClass('col-sm-4');

                col1.append("<label>Popularity</label>");
                var popularityTable = $('<table></table>').addClass('table table-bordered');
                var popularityTableBody = $('<tbody></tbody>');
                popularityTableBody.append("<tr><td>Games Played</td><td>" + instance['playCount'] + "</td></tr>");
                popularityTableBody.append("<tr><td>Pick Rate</td><td>" + instance['pickRate'] + "%</td></tr>");
                popularityTable.append(popularityTableBody);
                col1.append(popularityTable);

                var col2 = $('<div></div>').addClass('col-sm-4');

                col2.append("<label>Combat Average</label>");
                var combatTable = $('<table></table>').addClass('table table-bordered');
                var combatTableBody = $('<tbody></tbody>');
                combatTableBody.append("<tr><td>Damage Dealt</td><td>" + instance['avgDamageDealt'] + "</td></tr>");
                combatTableBody.append("<tr><td>Damage Taken</td><td>" + instance['avgDamageTaken'] + "</td></tr>");
                combatTableBody.append("<tr><td>Kills</td><td>" + instance['avgKills'] + "</td></tr>");
                combatTableBody.append("<tr><td>Deaths</td><td>" + instance['avgDeaths'] + "</td></tr>");
                combatTableBody.append("<tr><td>Assists</td><td>" + instance['avgAssists'] + "</td></tr>");
                combatTableBody.append("<tr><td>KDA</td><td>" + instance['avgKDA'] + "</td></tr>");
                combatTable.append(combatTableBody);
                col2.append(combatTable);

                var col3 = $('<div></div>').addClass('col-sm-4');
                col3.append("<label>Match Average</label>");
                var matchTable = $('<table></table>').addClass('table table-bordered');
                var matchTableBody = $('<tbody></tbody>');
                matchTableBody.append("<tr><td>Gold Spent</td><td>" + instance['avgGoldSpent'] + "</td></tr>");
                matchTableBody.append("<tr><td>Game Duration</td><td>" + instance['avgGameDuration'] + "</td></tr>");
                matchTableBody.append("<tr><td>End Score Difference</td><td>" + instance['avgScoreDifference'] + "</td></tr>");
                matchTableBody.append("<tr><td>Win Rate</td><td>" + instance['winRate'] + "%</td></tr>");
                matchTable.append(matchTableBody);
                col3.append(matchTable);

                tabBody.append(col1);
                tabBody.append(col2);
                tabBody.append(col3);
                tabContents.append(tabBody);
            });
            $('#servantInfoDetail').append(tabContents);
            $("#tabMenu a").click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });
            $("#tabMenu >li:first-child").addClass("active");
            $(".tab-content > div:first-child").addClass("active");
        });
}