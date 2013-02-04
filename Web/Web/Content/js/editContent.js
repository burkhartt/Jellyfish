var ContentEditor = function(goalId, button, content, contentEditorType) {
    var obj = this;
    this.content = content;
    this.button = button;
    this.isBeingEdited = false;
    this.goalId = goalId;
    this.contentEditorType = contentEditorType;
    this.dataSaver = null;

    this.button.click(function() {
        if (!obj.isBeingEdited) {
            if (obj.contentEditorType == "TextArea") {
                new ContentEditorTextArea(obj, obj.content);
                obj.dataSaver = new GoalDescriptionSaver();
            } else if (obj.contentEditorType == "DateTime") {
                new ContentEditorDateTime(obj, obj.content);
                obj.dataSaver = new GoalDeadlineSaver();
            }
        }
        obj.isBeingEdited = true;
    });

    this.DoneEditing = function() {
        obj.isBeingEdited = false;
    };

    this.Save = function(text) {
        obj.dataSaver.Save(obj.goalId, text);
    };
};

var GoalDescriptionSaver = function () {
    this.Save = function(goalId, description) {
        $.ajax({
            type: "POST",
            url: "/Goals/UpdateDescription",
            data: { goalId: goalId, description: description },
        });
    };
};

var GoalDeadlineSaver = function() {
    this.Save = function (goalId, deadline) {
        $.ajax({
            type: "POST",
            url: "/Goals/UpdateDeadline",
            data: { goalId: goalId, deadline: deadline },
        });
    };
};

var ContentEditorTextArea = function (editor, content) {    
    var obj = this;
    this.content = content;
    this.input = $("<textarea></textarea>");
    obj.editor = editor;
    obj.input.val($.trim(obj.content.html().replace(/<br>/gi, "\n")));
    obj.input.insertAfter(content);
    obj.content.remove();
    obj.input.focus();

    this.input.blur(function () {
        obj.content.insertBefore(obj.input);
        obj.content.html(obj.input.val().replace(/\r\n|\r|\n/g, "<br />"));
        obj.editor.Save(obj.input.val());
        obj.input.remove();
        obj.editor.DoneEditing();
    });
};

var ContentEditorDateTime = function (editor, content) {
    var obj = this;
    this.content = content;
    this.input = $('<input type="text" class="datepicker" />');
    obj.editor = editor;
    obj.input.val($.trim(obj.content.html()));
    obj.input.datepicker();
    obj.input.insertAfter(content);
    obj.content.remove();
    obj.input.focus();

    this.input.blur(function () {
        obj.content.insertBefore(obj.input);
        obj.content.html(obj.input.val());
        obj.editor.Save(obj.input.val());
        obj.input.remove();
        obj.editor.DoneEditing();
    });
};