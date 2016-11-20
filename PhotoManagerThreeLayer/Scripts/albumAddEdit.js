$(document).ready(function ($) {
    $(function() {
        $('#my-file-selector')
            .change(function() {
                console.log(111);
                $('#edit-upload-file-info').html($(this).val());
                $('.field-validation-error').html("");
            });
    });
})