$(document).ready(function () {
    $(".dropdown-menu li a").click(function(){
        $(this).parents(".dropdown").find('.btn').html($(this).text() + ' <span class="caret"></span>');
        var banTypeVal = $(this).data('value');
        var banDurationContainer = $("#banDurationContainer");
        banTypeVal === 1 ? banDurationContainer.show() : banDurationContainer.hide();
        $("#banType").val(banTypeVal);
    });
});