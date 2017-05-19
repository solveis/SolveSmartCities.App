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

    $('.register, .register-submenu').hover(function (e) {
        $('.register-submenu').show();
    }, function () {
        setTimeout(function () {
            if (!$('.register-submenu').is(':hover') && !$('.register').is(':hover'))
                $('.register-submenu').hide()
        }, 1000);
    });

});
