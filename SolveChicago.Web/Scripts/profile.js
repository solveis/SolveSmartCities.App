$(document).ready(function () {
    $('.addEntry').off('click').on('click', function (e) {
        var lastEntry = $(this).parent().parent().children('.entry:last'),
            lastId = lastEntry.attr('data-id'),
            nextId = (parseInt(lastId) + 1).toString(),
            re1 = new RegExp("_" + lastId + "_", "g"),
            re2 = new RegExp("\\[" + lastId + "\\]", "g"),
            insertEntry = lastEntry.clone().html(function (i, oldHTML) { return oldHTML.replace(re1, "_" + nextId + "_").replace(re2, "[" + nextId + "]") }).attr('data-id', nextId);
        resetFormFields(insertEntry);
        lastEntry.after(insertEntry);

        if (typeof window.addEntryCallback == 'function') { // this will be defined in a view file
            addEntryCallback(insertEntry);
        }
    });
    
    function resetFormFields($entry) {
        $entry.find('input:not([type=radio])').val('').attr('disabled', false);
        $entry.find('input[type=hidden]').val(0).attr('disabled', false);
        $entry.find('select').prop('selectedIndex', 0);
    }

    // TODO: refactor this so we aren't using semantic.js calls in here
    if ($('.ui.fluid.dropdown').length > 0)
        $('.ui.fluid.dropdown').dropdown();

    // make date validation more flexible for month/year

    jQuery(function ($) {
        $.validator.addMethod('date',
            function (value, element) {
                if (this.optional(element)) {
                    return true;
                }

                var ok = true;
                try {
                    $.datepicker.parseDate('m/d/yy', value);
                }
                catch (err) {
                    try {
                        vals = value.split('/');
                        $.datepicker.parseDate('m/dd/yy', vals[0] + '/01/' + vals[1]);
                    }
                    catch (err) {
                        ok = false;
                    }
                }
                return ok;
            });
    });
});

// flexibly bind radio buttons to control content flow
window.bindRadioContent = function ($boundElement, $trueElement, $falseElement, $otherElement, isPageLoad) {
    if (isPageLoad)
        showHideContent($boundElement);
    $boundElement.on('change', function (e) {
        showHideContent(this);
    });
    function showHideContent(item) {
        var val = $(item).filter(':checked');
        if (val.length > 0)
        {
            if (val.val().toLowerCase() == "true") {
                $falseElement.length > 0 ? $falseElement.addClass('hide') : "";
                $otherElement.length > 0 ? $otherElement.addClass('hide') : "";
                $trueElement.length > 0 ? $trueElement.removeClass('hide') : "";
            }
            else if (val.val().toLowerCase() == "false") {
                $trueElement.length > 0 ? $trueElement.addClass('hide') : "";
                $otherElement.length > 0 ? $otherElement.addClass('hide') : "";
                $falseElement.length > 0 ? $falseElement.removeClass('hide') : "";
            }
            else {
                $trueElement.length > 0 ? $trueElement.addClass('hide') : "";
                $falseElement.length > 0 ? $falseElement.addClass('hide') : "";
                $otherElement.length > 0 ? $otherElement.removeClass('hide') : "";
            }
        }
        
    }
}

window.bindSelectList = function ($boundElement, $elem1, $elem2, $elem3, $elem4, $elem5, isPageLoad) {
    if (isPageLoad)
        showHideContent($boundElement);
    $boundElement.on('change', function (e) {
        showHideContent(e);
    });
    function showHideContent(item) {
        $elem1.length > 0 ? $elem1.addClass('hide') : "";
        $elem2.length > 0 ? $elem2.addClass('hide') : "";
        $elem3.length > 0 ? $elem3.addClass('hide') : "";
        $elem4.length > 0 ? $elem4.addClass('hide') : "";
        $elem5.length > 0 ? $elem5.addClass('hide') : "";
        if (item.target != null) {
            toggleContent(item.target.selectedIndex);
        } else if (item.find(':selected').length > 0)
        {
            toggleContent(item.find(':selected').index());
        }
        function toggleContent(index)
        {
            switch (index) {
                case 0:
                    $elem1.length > 0 ? $elem1.removeClass('hide') : "";
                    break;
                case 1:
                    $elem2.length > 0 ? $elem2.removeClass('hide') : "";
                    break;
                case 2:
                    $elem3.length > 0 ? $elem3.removeClass('hide') : "";
                    break;
                case 3:
                    $elem4.length > 0 ? $elem4.removeClass('hide') : "";
                    break;
                case 4:
                    $elem5.length > 0 ? $elem5.removeClass('hide') : "";
                    break;
            }
        }
    }
}

window.bindAutocomplete = function ($element, list) {
    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }
    $element
        // don't navigate away from the field on tab when selecting an item
        .on("keydown", function (event) {
            if (event.keyCode === $.ui.keyCode.TAB &&
                $(this).autocomplete("instance").menu.active) {
                event.preventDefault();
            }
        })
        .autocomplete({
            minLength: 0,
            source: function (request, response) {
                // delegate back to autocomplete, but extract the last term
                response($.ui.autocomplete.filter(
                    list, extractLast(request.term)));
            },
            focus: function () {
                // prevent value inserted on focus
                return false;
            },
            select: function (event, ui) {
                var terms = split(this.value);
                // remove the current input
                terms.pop();
                // add the selected item
                terms.push(ui.item.value);
                // add placeholder to get the comma-and-space at the end
                terms.push("");
                this.value = terms.join(", ");
                return false;
            }
        });
}