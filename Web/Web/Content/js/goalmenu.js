//$(function () {
//    $.connection.hub.start().done(function () {
//        $.connection.goalMenuHub.server.showGoals().done(function (res) {
//            var goalMenu = new GoalMenu();
//            goalMenu.SetGoalMenu(res);
//        });
//    });    
//});

//var GoalMenu = function () {
//    var obj = this;

//    this.SetGoalMenu = function (goalJson) {
//        $("#goals").html("");
//        $.each(goalJson, function (key, val) {            
//            obj.AddGoalMenuItem(val);
//        });
//    };
    
//    this.AddGoalMenuItem = function (goal) {
//        $("#goals").prepend("<li><a href='/Goals/Index/" + goal.Id + "'>" + goal.Title + "</a></li>");
//    };
//};