using System;
using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using PhotoManager.BLL.Services;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;
using PhotoManagerModels.ViewModels;

namespace UnitTestProject1
{
    [TestFixture]
    public class BllServicesTests
    {
        private BllAlbumServices _albumServices = new BllAlbumServices();
        private BllPhotoServices _photoServices = new BllPhotoServices();
        private BllCategoryServices _categoryServices = new BllCategoryServices();
        private BllCommentServices _commentServices = new BllCommentServices();
        private BllDbServices bllDbServices = new BllDbServices();

        [OneTimeSetUp]
        public void InitEnvForTesting()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperConf>();
            });
            bllDbServices.SetUpDb();
        }

        [SetUp]
        public void CleanUpDb()
        {
            bllDbServices.CleanUpDb();
        }

        [Test]
        public void MapConfigurationTest()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void AlbumToAlbumDtoToAlbumListViewModelTest()
        {
            List<Album> albums;
            int numberOfAlbums = 3;
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                bllDbServices.CreateCategoriesInDb(bllDbServices.catTitles, unitOfWork);
                albums = bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                bllDbServices.CreatePhotosInDb(unitOfWork);
            }
            foreach (Album album in albums)
            {
                AlbumDTO albumDto = _albumServices.GetAlbum(album.Id);
                AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
                Assert.AreEqual(album.Id, albumDetailViewModel.Id);
                Assert.AreEqual(albumDto.Id, albumDetailViewModel.Id);
                Assert.AreEqual(album.Title, albumDetailViewModel.Title);
                Assert.AreEqual(albumDto.Title, albumDetailViewModel.Title);
                Assert.AreEqual(album.CoverImageData, albumDetailViewModel.CoverImageData);
                Assert.AreEqual(albumDto.CoverImageData, albumDetailViewModel.CoverImageData);
                Assert.AreEqual(album.Description, albumDto.Description);
                Assert.That(album.CreatedDate, Is.EqualTo(albumDto.CreatedDate).Within(TimeSpan.FromSeconds(1)));
                Assert.That(album.LastModified, Is.EqualTo(albumDto.LastModified).Within(TimeSpan.FromSeconds(1)));
            }
        }
        [Test]
        public void PhotoTPhotoDtoToPhotoListViewModelTest()
        {
            List<Photo> photos;
            int numberOfAlbums = 3;
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                bllDbServices.CreateCategoriesInDb(bllDbServices.catTitles, unitOfWork);
                bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                photos = bllDbServices.CreatePhotosInDb(unitOfWork);
            }
            foreach (Photo photo in photos)
            {
                PhotoDTO photoDto = _photoServices.GetPhoto(photo.Id);
                PhotoListViewModel photoListViewModel = Mapper.Map<PhotoListViewModel>(photoDto);
                Assert.AreEqual(photo.Id, photoDto.Id);
                Assert.AreEqual(photoDto.Id, photoListViewModel.Id);
                Assert.AreEqual(photo.Title, photoDto.Title);
                Assert.AreEqual(photoDto.Title, photoListViewModel.Title);
                Assert.That(photo.TakenDate, Is.EqualTo(photoDto.TakenDate).Within(TimeSpan.FromSeconds(1)));
                Assert.AreEqual(photo.Description, photoDto.Description);
                Assert.AreEqual(photoDto.Description, photoListViewModel.Description);
                Assert.AreEqual(photo.Place, photoDto.Place);
                Assert.AreEqual(photo.Camera, photoDto.Camera);
                Assert.AreEqual(photo.FocalLength, photoDto.FocalLength);
                Assert.AreEqual(photo.FocalLength, photoDto.FocalLength);
                Assert.AreEqual(photo.Aperture, photoDto.Aperture);
                Assert.AreEqual(photo.CameraLockSpeed, photoDto.CameraLockSpeed);
                Assert.AreEqual(photo.ISO, photoDto.ISO);
                Assert.AreEqual(photo.UsedFlash, photoDto.UsedFlash);
                Assert.AreEqual(photo.OriginalSizeImageData, photoDto.OriginalSizeImageData);
                Assert.AreEqual(photo.MiddleSizeImageData, photoDto.MiddleSizeImageData);
                Assert.AreEqual(photo.SmallSizeImageData, photoDto.SmallSizeImageData);
                Assert.AreEqual(photoDto.SmallSizeImageData, photoListViewModel.SmallSizeImageData);
                Assert.That(photo.LastModified, Is.EqualTo(photoDto.LastModified).Within(TimeSpan.FromSeconds(1)));
                Assert.That(photo.Uploaded, Is.EqualTo(photoDto.Uploaded).Within(TimeSpan.FromSeconds(1)));
                Assert.That(photoDto.Uploaded, Is.EqualTo(photoListViewModel.Uploaded).Within(TimeSpan.FromSeconds(1)));
                Assert.AreEqual(photo.AnyOneCanSee, photoDto.AnyOneCanSee);
                Assert.AreEqual(photoDto.AnyOneCanSee, photoListViewModel.anyOneCanSee);
            }
        }

        //[Test]
        //public void AlbumToAlbumDtoToAlbumDetailViewModelTest()
        //{
        //    List<Album> albums;
        //    List<Photo> photos;
        //    List<PhotoDTO> photoDtos;
        //    int numberOfAlbums = 3;
        //    using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
        //    {
        //        CreateCategoriesInDb(catTitles, unitOfWork);
        //        albums = CreateAlbumsInDb(numberOfAlbums, unitOfWork);
        //        photos = CreatePhotosInDb(unitOfWork);
        //    }
        //    photoDtos = Mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
        //    foreach (Photo photo in photos )
        //    {
        //        PhotoDTO photoDto = _photoServices.GetPhoto(photo.Id);
        //        AlbumDetailViewModel albumDetailViewModel = Mapper.Map<AlbumDetailViewModel>(albumDto);
        //        List<PhotoDTO> photoDtoList = _photoServices.GetPhotosByAlbum(album.Id);
        //        List<CategoryDTO> categoriesDtoList = _categoryServices.GetCategoriesByAlbum(album.Id);
        //        albumDetailViewModel.Photos = Mapper.Map<List<PhotoDTO>, List<PhotoListViewModel>>(photoDtoList);
        //        albumDetailViewModel.Categories = Mapper.Map<List<CategoryDTO>, List<CategoryViewModel>>(categoriesDtoList);
        //    }
        //}
    }
}
