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
        public void CreateAlbumsInDbTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int numberOfAlbums = 3;
                bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                List<Album> albums = unitOfWork.Albums.GetAll().ToList();
                Assert.AreEqual(numberOfAlbums, albums.Count);
            }
        }
        [Test]
        public void CreatePhotosInDbTest()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int numberOfAlbums = 3;
                bllDbServices.CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                bllDbServices.CreatePhotosInDb(unitOfWork);

                List<Photo> photosAll = unitOfWork.Photos.GetAll().ToList();
                List<Album> albomsAll = unitOfWork.Albums.GetAll().ToList();
                Assert.AreEqual(bllDbServices.fileNames.Count, photosAll.Count);
                foreach (Photo photo in photosAll)
                {
                    List<Album> albums = unitOfWork.Albums.GetAlbumsByPhoto(photo.Id);
                    Assert.True(albums.Any());
                }
                foreach (Album album in albomsAll)
                {
                    List<Photo> photo = unitOfWork.Photos.GetPhotosByAlbum(album.Id);
                    Assert.True(photo.Any());
                }
            }
        }
    }
}
