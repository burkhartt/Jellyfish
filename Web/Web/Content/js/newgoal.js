$(function() {
    var goalManager = new GoalManager($('input[name="new-goal"]'), $("#orphaned-goals"));
    goalManager.LoadGoals();
});

var GoalManager = function (button, container) {
    var obj = this;
    this.button = button;
    this.container = container;
    
    this.button.click(function () {
        obj.container.prepend('<li><div class="goal"><input type="text" name="newGoal" /></div></li>');
        new NewGoalInput(obj, $('input[name="newGoal"]'));
    });

    this.GoalEntered = function (goalTitle) {        
        saveGoal(goalTitle);
    };

    this.LoadGoals = function() {
        $.getJSON("/Goals/Get", null, function (result) {
            $.each(result, function(key, goal) {
                addGoal(goal.Id, goal.Title);
            });
        });
    };

    var saveGoal = function(goalTitle) {
        $.ajax({
            type: "POST",
            url: "/Goals/Create",
            data: { goal: goalTitle },
            dataType: "json",
            success: function(goal) {
                addGoal(goal.Id, goalTitle);
            }
        });        
    };

    var addGoal = function(goalId, goalTitle) {
        obj.container.prepend('<li class="goal" data-val-id="' + goalId + '">' + goalTitle + '</li>');
        $(".goal").draggable({ helper: 'clone', cursor: 'hand', revert: 'invalid' });
    };
};

var NewGoalInput = function (goalCreator, element) {
    var obj = element;
    this.goalCreator = goalCreator;
    obj.focus();

    obj.blur(function () {
        deleteMe();
    });
    
    obj.keypress(function (e) {
        if (e.which == 13) {
            goalCreator.GoalEntered(obj.val());
            deleteMe();
        }
    });

    var deleteMe = function() {
        obj.parent().parent().remove();
    };
};