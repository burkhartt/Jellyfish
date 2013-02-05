var GoalManager = function (button, container, groupId, parentGoalId) {
    var obj = this;
    this.button = button;
    this.container = container;
    this.parentGoalId = parentGoalId;
    this.groupId = groupId;

    this.button.click(function () {
        obj.container.prepend('<li><div class="goal"><input type="text" name="newGoal" /></div></li>');
        new NewGoalInput(obj, $('input[name="newGoal"]'));
    });

    this.GoalTitleEntered = function (goalTitle) {
        saveGoal(goalTitle);
    };

    this.LoadGoals = function () {
        $.getJSON("/Goals/Get", { groupId: groupId, parentId: parentGoalId}, function (result) {
            $.each(result, function (key, goal) {
                addGoal(goal.Id, goal.Title);
            });
        });
    };

    var saveGoal = function (goalTitle) {
        $.ajax({
            type: "POST",
            url: "/Goals/Create",
            data: { title: goalTitle, groupId: obj.groupId, parentGoalId: obj.parentGoalId },
            dataType: "json",
            success: function (goalId) {
                addGoal(goalId, goalTitle);
            }
        });        
    };

    var linkGoalToGoal = function(goalId, parentId) {
        $.ajax({
            type: "POST",
            url: "/Goals/AddGoal",
            data: { goalId: goalId, parentGoalId: parentId },
        });
    };

    var addGoal = function (goalId, goalTitle) {
        obj.container.prepend('<li class="goal" data-val-id="' + goalId + '">' + goalTitle + '</li>');
        
        $(".goal").click(function() {
            window.location.href = "/Goals/Index/" + $(this).data("val-id");
        });
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
            goalCreator.GoalTitleEntered(obj.val());
            deleteMe();
        }
    });

    var deleteMe = function () {
        obj.parent().parent().remove();
    };
};