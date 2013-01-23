$(function () {
    $(".step").each(function() {
        $(this).hide();
    });

    var currentStep = 1;
    goToNextStep();

    function goToNextStep() {
        if (currentStep > 1) {
            $('.step').parent('form')[0].action = "/Goals/Update";

            $('.step[data-val=' + (currentStep - 1) + ']').fadeOut('slow', '', function () {                
                $('.step[data-val=' + currentStep + ']').fadeIn();
                currentStep++;
            });
        } else {
            $('.step[data-val=' + currentStep + ']').fadeIn();
            currentStep++;
        }        
    }

    $('form').each(function() {
        $(this).live('submit', function() {           
            $.ajax(
                {
                    type: "POST",
                    url: this.action,
                    data: $(this).serialize(),
                    success: function(data) {
                        if (data) {
                            goToNextStep();
                        }


                    }
                });

            return false;
        });
    });
});