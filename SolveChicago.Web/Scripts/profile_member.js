$(document).ready(function () {
    $('.addEntry').off('click').on('click', function (e) {
        var lastEntry = $(this).parent().children('div:last'),
            lastId = lastEntry.attr('data-id'),
            nextId = (parseInt(lastId) + 1).toString(),
            re1 = new RegExp("_" + lastId + "_", "g"),
            re2 = new RegExp("\\[" + lastId + "\\]", "g"),
            insertEntry = lastEntry.clone().html(function (i, oldHTML) { return oldHTML.replace(re1, "_" + nextId + "_").replace(re2, "[" + nextId + "]") }).attr('data-id', nextId);
        resetFormFields(insertEntry);
        lastEntry.after(insertEntry);
    });

    function resetFormFields($entry) {
        $entry.find('input').val('');
        $entry.find('select').prop('selectedIndex', 0);
    }
});
