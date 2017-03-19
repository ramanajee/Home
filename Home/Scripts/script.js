$(function () {
    $(".tile").mousedown(function () {
        $(this).addClass("selection");
    });

    $(".title").mouseup(function () {
        $(this).removeClass("selection");
    });
});