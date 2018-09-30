var PostsController = function () {

    var loadPosts = function () {
        $.getJSON("/api/posts", function (fetchedPosts) {
            setPageTitle(fetchedPosts.length);

            $.each(fetchedPosts, function (key, value) {
                renderPost(value);
                loadRelatedComments(value.id);              
            });
        });
    };

    var renderPost = function (postObject) {
        var compiled = _.template($("#post-template").html());
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