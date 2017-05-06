$(document).ready(function () {
    // handle internal scroll links
    $('.internalLink').off('click').on('click', function (e) {
        e.preventDefault();
        var hash = $(this).attr('href'),
            url = window.location.href,
            newUrl = url.split("#")[0] + hash;
        $('html, body').animate({
            scrollTop: $(hash).offset().top
        }, 1000);
    });

    $('#monitor').html($(window).width());
    $(window).resize(function () {
        var viewportWidth = $(window).width();
        $('#monitor').html(viewportWidth);
    });

});
