$(document).ready(function ($) {

    function loadPhotosToAlbumDetailView(inAlbum, pageNumb) {
        var albumId = $("#AlbumId").val();
        $(".albumPhotosBlock")
            .load("/Photo/LoadPhotosToAlbumDetailView",
                { albumId: albumId, inAlbum: inAlbum, pageNumber: pageNumb },
                function () {
                    updatePagination(albumId, inAlbum, pageNumb);
                    $(".albumPhotosBlock")
                        .find(".glyphicon")
                        .each(function () {
                            $(this).addClass("glyphicon-eye-open");
                        });
                    $(".albumPhotosBlock")
                        .find(".action")
                        .each(function () {
                            $(this).attr('href', '/Photo/Details/' + $(this).data("id"));

                        });
                    $(".albumPhotosBlock")
                        .find(".smallImage")
                        .each(function () {
                            $(this).click(function (e) {
                                e.preventDefault();
                                loadMiddleImage($(this).data("id"));
                            });
                        });
                    var firstPhoto = $(".photoListItem img")[0];
                    loadMiddleImage($(firstPhoto).data("id"));
                });
    };

    function loadMiddleImage(id) {
        var image = $('<img class="middleImage"></img>');
        image.attr('src', '/Photo/GetMiddleImage/' + id);
        $('#middleImage').empty().append(image);
    }


    function attachPagination() {
        $("#AlbumPhotoBackward").click(
            function () {
                loadPhotosToAlbumDetailView(true, $(this).data("page"));
            });
        $("#AlbumPhotoForward").click(
            function () {
                loadPhotosToAlbumDetailView(true, $(this).data("page"));
            });
    }

    function updatePagination(albumId, inAlbum, pageNumb) {
        $.ajax({
            type: "GET",
            url: '/Photo/GetDataForPagination',
            data: { albumId: albumId, inAlbum: inAlbum, pageNumber: pageNumb },
            success: function (data) {
                if (data.AlbumPhotosPreviousePage > 0) {
                    $("#AlbumPhotoBackward").show().attr('data-page', data.AlbumPhotosPreviousePage);

                } else {
                    $("#AlbumPhotoBackward").hide();
                }
                if (data.AlbumPhotosNextPage > -1) {
                    $("#AlbumPhotoForward").show().attr('data-page', data.AlbumPhotosNextPage);

                } else {
                    $("#AlbumPhotoForward").hide();
                }
            }
        });
    }

    attachPagination();
    loadPhotosToAlbumDetailView(true, 1);
});