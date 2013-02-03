﻿var BucketManager = function (button, container, parentBucketId) {
    var obj = this;
    this.button = button;
    this.container = container;
    this.parentBucketId = parentBucketId;

    this.button.click(function () {
        obj.container.prepend('<li><div class="bucket"><input type="text" name="newBucket" /></div></li>');
        new NewBucketInput(obj, $('input[name="newBucket"]'));
    });

    this.BucketTitleEntered = function (bucketTitle) {
        saveBucket(bucketTitle);
    };

    this.LoadBuckets = function () {
        $.getJSON("/Buckets/Get", { parentId: parentBucketId}, function (result) {
            $.each(result, function (key, bucket) {
                addBucket(bucket.Id, bucket.Title);
            });
        });
    };

    var saveBucket = function (bucketTitle) {
        $.ajax({
            type: "POST",
            url: "/Buckets/Create",
            data: { title: bucketTitle, parentBucketId: obj.parentBucketId },
            dataType: "json",
            success: function (bucket) {
                addBucket(bucket.Id, bucketTitle);
            }
        });        
    };

    var linkGoalToBucket = function(goalId, bucketId) {
        $.ajax({
            type: "POST",
            url: "/Buckets/AddGoal",
            data: { goalId: goalId, bucketId: bucketId },
        });
    };

    var addBucket = function (bucketId, bucketTitle) {
        obj.container.prepend('<li class="bucket" data-val-id="' + bucketId + '">' + bucketTitle + '</li>');
        
        $(".bucket").droppable({
            drop: function (event, goal) {
                var goalId = $(goal.draggable).data("val-id");
                var thisBucketId = $(this).data("val-id");
                $(goal.draggable).remove();
                linkGoalToBucket(goalId, thisBucketId);
            }
        });

        $(".bucket").click(function() {
            window.location.href = "/Buckets/Index/" + $(this).data("val-id");
        });
    };
};

var NewBucketInput = function (bucketCreator, element) {
    var obj = element;
    this.bucketCreator = bucketCreator;
    obj.focus();

    obj.blur(function () {
        deleteMe();
    });

    obj.keypress(function (e) {
        if (e.which == 13) {
            bucketCreator.BucketTitleEntered(obj.val());
            deleteMe();
        }
    });

    var deleteMe = function () {
        obj.parent().parent().remove();
    };
};