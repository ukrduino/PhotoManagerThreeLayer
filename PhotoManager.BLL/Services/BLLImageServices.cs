using System.IO;
using PhotoManager.BLL.Utils;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public class BLLImageServices
    {
        public int SaveImageToDb(Stream imageDataStream , Enums.ImageSize size)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Image newImage = new Image();
                newImage.ImageData = ImageUtils.ResizeImage(imageDataStream, size);
                unitOfWork.Images.Add(newImage);
                unitOfWork.Complete();
                return newImage.Id;
            }
        }

        public byte[] GetImageBytesFromDb(int imageId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Images.Get(imageId).ImageData;
            }
        }
    }
}
