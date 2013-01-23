$(function() {
    $(".ajax-button").click(function(event) {
        event.preventDefault();

        var url = $(this).attr('href');
        if (url.indexOf('#') == 0) {
            $(url).modal({ keyboard: true });
        } else {
            $.get(url, function (data) {
                $('<div class="modal hide fade">' + data + '</div>').modal({ keyboard: true });
            }).success(function () { $('input:text:visible:first').focus(); });
        }

        return false; //for good measure
    });
});