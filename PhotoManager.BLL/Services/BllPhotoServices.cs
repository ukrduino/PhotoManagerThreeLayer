using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManager.BLL.Services
{
    public class BllPhotoServices
    {
        public List<PhotoDTO> GetAllPhotos()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Photo> photos = unitOfWork.Photos.GetAll();
                return Mapper.Map<IEnumerable<Photo>, List<PhotoDTO>>(photos);
            }
        }
        public List<PhotoDTO> GetPhotosByAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<Photo> photos = unitOfWork.Photos.GetPhotosByAlbum(id);
                return Mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            }
        }
        public PhotoDTO GetPhoto(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Photo photo = unitOfWork.Photos.Get(id);
                return Mapper.Map<PhotoDTO>(photo);
            }
        }
    }
}
