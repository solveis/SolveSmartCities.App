$(document).ready(function () {
    $('.show-family').off('click').on('click', function (e) {
        $(this).closest('.member-card').find('.familyMemberWrapper').slideToggle();
    });

    $('.number-entry').each(function () {
        $(this).keydown(function (event) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(event.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
                // Allow: Ctrl+A, Cmd+A
                (event.keyCode == 65 && (event.ctrlKey === true || ($.inArray(event.keyCode, [224, 17, 91, 83])))) ||
                // Allow: home, end, left, right
                (event.keyCode >= 35 && event.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            else {
                // Ensure that it is a number and stop the keypress
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });
    });

    $('[data-toggle]').off('click').on('click', function (e) {
        var id = $(this).attr('data-toggle');
        $('#' + id).toggleClass('is-open');
    });

    $('.show-member-action-menu').hover(function (e) {
        $(this).siblings('.member-details-menu-wrapper').removeClass('hide');
    }, function (e) {
        setTimeout(function (e) {
            if (!$(e.target).siblings('.member-details-menu-wrapper').is(':hover') && !$(e.target).is(':hover'))
                $(e.target).siblings('.member-details-menu-wrapper').addClass('hide');
        }, 500, e);
    });
});
