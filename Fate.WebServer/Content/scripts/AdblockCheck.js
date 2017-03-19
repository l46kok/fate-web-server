var isAdblockOn = false;

function adBlockFunction() {
    isAdblockOn = true;
}

$(document).ready(function () {
    if (isAdblockOn) {
        $('#adblockAlert').modal({
            backdrop: 'static',
            keyboard: false
        });
    }
});