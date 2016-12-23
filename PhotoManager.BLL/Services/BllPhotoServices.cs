using System.Collections.Generic;
using System.Configuration;
using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public class BllPhotoServices
    {
        public List<PhotoDTO> GetAllPhotos()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Photo> photos = unitOfWork.Photos.GetPhotosByUser(WebSecurityService.GetCurrentUser());
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
                Photo photo = unitOfWork.Photos.GetPhotoById(id);
                return Mapper.Map<PhotoDTO>(photo);
            }
        }

        public void CreatePhoto(PhotoDTO photoDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {

                Photo photo = Mapper.Map<Photo>(photoDto);
                photo.UserId = WebSecurityService.GetCurrentUser().UserId;
                unitOfWork.Photos.Add(photo);
                unitOfWork.Complete();
            }
        }

        public void UpdatePhoto(PhotoDTO photoDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Photo photo = Mapper.Map<Photo>(photoDto);
                unitOfWork.Photos.UpdatePhoto(photo);
                unitOfWork.Complete();
            }
        }

        public static int GetPhotosNumberForCurrentUser()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Photos.GetPhotosByUser(WebSecurityService.GetCurrentUser()).Count;
            }
        }

        public int GetPhotosNumberForCurrentUserAndAlbum(int albumId, bool inAlbum)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Photos.GetPhotosByUserAndAlbum(WebSecurityService.GetCurrentUser(), unitOfWork.Albums.Get(albumId), inAlbum).Count;
            }
        }

        public List<PhotoDTO> SearchPhotos(string photoSearchText)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<Photo> photos = unitOfWork.Photos.SearchPhotos(photoSearchText, WebSecurityService.GetCurrentUser().UserId);
                return Mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            }
        }

        public List<PhotoDTO> GetPhotosForCurrentUserAndAlbumWithPagination(int albumId, bool inAlbum, int pageNumber)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int pageSize = int.Parse(ConfigurationManager.AppSettings["AlbumDetailsPhotosPageSize"]);
                int skip = (pageNumber - 1)*pageSize;
                List<Photo> photos = unitOfWork.Photos.GetPhotosByUserAndAlbumWithPagination(
                    WebSecurityService.GetCurrentUser(),
                    unitOfWork.Albums.Get(albumId),
                    inAlbum,
                    skip,
                    pageSize);
                return Mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            }
        }
    }
}
