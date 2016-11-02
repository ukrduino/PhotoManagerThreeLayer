using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLipsum.Core;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManager.Utils;
using PhotoManagerModels.Models;
using PhotoManagerModels.Models.Interfaces;

namespace PhotoManager.BLL.Services
{
    public class BllDbServices
    {

        public List<string> catTitles = new List<string>() { "Aqua", "Nature", "Travel", "Friends", "Animals" };
        private static string uriSampleImagesFolderPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "SampleImages");
        public List<string> fileNames = Directory.EnumerateFiles(new Uri(uriSampleImagesFolderPath).LocalPath).ToList();
        private LipsumGenerator generator = new LipsumGenerator();

        public void SetUpDb()
        {
            DalServices dalServices = new DalServices();
            dalServices.DalSetUpDb();
        }

        public void CreateCategoriesInDb(List<string> catTitles, UnitOfWork unitOfWork)
        {
            List<Category> categories = new List<Category>();
            foreach (var title in catTitles)
            {
                categories.Add(new Category(title));
            }
            unitOfWork.Categories.AddRange(categories);
            unitOfWork.Complete();
        }
        public List<Album> CreateAlbumsInDb(int quantity, UnitOfWork unitOfWork)
        {
            List<string> albumNames = new List<string>();
            for (int i = 0; i < quantity; i++)
            {
                albumNames.Add("Album_" + StringUtils.RandomAlphaNumericalStr(6));
            }
            List<Album> albums = new List<Album>();
            foreach (var albumName in albumNames)
            {
                Album album = new Album();
                album.Title = albumName;
                AddRandomCategories(album, unitOfWork);
                album.Description = generator.GenerateSentences(1)[0];
                album.CoverImageData = File.ReadAllBytes(fileNames[Math.Min(albumNames.IndexOf(albumName), fileNames.Count - 1)]);
                albums.Add(album);
            }
            unitOfWork.Albums.AddRange(albums);
            unitOfWork.Complete();
            return albums;
        }

        public List<Photo> CreatePhotosInDb(UnitOfWork unitOfWork)
        {
            List<Photo> photos = new List<Photo>();
            foreach (string file in fileNames)
            {
                Photo photo = new Photo();
                photo.Title = Path.GetFileNameWithoutExtension(file) + '_' + StringUtils.RandomAlphaNumericalStr(6);
                photo.TakenDate = DateTime.Now.AddDays(NumberUtils.RandomIntInRange(-20, -5));
                photo.OriginalSizeImageData = File.ReadAllBytes(file);
                photo.MiddleSizeImageData = File.ReadAllBytes(file);
                photo.SmallSizeImageData = File.ReadAllBytes(file);
                AddRandomCategories(photo, unitOfWork);
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

        private void AddRandomCategories(ICategorized categorizable, UnitOfWork unitOfWork)
        {
            int numberOfCategories = NumberUtils.RandomIntInRange(1, unitOfWork.Categories.GetAll().Count());
            Random r = new Random();
            var categories = unitOfWork.Categories.GetAll().OrderBy(x => r.Next()).Take(numberOfCategories);
            foreach (var category in categories)
            {
                categorizable.Categories.Add(category);
            }
        }

        private void AddPhotoToRandomAlbums(Photo photo, UnitOfWork unitOfWork)
        {
            int numberOfAlbums = NumberUtils.RandomIntInRange(1, unitOfWork.Albums.GetAll().ToList().Count);
            var albums = unitOfWork.Albums.GetAll().OrderBy(arg => Guid.NewGuid()).Take(numberOfAlbums);
            foreach (var album in albums)
            {
                photo.Albums.Add(album);
            }
        }

        public void CleanUpDb()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<AlbumComment> aCom = unitOfWork.AlbumComments.GetAll();
                if (aCom.Any())
                {
                    unitOfWork.AlbumComments.RemoveRange(aCom);
                    unitOfWork.Complete();
                }
                IEnumerable<PhotoComment> pCom = unitOfWork.PhotoComments.GetAll();
                if (pCom.Any())
                {
                    unitOfWork.PhotoComments.RemoveRange(pCom);
                    unitOfWork.Complete();
                }

                IEnumerable<Category> cat = unitOfWork.Categories.GetAll();
                if (cat.Any())
                {
                    unitOfWork.Categories.RemoveRange(cat);
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
            int numberOfAlbums = 3;
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                CreateCategoriesInDb(catTitles, unitOfWork);
                CreateAlbumsInDb(numberOfAlbums, unitOfWork);
                CreatePhotosInDb(unitOfWork);
            }
        }
    }
}
