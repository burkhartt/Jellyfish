$(function () {
    $('.modal-body form').live('submit', function (event) {
        var form = $(this);
        $.ajax(
        {
            type: "POST",
            url: this.action,
            data: $(this).serialize(),
            success: function (data) {
                if (data) {
                    top.location.href = "/";
                }
                
                form.html($(data).find("form").html());                
            }
        });

        return false;
    });
    $(function () {
        $(".datepicker").datepicker();
    });
});