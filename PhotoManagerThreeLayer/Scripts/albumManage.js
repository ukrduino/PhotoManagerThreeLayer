$(document).ready(function ($) {

    function loadPhotosToManageAlbumView(inAlbum, pageNumb) {
        var albumId = $("#AlbumId").val();
        if (inAlbum) {
            $(".albumPhotosBlock")
                .load("/Photo/LoadPhotosToAlbumDetailView",
                    { albumId: albumId, inAlbum: inAlbum, pageNumber: pageNumb },
                    function () {
                        updatePagination(albumId, inAlbum, pageNumb);
                        $(".albumPhotosBlock")
                            .find(".glyphicon")
                            .each(function () {
                                $(this).addClass("glyphicon-minus").css("color", "red");
                            });
                        $(".albumPhotosBlock")
                            .find(".action")
                            .each(function () {
                                $(this).click(function (e) {
                                    e.preventDefault();
                                    $.ajax({
                                        type: "POST",
                                        url: '/Album/RemovePhotoFromAlbum',
                                        data: { albumId: albumId, photoId: $(this).data("id") },
                                        success: function (data) {
                                            loadPhotosToManageAlbumView(true, 1);
                                            loadPhotosToManageAlbumView(false, 1);
                                        }
                                    });
                                });
                            });
                    });

        } else {
            $(".albumAddPhotosBlock")
                .load("/Photo/LoadPhotosToAlbumDetailView",
                    { albumId: albumId, inAlbum: inAlbum, pageNumber: pageNumb },
                    function () {
                        updatePagination(albumId, inAlbum, pageNumb);
                        $(".albumAddPhotosBlock")
                            .find(".glyphicon")
                            .each(function () {
                                $(this).addClass("glyphicon-plus");
                            });
                        $(".albumAddPhotosBlock").find(".action").each(function () {
                            $(this).click(function (e) {
                                e.preventDefault();
                                $.ajax({
                                    type: "POST",
                                    url: '/Album/AddPhotoToAlbum',
                                    data: { albumId: albumId, photoId: $(this).data("id") },
                                    success: function (data) {
                                        loadPhotosToManageAlbumView(true, 1);
                                        loadPhotosToManageAlbumView(false, 1);
                                    }
                                });
                            });
                        });
                    });
        }
    }


    function attachPagination() {
        $("#AlbumPhotoBackward").click(
            function () {
                loadPhotosToManageAlbumView(true, $(this).data("page"));
            });
        $("#AlbumPhotoForward").click(
            function () {
                loadPhotosToManageAlbumView(true, $(this).data("page"));
            });
        $("#AddPhotoBackward").click(
            function () {
                loadPhotosToManageAlbumView(false, $(this).data("page"));
            });
        $("#AddAlbumPhotoForward").click(
            function () {
                loadPhotosToManageAlbumView(false, $(this).data("page"));
            });
    }

    function updatePagination(albumId, inAlbum, pageNumb) {
        $.ajax({
            type: "GET",
            url: '/Photo/GetDataForPagination',
            data: { albumId: albumId, inAlbum: inAlbum, pageNumber: pageNumb },
            success: function (data) {
                if (inAlbum) {
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
                } else {
                    if (data.AddPhotosPreviousePage > 0) {
                        $("#AddPhotoBackward").show().attr('data-page', data.AddPhotosPreviousePage);
                    } else {
                        $("#AddPhotoBackward").hide();
                    }
                    if (data.AddPhotosNextPage > -1) {
                        $("#AddAlbumPhotoForward").show().attr('data-page', data.AddPhotosNextPage);
                    } else {
                        $("#AddAlbumPhotoForward").hide();
                    }
                }
            }
        });
    }

    attachPagination();
    loadPhotosToManageAlbumView(true, 1);
    loadPhotosToManageAlbumView(false, 1);
})