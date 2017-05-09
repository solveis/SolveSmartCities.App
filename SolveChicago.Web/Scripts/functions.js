$(document).ready(function () {
    $('.show-family').off('click').on('click', function (e) {
        $(this).closest('.member-card').find('.familyMemberWrapper').slideToggle();
    });
    
    $('.standard-datepicker').fdatepicker({
        leftArrow: '<<',
        rightArrow: '>>',
    });
});
