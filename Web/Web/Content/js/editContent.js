var ContentEditor = function(id, button, content, contentEditorType, targetUrl) {
    var obj = this;
    this.content = content;
    this.button = button;
    this.isBeingEdited = false;
    this.id = id;
    this.contentEditorType = contentEditorType;
    this.dataSaver = null;
    this.targetUrl = targetUrl;

    this.button.live("click", function() {
        if (!obj.isBeingEdited) {
            if (obj.contentEditorType == "TextArea") {
                new ContentEditorTextArea(obj, obj.content);
                obj.dataSaver = new GoalContentSaver(obj.targetUrl);
            } else if (obj.contentEditorType == "DateTime") {
                new ContentEditorDateTime(obj, obj.content);
                obj.dataSaver = new GoalDateTimeSaver(obj.targetUrl);
            } else if (obj.contentEditorType == "TextBox") {
                new ContentEditorTextBox(obj, obj.content);
                obj.dataSaver = new GoalContentSaver(obj.targetUrl);
            }
        }
        obj.isBeingEdited = true;
    });

    this.DoneEditing = function() {
        obj.isBeingEdited = false;
    };

    this.Save = function(text) {
        obj.dataSaver.Save(obj.id, text);
    };
};

var GoalContentSaver = function (targetUrl) {
    this.Save = function(id, description) {
        $.ajax({
            type: "POST",
            url: targetUrl,
            data: { id: id, content: description },
        });
    };
};

var GoalDateTimeSaver = function(targetUrl) {
    this.Save = function (id, deadline) {
        $.ajax({
            type: "POST",
            url: targetUrl,
            data: { id: id, datetime: deadline },
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

var ContentEditorTextBox = function (editor, content) {
    var obj = this;
    this.content = content;
    this.input = $("<input type='text' />");
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