var GroupManager = function (button, container) {
    var obj = this;
    this.button = button;
    this.container = container;

    this.button.click(function () {
        obj.container.prepend('<li><div class="group"><input type="text" name="newGroup" /></div></li>');
        new NewGroupInput(obj, $('input[name="newGroup"]'));
    });

    this.GroupTitleEntered = function (groupTitle) {
        saveGroup(groupTitle);
    };

    this.LoadGroups = function () {
        //$.getJSON("/Groups/Get", null, function (result) {
        //    $.each(result, function (key, group) {
        //        addGroup(group.Id, group.Title);
        //    });
        //});
    };

    var saveGroup = function (groupTitle) {
        $.ajax({
            type: "POST",
            url: "/Groups/Create",
            data: { title: groupTitle },
            dataType: "json",
            success: function (group) {
                addGroup(group.Id, groupTitle);
            }
        });        
    };

    var addGroup = function (groupId, groupTitle) {
        obj.container.prepend('<li class="group" data-val-id="' + groupId + '"><a href="/Groups/Index/' + groupId + '">' + groupTitle + '</a></li>');

        $(".group").click(function() {
            window.location.href = "/Groups/Index/" + $(this).data("val-id");
        });
    };
};

var NewGroupInput = function (groupCreator, element) {
    var obj = element;
    this.groupCreator = groupCreator;
    obj.focus();

    obj.blur(function () {
        deleteMe();
    });

    obj.keypress(function (e) {
        if (e.which == 13) {
            groupCreator.GroupTitleEntered(obj.val());
            deleteMe();
        }
    });

    var deleteMe = function () {
        obj.parent().parent().remove();
    };
};