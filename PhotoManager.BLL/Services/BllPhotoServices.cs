using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManager.BLL.Services
{
    public class BllPhotoServices
    {
        public IEnumerable<PhotoDTO> GetAllPhotos()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Photo> photos = unitOfWork.Photos.GetAll();
                return Mapper.Map<IEnumerable<PhotoDTO>>(photos);
            }
        }
    }
}
