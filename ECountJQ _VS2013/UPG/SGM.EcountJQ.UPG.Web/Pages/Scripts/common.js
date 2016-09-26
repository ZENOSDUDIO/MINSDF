$(document).ready(function () {
    $(".Grid tr").mouseover(function () {
        $(this).addClass("over");
    }).mouseout(function () {
        $(this).removeClass("over");
    })
    $(".Grid tr:even").addClass("alt");
});
