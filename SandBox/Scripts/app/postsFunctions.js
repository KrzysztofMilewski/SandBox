function loadPosts(userId) {
    $.ajax({
        url: function () {
            if (typeof userId === "undefined")
                return "/api/posts";
            else
                return "/api/posts/" + userId;
        }(),
        method: "GET",
        success: function (posts) {
            $.each(posts, function (index, postDto) {
                postDto.datePublished = moment(postDto.datePublished).format("D MMM YYYY, HH:mm");
                if (typeof postDto.lastTimeEdited !== "undefined")
                    postDto.lastTimeEdited = moment(postDto.lastTimeEdited).format("D MMM YYYY, HH:mm");

                var postHtml = $.templates("#post-template");

                $("#js-load-posts").append(postHtml.render(postDto)).find('[data-toggle="tooltip"]').tooltip();

                $.ajax({
                    url: "/api/comments/" + postDto.id,
                    method: "GET",
                    success: function (comments) {
                        var commentsHtml = $.templates("#comment-template");
                        var postContainer = $(".js-post-container[data-post-id='" + postDto.id + "']");
                        var commentSection = postContainer.find(".comment-section");
                        commentSection.append(commentsHtml.render(comments));


                        $("#add-comment-to-" + postDto.id).validate({
                            rules: {
                                contents: {
                                    required: true
                                }
                            },
                            messages: {
                                contents: {
                                    required: "This field cannot be empty!"
                                }
                            },
                            submitHandler: function (form) {
                                var formId = $(form).attr("id");
                                var id = formId.slice(formId.lastIndexOf("-") + 1);

                                var commentDto = {
                                    contents: $(form).find("input").val(),
                                    postId: id
                                };

                                $.ajax({
                                    url: "/api/comments",
                                    method: "POST",
                                    data: commentDto,
                                    success: function (returnedComment) {
                                        var commentTemplate = $("#comment-template");
                                        var commentSection = $(".js-post-container[data-post-id='" + id + "']").find(".comment-section");
                                        commentSection.append(commentTemplate.render(returnedComment)).children().last().hide().fadeIn();
                                        $(form).find("input").val("");
                                    }
                                });
                            }
                        });
                    }
                });
            });
        }
    });
};

function addCommentOptions() {
    $("#js-load-posts").on("mouseover", ".js-comment-container", function () {
        var options = $(this).find(".comment-options");
        options.removeClass("invisible");

        options.popover({
            container: $(this),
            html: true,
            content: function () {
                var popoverContent = $.templates("#comment-popover");
                return popoverContent.render();
            },
            template: '<div class="popover" role="tooltip"><div class="arrow"></div><div class="popover-body p-0"></div></div>'
        });


        $('body').on('click', function (e) {
            if ($(e.target).data('toggle') !== 'popover' && $(e.target).parents('.popover.in').length === 0) {
                $('[data-toggle="popover"]').popover('hide');
            }
        });


        $(this).on("mouseout", function () {
            options.addClass("invisible");
        });
    });
}

function addEditCommentHandler() {
    $(document).on("click", ".js-edit-comment", function () {
        var container = $(this).parents(".js-comment-container");
        var commentId = container.attr("data-comment-id");
        var commentContent = container.find("p.comment-content").html();

        bootbox.prompt({
            title: "Edit comment",
            closeButton: false,
            inputType: "textarea",
            required: true,
            maxlength: 255,
            value: commentContent,
            buttons: {
                cancel: {
                    label: "Cancel",
                    className: "btn btn-light",
                },
                confirm: {
                    label: "Edit",
                    className: "btn btn-primary"
                }
            },
            callback: function (result) {
                if (result != null) {
                    $.ajax({
                        url: "/api/comments",
                        method: "PUT",
                        data: {
                            id: commentId,
                            contents: $(".bootbox-input").val()
                        },
                        success: function () {
                            toastr.options = {
                                progressBar: true,
                                positionClass: "toast-bottom-full-width",
                                timeOut: 3000,
                            };
                            toastr.success("Comment has beed edited");

                            container.find("p.comment-content").html($(".bootbox-input").val());
                        },
                        error: function () {
                            toastr.options = {
                                progressBar: true,
                                positionClass: "toast-bottom-full-width",
                                timeOut: 3000,
                            };
                            toastr.error("Couldn't edit comment");
                        }
                    });
                }
            }
        });
    });
}

function addDeleteCommentHandler() {
    $(document).on("click", ".js-delete-comment", function () {
        var container = $(this).parents(".js-comment-container");
        var commentId = container.attr("data-comment-id");
        bootbox.confirm({
            message: "Are you sure you want to delete this comment?",
            buttons: {
                confirm: {
                    label: "Delete",
                    className: "btn btn-danger"
                },
                cancel: {
                    label: "Cancel",
                    className: "btn btn-light"
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/comments/" + commentId,
                        method: "DELETE",
                        success: function () {
                            toastr.options = {
                                progressBar: true,
                                positionClass: "toast-bottom-full-width",
                                timeOut: 3000,
                            };
                            toastr.success("Comment successfully deleted");
                            container.fadeOut(1000, function () {
                                $(this).remove();
                            });
                        },
                        error: function () {
                            toastr.options = {
                                progressBar: true,
                                positionClass: "toast-bottom-full-width",
                                timeOut: 3000,
                            };
                            toastr.error("Couldn't delete this comment");
                        }
                    });
                }
            }
        });
    });
}