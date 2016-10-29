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
        public void SetUpDb()
        {
            DalServices dalServices = new DalServices();
            dalServices.DalSetUpDb();
        }

        public void SeedDb()
        {
            LipsumGenerator generator = new LipsumGenerator();
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<Category> categories = new List<Category>();
                categories.Add(new Category("Aqua"));
                categories.Add(new Category("Nature"));
                categories.Add(new Category("Travel"));
                categories.Add(new Category("Friends"));
                categories.Add(new Category("Animals"));
                unitOfWork.Categories.AddRange(categories);
                unitOfWork.Complete();

                string uriSampleImagesFolderPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "SampleImages");
                List<string> fileNames = Directory.EnumerateFiles(new Uri(uriSampleImagesFolderPath).LocalPath).ToList();

                for (int i = 0; i < 3; i++)
                {
                    Album album = new Album();
                    album.Title = "Album_" + StringUtils.RandomAlphaNumericalStr(6);
                    AddRandomCategories(album, unitOfWork);
                    album.Description = generator.GenerateSentences(1)[0];
                    album.CoverImageData = File.ReadAllBytes(fileNames[Math.Min(i, fileNames.Count - 1)]);
                    unitOfWork.Albums.Add(album);
                }
                unitOfWork.Complete();


                List<Photo> photos = new List<Photo>();
                foreach (string file in fileNames)
                {
                    Photo photo = new Photo();
                    photo.Title = Path.GetFileNameWithoutExtension(file) + '_' + StringUtils.RandomAlphaNumericalStr(6);
                    photo.TakenDate = DateTime.Now.AddDays(NumberUtils.RandomIntInRange(-20, -5));
                    photo.OriginalSizeImageData = File.ReadAllBytes(file);
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
                    photos.Add(photo);
                    unitOfWork.Photos.Add(photo);
                }
                unitOfWork.Complete();
            }
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
            int numberOfAlbums = NumberUtils.RandomIntInRange(1, unitOfWork.Albums.GetAll().Count());
            var albums = unitOfWork.Albums.GetAll().OrderBy(arg => Guid.NewGuid()).Take(numberOfAlbums);
            foreach (var album in albums)
            {
                photo.Albums.Add(album);
            }
        }
    }
}
