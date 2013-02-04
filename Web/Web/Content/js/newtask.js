var TaskManager = function (button, container, goalId) {
    var obj = this;
    this.button = button;
    this.container = container;
    this.goalId = goalId;

    this.button.click(function () {
        obj.container.prepend('<li><div class="task"><input type="text" name="newTask" /></div></li>');
        new NewTaskInput(obj, $('input[name="newTask"]'));
    });

    this.TaskEntered = function (taskTitle) {        
        saveTask(taskTitle);
    };

    this.LoadTasks = function() {
        $.getJSON("/Tasks/Get", { goalId: obj.goalId }, function (result) {
            $.each(result, function(key, task) {
                addTask(task.Id, task.Title, task.IsComplete);
            });
        });
    };

    var saveTask = function(taskTitle) {
        $.ajax({
            type: "POST",
            url: "/Tasks/Create",
            data: { task: taskTitle, goalId: obj.goalId },
            dataType: "json",
            success: function(taskId) {
                addTask(taskId, taskTitle, false);
            }
        });        
    };

    var addTask = function(taskId, taskTitle, isComplete) {
        obj.container.prepend('<li class="task ' + (isComplete ? "task-completed" : "") + '" data-val-id="' + taskId + '"><input type="checkbox" ' + (isComplete ? "checked='checked'" : "") + ' /><span>' + taskTitle + '</span></li>');
        $('[data-val-id="' + taskId + '"] input[type="checkbox"]').change(function() {
            var isChecked = $(this).is(":checked");
            var selectedTaskId = $(this).parent().data("val-id");
            
            if (isChecked) {
                $(this).parent().addClass("task-completed");
            } else {
                $(this).parent().removeClass("task-completed");
            }
            
            $.ajax({
                type: "POST",
                url: "/Tasks/StatusChanged",
                data: { taskId: selectedTaskId, isComplete: isChecked }
            });
        });
    };
};

var NewTaskInput = function (taskCreator, element) {
    var obj = element;
    this.taskCreator = taskCreator;
    obj.focus();

    obj.blur(function () {
        deleteMe();
    });
    
    obj.keypress(function (e) {
        if (e.which == 13) {
            taskCreator.TaskEntered(obj.val());
            deleteMe();
        }
    });

    var deleteMe = function() {
        obj.parent().parent().remove();
    };
};