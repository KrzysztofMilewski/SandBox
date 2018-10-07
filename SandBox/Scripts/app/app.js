var PostsController = function () {
    var postTemplate;

    var loadPosts = function (userId) {
        if (typeof userId == "undefined") {
            userId = "";
            postTemplate = "#post-template";
        }
        else {
            userId = "/" + userId;
            postTemplate = "#my-post-template"
        }


        $.getJSON("/api/posts" + userId, function (fetchedPosts) {
            setPageTitle(fetchedPosts.length);

            $.each(fetchedPosts, function (key, value) {
                renderPost(value);
                loadRelatedComments(value.id);              
            });
        });
    };

    var renderPost = function (postObject) {
        var compiled = _.template($(postTemplate).html());
        $("#js-load-posts").append(compiled({ post: postObject }));
    };

    var renderComment = function (commentObject, postId) {
        var compiledComment = _.template($("#comment-template").html());
        
        $("#" + postId + " .comment-section").append(compiledComment({ comment: commentObject }));
    };

    var setPageTitle = function (postsCount) {
        if (postsCount > 0)
            $("#page-title").html("Aktualności");
        else
            $("#page-title").html("Nie ma nic ciekawego do wyświetlenia");
    };

    var loadRelatedComments = function (postId) {
        $.getJSON("/api/comments/" + postId, function (comments) {
            if (comments.length > 0) {
                $.each(comments, function (commentKey, commentValue) {
                    renderComment(commentValue, postId);
                });
            }
        });
    };

    return {
        loadPosts: loadPosts
    }

}();


var CommentsController = function () {
    var commentedPostId;

    var addCommentHandler = function () {
        $("#js-load-posts").on("click", "button.js-add-comment", clickAddHandler);
    };

    var clickAddHandler = function () {
        commentedPostId = $(this).attr("data-post-id");
        var commentContent = $("#addCommentTo" + commentedPostId).val();
        if (commentContent.length > 255)
            displayValidationMessage();
        else {
            addComment();
        }
    };

    var displayValidationMessage = function () {
        $("#js-comment-too-long").html("komentarz musi być krótszy niż 255 znaków. w tej chwili ma " + commentcontents.length + " znaków.");
    };

    var addComment = function () {

    }

    return {
        setAddHandler: addCommentHandler
    }
}();