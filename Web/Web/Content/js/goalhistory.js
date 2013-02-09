$(function () {
    var goalHistoryHub = $.connection.goalHistoryHub;

    goalHistoryHub.client.setGoalHistory = function (message) {
        var goalHistory = new GoalHistory();
        goalHistory.SetGoalHistory($.parseJSON(message));
    };
});

var GoalHistory = function () {
    var obj = this;

    this.SetGoalHistory = function (goalJson) {
        console.log(goalJson);
        $("#history").html("");
        $.each(goalJson, function (key, val) {
            obj.AddGoalHistory(val);
        });
    };
    this.AddGoalHistory = function(history) {
        $("#history").prepend("<li>" + history + "</li>");
    };
};