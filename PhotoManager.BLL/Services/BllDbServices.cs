using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Security;
using NLipsum.Core;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;
using PhotoManager.Utils;
using WebMatrix.WebData;


namespace PhotoManager.BLL.Services
{
    public class BllDbServices
    {

        private static string uriSampleImagesFolderPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "SampleImages");
        public List<string> fileNames = Directory.EnumerateFiles(new Uri(uriSampleImagesFolderPath).LocalPath).ToList();
        private LipsumGenerator generator = new LipsumGenerator();

        public void SetUpDb()
        {
            DalServices dalServices = new DalServices();
            dalServices.DalSetUpDb();
        }

        //public void CreateAlbumsInDb(UnitOfWork unitOfWork, int numberOfAlbums)
        //{
        //    List<User> users = unitOfWork.Users.GetAll().ToList();
        //    foreach (var user in users)
        //    {
        //        List<string> albumNames = new List<string>();
        //        for (int i = 0; i < numberOfAlbums; i++)
        //        {
        //            albumNames.Add("Album_" + StringUtils.RandomAlphaNumericalStr(6));
        //        }
        //        foreach (var albumName in albumNames)
        //        {
        //            Album album = new Album();
        //            album.Title = albumName;
        //            album.User = user;
        //            album.Description = generator.GenerateSentences(1)[0];
        //            album.Image =  

        //            File.ReadAllBytes(fileNames[Math.Min(albumNames.IndexOf(albumName), fileNames.Count - 1)]);
        //            List<Album> albums = new List<Album>();
        //            albums.Add(album);
        //            unitOfWork.Albums.AddRange(albums);
        //            unitOfWork.Complete();
        //        }
        //    }
        //}

        public List<Photo> CreatePhotosInDb(UnitOfWork unitOfWork)
        {
            List<User> users = unitOfWork.Users.GetAll().ToList();
            List<Photo> photos = new List<Photo>();
            foreach (string file in fileNames)
            {
                Photo photo = new Photo();
                photo.Title = Path.GetFileNameWithoutExtension(file) + '_' + StringUtils.RandomAlphaNumericalStr(6);
                photo.User = users[NumberUtils.RandomIntInRange(1, users.Count)];
                photo.TakenDate = DateTime.Now.AddDays(NumberUtils.RandomIntInRange(-20, -5));
                photo.OriginalSizeImageData = File.ReadAllBytes(file);
                photo.MiddleSizeImageData = File.ReadAllBytes(file);
                photo.SmallSizeImageData = File.ReadAllBytes(file);
                AddPhotoToRandomAlbums(photo, unitOfWork);
                photo.Description = generator.GenerateSentences(1)[0];
                photo.Place = generator.GenerateSentences(1)[0];
                photo.Camera = "CAM_" + StringUtils.RandomAlphaNumericalStr(5);
                photo.FocalLength = NumberUtils.RandomIntInRange(18, 56) + " mm";
                photo.Aperture = "1/" + NumberUtils.RandomIntInRange(2, 16);
                photo.CameraLockSpeed = "1/" + NumberUtils.RandomIntInRange(10, 1000);
                photo.ISO = "200";
                photo.UsedFlash = StringUtils.RandomBool();
                photo.Views = NumberUtils.RandomIntInRange(0, 50);
                photo.AnyOneCanSee = StringUtils.RandomBool();
                photos.Add(photo);
            }
            unitOfWork.Photos.AddRange(photos);
            unitOfWork.Complete();
            return photos;
        }

        private void AddPhotoToRandomAlbums(Photo photo, UnitOfWork unitOfWork)
        {
            int numberOfAlbums = NumberUtils.RandomIntInRange(1, unitOfWork.Albums.GetAll().ToList().Count);
            var albums = unitOfWork.Albums.GetAlbumsByUser(photo.User).OrderBy(arg => Guid.NewGuid()).Take(numberOfAlbums);
            foreach (var album in albums)
            {
                photo.Albums.Add(album);
            }
        }

        public void CleanUpDb()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<PhotoComment> pCom = unitOfWork.PhotoComments.GetAll();
                if (pCom.Any())
                {
                    unitOfWork.PhotoComments.RemoveRange(pCom);
                    unitOfWork.Complete();
                }

                IEnumerable<Photo> pho = unitOfWork.Photos.GetAll();
                if (pho.Any())
                {
                    unitOfWork.Photos.RemoveRange(pho);
                    unitOfWork.Complete();
                }

                IEnumerable<Album> alb = unitOfWork.Albums.GetAll();
                if (alb.Any())
                {
                    unitOfWork.Albums.RemoveRange(alb);
                    unitOfWork.Complete();
                }
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

            //int numberOfAlbums = 3;
            //using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            //{
            //    CreateAlbumsInDb(unitOfWork, numberOfAlbums);
            //    CreatePhotosInDb(unitOfWork);
            //}
        }
    }
}
