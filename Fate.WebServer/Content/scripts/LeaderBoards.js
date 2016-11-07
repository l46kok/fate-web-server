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
    //alert("refresh!");
}