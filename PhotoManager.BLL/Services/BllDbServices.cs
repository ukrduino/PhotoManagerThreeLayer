using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using NLipsum.Core;
using PhotoManager.BLL.Utils;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;
using PhotoManager.Utils;
using WebMatrix.WebData;


namespace PhotoManager.BLL.Services
{
    public class BllDbServices
    {

        private static readonly string UriSampleImagesFolderPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "SampleImages");
        private readonly List<string> FileNames = Directory.EnumerateFiles(new Uri(UriSampleImagesFolderPath).LocalPath).ToList();
        private readonly LipsumGenerator  _generator = new LipsumGenerator();
        private readonly BLLImageServices _imageServices = new BLLImageServices();

        public void SetUpDb()
        {
            DalServices dalServices = new DalServices();
            dalServices.DalSetUpDb();
        }

        private void CreateAlbumsInDb(UnitOfWork unitOfWork, int numberOfAlbums)
        {
            List<User> users = unitOfWork.Users.GetAll().ToList();
            List<Album> albums = new List<Album>();
            foreach (var user in users)
            {
                for (int i = 0; i < numberOfAlbums; i++)
                {
                    Album album = new Album();
                    album.Title = "Album_" + StringUtils.RandomAlphaNumericalStr(6);
                    album.UserId = user.UserId;
                    album.Description = _generator.GenerateSentences(1)[0];
                    albums.Add(album);
                }
            }
            unitOfWork.Albums.AddRange(albums);
            unitOfWork.Complete();
        }

        private void CreatePhotosInDb(UnitOfWork unitOfWork)
        {
            List<User> users = unitOfWork.Users.GetAll().ToList();
            List<Photo> photos = new List<Photo>();
            foreach (var user in users)
            {
                foreach (string file in FileNames)
                {
                    Photo photo = new Photo();
                    photo.Title = Path.GetFileNameWithoutExtension(file) + '_' + StringUtils.RandomAlphaNumericalStr(6);
                    photo.UserId = user.UserId;
                    photo.TakenDate = DateTime.Now.AddDays(NumberUtils.RandomIntInRange(-20, -5));
                    using (FileStream fileStream = new FileStream(file, FileMode.Open))
                    {
                        photo.ImageId = _imageServices.SaveImageToDb(fileStream, Enums.ImageSize.Actual);
                        photo.SmallImageId = _imageServices.SaveImageToDb(fileStream, Enums.ImageSize.Small);
                        photo.MiddleImageId = _imageServices.SaveImageToDb(fileStream, Enums.ImageSize.Middle);
                    }
                    AddPhotoToRandomAlbums(photo, unitOfWork, user);
                    photo.Description = _generator.GenerateSentences(1)[0];
                    photo.Place = _generator.GenerateSentences(1)[0];
                    photo.Camera = "CAM_" + StringUtils.RandomAlphaNumericalStr(5);
                    photo.FocalLength = NumberUtils.RandomIntInRange(18, 56) + " mm";
                    photo.Aperture = "1/" + NumberUtils.RandomIntInRange(2, 16);
                    photo.CameraLockSpeed = "1/" + NumberUtils.RandomIntInRange(10, 1000);
                    photo.ISO = "200";
                    photo.UsedFlash = StringUtils.RandomBool();
                    photo.Views = NumberUtils.RandomIntInRange(0, 50);
                    photo.AnyOneCanSee = StringUtils.RandomBool();
                    photos.Add(photo);
                    photo.Created = DateTime.Now;
                }
            }
            unitOfWork.Photos.AddRange(photos);
            unitOfWork.Complete();
        }

        private void AddPhotoToRandomAlbums(Photo photo, UnitOfWork unitOfWork, User user)
        {
            int numberOfAlbums = NumberUtils.RandomIntInRange(1, unitOfWork.Albums.GetAlbumsByUser(user).ToList().Count);
            var albums = unitOfWork.Albums.GetAlbumsByUser(user).OrderBy(arg => Guid.NewGuid()).Take(numberOfAlbums);
            foreach (var album in albums)
            {
                photo.Albums.Add(album);
            }
        }


        public void SeedDb()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (membership.GetUser("Bob", false) == null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Discriminator"] = "PayedUser";
                membership.CreateUserAndAccount("Bob", "test", false, parameters);
            }
            if (membership.GetUser("Joe", false) == null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["Discriminator"] = "FreeUser";
                membership.CreateUserAndAccount("Joe", "test", false, parameters);
            }

            int numberOfAlbums = 3;
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                if (!unitOfWork.Albums.GetAll().Any())
                {
                    CreateAlbumsInDb(unitOfWork, numberOfAlbums);
                    CreatePhotosInDb(unitOfWork);
                }
            }
        }
    }
}
