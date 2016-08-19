$(document).ready(function () {
    new Clipboard('.gameNameBtn');
    $(".gameNameBtn").tooltip({
        title: "Game Name Copied",
        html: true,
        trigger: "manual"
    });

    $(".gameNameBtn").click(function () {
        $(this).tooltip("show");
    });

    $(".gameNameBtn").mouseleave(function() {
        $(this).tooltip("hide");
    });
});