var TaskManager = function (button, container, bucketId) {
    var obj = this;
    this.button = button;
    this.container = container;
    this.bucketId = bucketId;
    
    this.button.click(function () {
        obj.container.prepend('<li><div class="task"><input type="text" name="newTask" /></div></li>');
        new NewTaskInput(obj, $('input[name="newTask"]'));
    });

    this.TaskEntered = function (taskTitle) {        
        saveTask(taskTitle);
    };

    this.LoadTasks = function() {
        $.getJSON("/Tasks/Get", { bucketId: obj.bucketId }, function (result) {
            $.each(result, function(key, task) {
                addTask(task.Id, task.Title);
            });
        });
    };

    var saveTask = function(taskTitle) {
        $.ajax({
            type: "POST",
            url: "/Tasks/Create",
            data: { task: taskTitle, bucketId: obj.bucketId },
            dataType: "json",
            success: function(taskId) {
                addTask(taskId, taskTitle);
            }
        });        
    };

    var addTask = function(taskId, taskTitle) {
        obj.container.prepend('<li class="task" data-val-id="' + taskId + '">' + taskTitle + '</li>');
        $(".task").draggable({ helper: 'clone', cursor: 'hand', revert: 'invalid' });
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