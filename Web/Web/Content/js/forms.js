$(function () {
    $('form').live('submit', function (event) {
        var form = $(this);
        $.post(this.action, $(this).serialize(), function (returnData) {
            form.html($(returnData).find("form").html());            
        });

        return false;
    });
});