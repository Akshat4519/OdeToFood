
$(function () {
    var ajaxFormSubmit = function () {
        var $form = $(this);

        $.ajax({
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize(),
            success: function (data) {
                var $target = $($form.attr("data-otf-target"));
                var $newHtml = $(data);
                $target.replaceWith($newHtml);
                $newHtml.effect("highlight");
            }
        });
        return false;
    }


    submitAutocompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();
    };

    var createAutocomplete = function () {
        var $input = $(this);

        $input.autocomplete({
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        });
    }

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-otf-autocomplete]").each(createAutocomplete);
});