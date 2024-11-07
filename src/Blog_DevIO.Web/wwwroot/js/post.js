$(document).ready(function () {
    function registerNewComment() {
        $("#form-new-comment").submit(function (event) {
            event.preventDefault();

            $.ajax({
                url: '/comments/create/',
                type: "POST",
                data: $(this).serialize(),
                success: function (response) {

                    const div = $('<div>').html(response);
                    if (div.find('#list-comments').length) {
                        $('#Content').val('');
                        $('#list-comments').html(response);
                        registerDeleteComment();
                        registerEditModalComment();                      
                    } else {
                        $(this).html(response);
                    }
                }
            });
        });
    }
    function registerDeleteComment() {
        const deleteModal = $('#deleteModal');
        deleteModal.on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            var commentId = button.data('itemid'); // Extract item ID from data-* attribute
            var postId = button.data('itemid'); // Extract item ID from data-* attribute

            $('#confirmDeleteBtn').off('click').on('click', function (event) {
                event.preventDefault();

                const commentId = button.data('id');
                const postId = button.data('postid');

                $.ajax({
                    url: '/comments/delete/' + commentId + "/" + postId,
                    type: "POST",
                    success: function (response) {

                        const div = $('<div>').html(response);
                        if (div.find('#list-comments').length) {
                            $('#list-comments').html(response);
                            registerDeleteComment();
                            registerEditModalComment();                           
                            deleteModal.modal('hide');
                        } else {
                            deleteModal.find('.modal-body').html(response);
                        }
                    }
                });
            });
        });
    }
    function registerEditModalComment() {
        const editModal = $('#editModal');
        editModal.on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            const commentId = button.data('id');
            const postId = button.data('postid');
            var modal = $(this);

            // Load the partial view into the modal
            $.ajax({
                url: '/comments/edit/' + commentId + "/" + postId,
                type: 'GET',
                success: function (data) {
                    modal.find('#modalContent').html(data);
                    registerEditComment(commentId, postId, editModal);
                },
                error: function () {
                    modal.find('#modalContent').html('<p>An error occurred while loading the data.</p>');
                }
            });
        });
    }


    registerDeleteComment();
    registerEditModalComment();
    registerNewComment(); 

    function registerEditComment(commentId, postId, editModal) {
        $("#form-edit-comment").submit(function (event) {
            event.preventDefault();

            $.ajax({
                url: '/comments/edit' + "/" + commentId + "/" + postId,
                type: "POST",
                data: $(this).serialize(),
                success: function (response) {

                    const div = $('<div>').html(response);
                    if (div.find('#list-comments').length) {
                        $('#Content').val('');
                        $('#list-comments').html(response);
                        editModal.modal('hide');
                        registerDeleteComment();
                        registerEditModalComment();                        
                    } else {
                        $(this).html(response);
                    }
                }
            });
        });
    }

    


})