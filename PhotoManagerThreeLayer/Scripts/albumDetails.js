$(document).ready(function ($) {
    $(function () {
        $("#MyForm").submit(function (e) {
            e.preventDefault(); // prevent the form's normal submission
            if ($("#PhotosArr").val().length > 0) {
                var formData = $(this).serialize();
                console.log(formData);
                $.ajax(
                {
                    type: "POST",
                    data: formData,
                    url: "/Album/RemovePhotosFromAlbum",
                    success: function () {
                        location.reload();
                    }
                });
            }
        });
    });
    $(function () {
        $("input:checkbox.photoSelect").click(function (e) {
            var photosArr = $('input:checkbox.photoSelect:checked')
                .map(function () {
                    return this.id;
                })
                .get();
            if (photosArr.length > 0) {
                $("#PhotosArr").val(photosArr.join());
            }
        });
    });
})