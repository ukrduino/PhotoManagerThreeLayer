using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PhotoManager.BLL.Services;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.Models;

namespace UnitTestProject1
{
    [TestFixture]
    public class DalRepositoriesTests
    {
        private BllDbServices bllDbServices = new BllDbServices();

        [OneTimeSetUp]
        public void InitEnvForTesting()
        {
            bllDbServices.SetUpDb();
        }

        [SetUp]
        public void CleanUpDb()
        {
            bllDbServices.CleanUpDb();
        }

        [Test]
        public void CreateCategoriesInDbTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
            bllDbServices.CreateCategoriesInDb(bllDbServices.catTitles, unitOfWork);
                foreach (var title in bllDbServices.catTitles)
                {
                    Assert.True(unitOfWork.Categories.GetAll().Any(cat => cat.Title.Equals(title)));
                }
            }
        }

        [Test]
        public void CreateAlbumsInDbTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int numberOfAlbums = 3;
                bllDbServices.CreateCategoriesInDb(bllDbServices.catTitles, unitOfWork);
                bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                List<Album> albums = unitOfWork.Albums.GetAll().ToList();
                Assert.AreEqual(numberOfAlbums, albums.Count);
                foreach (Album alb in albums)
                {
                    List<Category> categories = unitOfWork.Categories.GetCategoriesByAlbum(alb.Id);
                    Assert.True(categories.Any());
                }
            }
        }
        [Test]
        public void CreatePhotosInDbTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int numberOfAlbums = 3;
                bllDbServices.CreateCategoriesInDb(bllDbServices.catTitles, unitOfWork);
                bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                bllDbServices.CreatePhotosInDb(unitOfWork);

                List<Photo> photosAll = unitOfWork.Photos.GetAll().ToList();
                List<Album> albomsAll = unitOfWork.Albums.GetAll().ToList();
                List<Category> categoriesAll = unitOfWork.Categories.GetAll().ToList();
                Assert.AreEqual(bllDbServices.fileNames.Count, photosAll.Count);
                foreach (Photo photo in photosAll)
                {
                    List<Category> categories = unitOfWork.Categories.GetCategoriesByPhoto(photo.Id);
                    Assert.True(categories.Any());
                    List<Album> albums = unitOfWork.Albums.GetAlbumsByPhoto(photo.Id);
                    Assert.True(albums.Any());
                }
                foreach (Album album in albomsAll)
                {
                    List<Category> categories = unitOfWork.Categories.GetCategoriesByAlbum(album.Id);
                    Assert.True(categories.Any());
                    List<Photo> photo = unitOfWork.Photos.GetPhotosByAlbum(album.Id);
                    Assert.True(photo.Any());
                }
                foreach (Category cat in categoriesAll)
                {
                    List<Album> albums = unitOfWork.Albums.GetAlbumsByCategory(cat.Id);
                    Assert.True(albums.Any());
                    List<Photo> photo = unitOfWork.Photos.GetPhotosByCategory(cat.Id);
                    Assert.True(photo.Any());
                }
            }
        }
    }
}
