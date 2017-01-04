using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                IEnumerable<Photo> photos = unitOfWork.Photos.GetPhotosByUserId(WebSecurityService.GetCurrentUserId());
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
                photo.UserId = WebSecurityService.GetCurrentUserId();
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
                return unitOfWork.Photos.GetPhotosByUserId(WebSecurityService.GetCurrentUserId()).Count;
            }
        }

        public int GetPhotosNumberForAlbum(int albumId, bool inAlbum)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                bool excludePrivate = true;
                int userId = unitOfWork.Albums.Get(albumId).Id;
                if (WebSecurityService.IsAuthenticated)
                {
                    excludePrivate = WebSecurityService.GetCurrentUserId() != userId;
                }
                return unitOfWork.Photos.GetPhotosByUserAndAlbum(userId, albumId, inAlbum, excludePrivate).Count;
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

        public List<PhotoDTO> SearchPhotosExtended(
            string title,
            string takenDate,
            string place,
            string camera,
            string focalLength,
            string aperture,
            string cameraLockSpeed,
            string iso,
            string usedFlash
            )
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                DateTime td = DateTime.MinValue;
                if (!string.IsNullOrEmpty(takenDate))
                {
                    //TODO not working
                    DateTime.TryParse(takenDate, out td);
                }
                IEnumerable<Photo> photos = unitOfWork.Photos.SearchPhotosExtended(
                    title,
                    td,
                    place,
                    camera,
                    focalLength,
                    aperture,
                    cameraLockSpeed,
                    iso,
                    usedFlash,
                    WebSecurityService.GetCurrentUser().UserId);
                return Mapper.Map<List<Photo>, List<PhotoDTO>>(photos.ToList());
            }
        }

        public List<PhotoDTO> GetPhotosForCurrentUserAndAlbumWithPagination(int albumId, bool inAlbum, int pageNumber)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                int pageSize = int.Parse(ConfigurationManager.AppSettings["AlbumDetailsPhotosPageSize"]);
                int skip = (pageNumber - 1)*pageSize;
                Album album = unitOfWork.Albums.Get(albumId);
                List<Photo> photos = unitOfWork.Photos.GetPhotosByUserAndAlbumWithPagination(
                    album.UserId,
                    albumId,
                    inAlbum,
                    skip,
                    pageSize);
                return Mapper.Map<List<Photo>, List<PhotoDTO>>(photos);
            }
        }
    }
}
