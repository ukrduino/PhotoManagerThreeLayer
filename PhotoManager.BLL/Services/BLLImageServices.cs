using System;
using System.IO;
using PhotoManager.BLL.Utils;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public class BLLImageServices
    {
        public Guid SaveImageToDb(Stream imageDataStream , Enums.ImageSize size)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Image newImage = new Image();
                if (size.Equals(Enums.ImageSize.Actual))
                {
                    newImage.ImageData = imageDataStream.ReadAllBytes();
                }
                else
                {
                    newImage.ImageData = ImageUtils.ResizeImage(imageDataStream, size);
                }
                unitOfWork.Images.Add(newImage);
                unitOfWork.Complete();
                return newImage.Id;
            }
        }

        public byte[] GetImageBytesFromDb(Guid imageId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Images.Get(imageId).ImageData;
            }
        }
    }
}
