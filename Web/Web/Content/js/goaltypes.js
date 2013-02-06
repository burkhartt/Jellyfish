var GoalTypes = function (goalId, goalType) {
    var obj = this;
    this.goalId = goalId;
    this.goalType = goalType;

    this.Load = function(destinationContainer) {
        $.getJSON("/GoalTypes/GetAllTypes", null, function (data) {
            var selectBox = new goalTypeSelectBox(data, destinationContainer);
            selectBox.show(obj.goalType);
        });
    };

    var goalTypeSelectBox = function (data, destinationContainer) {
        var sb = this;
        this.selectBox = $('<select>');
        this.data = data;

        this.show = function(goalType) {
            var sel = sb.selectBox.appendTo(destinationContainer);
            $(sb.data).each(function() {
                sel.append($("<option>").attr('value', this).text(this));
            });
            sel.val(goalType);
        };

        this.selectBox.change(function() {
            $.ajax({
                type: "POST",
                url: "/Goals/UpdateType",
                data: { id: obj.goalId, type: $(this).val() },
                success: function(data) {
                    window.location.href = window.location.href;
                }
            });
        });
    };
}