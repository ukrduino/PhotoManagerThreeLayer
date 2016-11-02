using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManager.BLL.Services
{
    public class BllAlbumServices
    {
        public List<AlbumDTO> GetAllAlbums()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Album> albums = unitOfWork.Albums.GetAll();
                return Mapper.Map<IEnumerable<Album>, List<AlbumDTO>>(albums);
            }
        }
        public AlbumDTO GetAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = unitOfWork.Albums.Get(id);
                return Mapper.Map<AlbumDTO>(album);
            }
        }
    }
}
