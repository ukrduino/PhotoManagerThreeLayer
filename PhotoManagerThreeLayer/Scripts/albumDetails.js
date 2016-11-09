$(document).ready(function ($) {
    $(function () {
        //$("#removePhotos")
        //    .click(function (e) {
        //        e.preventDefault();
        //        var photosArr = $('input:checkbox.photoSelect:checked')
        //            .map(function() {
        //                return this.id;
        //            })
        //            .get();
        //        console.log(photosArr);
        //        if (photosArr.length > 0) {
        //            var albumId = $("#album").val();
        //            var dataToPost = {
        //                albumId: albumId,
        //                photosArr: photosArr
        //            }
        //            var token = $('#removeForm').serialize();
        //            var dataWithAntiforgeryToken = $.extend(dataToPost, token);
        //            console.log(dataToPost);
        //            $.ajax(
        //            {
        //                type: "POST",
        //                data: dataWithAntiforgeryToken,
        //                url: "/Album/RemovePhotosFromAlbum",
        //                dataType: "json"
        //            });
        //        }
        //    });

        $("#MyForm").submit(function (e) {
            e.preventDefault(); // prevent the form's normal submission
            console.log(111);


            var formData = $(this).serialize();
            var photosArr = $('input:checkbox.photoSelect:checked')
                .map(function () {
                    return this.id;
                })
                .get();
            console.log(photosArr);
            if (photosArr.length > 0) {
                var albumId = $("#album").val();
                var dataToPost = {
                    albumId: albumId,
                    photosArr: photosArr
                }
                $.extend(formData, dataToPost);
                $.ajax(
                {
                    type: "POST",
                    data: formData,
                    url: "/Album/RemovePhotosFromAlbum",
                });
            }
        });
    });
})