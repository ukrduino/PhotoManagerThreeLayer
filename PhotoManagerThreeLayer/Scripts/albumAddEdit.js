$(document).ready(function ($) {
    $(function() {
        $('#my-file-selector')
            .change(function() {
                console.log($(this).val());
                $('#edit-upload-file-info').html($(this).val());
                $('.field-validation-error').html("");
            });
    });
})