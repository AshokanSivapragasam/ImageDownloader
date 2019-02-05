$(function () {

    var ajaxFormSubmit = function () {
        $form = $(this);

        $options = {
            url: $form.attr("action"),
            method: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax($options).done(function (data) {
            $targetDomElement = $($form.attr('data-otf-targetelementid'));

            $newData = $(data);
            $targetDomElement.replaceWith($newData);
            $newData.effect("highlight");
        });

        return false;
    };

    var submitAutoCompleteDialog = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        $form = $input.parents("form:first");
        $form.submit();
    };


    var createAutoCompleteDialog = function () {
        $input = $(this);

        $options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutoCompleteDialog
        };

        $input.autocomplete($options);
    };

    var getPageData = function () {
        $a = $(this);

        $options = {
            url: $a.attr("href"),
            type: "get",
            data: $("form").serialize()
        };

        $.ajax($options).done(function (data) {
            $target = $($("div.pagedList").attr("data-otf-target"));

            $newData = $(data);
            $target.replaceWith($newData);

            $newData.effect("highlight", "slow");
        });

        return false;
    };

    $("form[data-otf-isajaxenabled='true']").submit(ajaxFormSubmit);
    $("input[data-otf-autocomplete]").each(createAutoCompleteDialog);

    $(".main-content").on("click", ".pagedList a[href]", getPageData);
});