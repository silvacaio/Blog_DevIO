function initializePartialForm(newCommentAction) {
    $("#form-new-comment").submit(function (event) {
        console.log('oi');
        event.preventDefault();

        $.ajax({
            url: newCommentAction,
            type: "POST",
            data: $(this).serialize(),
            success: function (response) {

                const div = $('<div>').html(response);
                if (div.find('#list-comments').length) {
                    $('#Content').val('');
                    $('#list-comments').html(response);
                } else {
                    $(this).html(response);
                }
            }
        });
    });
}