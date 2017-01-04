using System.Collections.Generic;
using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.BLL.Utils;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;
using UnidecodeSharpFork;

namespace PhotoManager.BLL.Services
{
    public class BllAlbumServices
    {
        public List<AlbumDTO> GetAllAlbums()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Album> albums = unitOfWork.Albums.GetAlbumsByUser(WebSecurityService.GetCurrentUser());
                foreach (Album album in albums)
                {
                    UpdateAlbumCover(unitOfWork, album);
                }
                return Mapper.Map<IEnumerable<Album>, List<AlbumDTO>>(albums);
            }
        }

        private void UpdateAlbumCover(UnitOfWork unitOfWork, Album album)
        {
            List<Photo> photos = unitOfWork.Photos.GetPhotosByAlbum(album.Id);
            if (album.ImageId == null && photos.Count > 0)
            {
                album.ImageId = photos[0].SmallImageId;
                unitOfWork.Albums.UpdateAlbum(album);
                unitOfWork.Complete();
                return;
            }
            if (album.ImageId != null && photos.Count == 0)
            {
                album.ImageId = null;
                unitOfWork.Albums.UpdateAlbum(album);
                unitOfWork.Complete();
                return;
            }
            if (album.ImageId != null && photos.Count > 0 && !photos[0].Id.Equals(album.ImageId))
            {
                album.ImageId = photos[0].SmallImageId;
                unitOfWork.Albums.UpdateAlbum(album);
                unitOfWork.Complete();
            }
        }

        public AlbumDTO GetAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = unitOfWork.Albums.GetAlbumById(id);
                UpdateAlbumCover(unitOfWork, album);
                return Mapper.Map<AlbumDTO>(album);
            }
        }

        public AlbumDTO GetAlbumBySlug(string titleSlug)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = unitOfWork.Albums.GetAlbumBySlug(titleSlug);
                UpdateAlbumCover(unitOfWork, album);
                return Mapper.Map<AlbumDTO>(album);
            }
        }

        public void CreateAlbum(AlbumDTO albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = Mapper.Map<Album>(albumDto);
                album.UserId = WebSecurityService.GetCurrentUser().UserId;
                album.TitleSlug = album.Title.Unidecode().GenerateSlug();
                unitOfWork.Albums.Add(album);
                unitOfWork.Complete();
            }
        }

        public void UpdateAlbum(AlbumDTO albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = Mapper.Map<Album>(albumDto);
                album.TitleSlug = album.Title.Unidecode().GenerateSlug();
                unitOfWork.Albums.UpdateAlbum(album);
                unitOfWork.Complete();
            }
        }
        public void RemovePhotoFromAlbum(int albumId, int photoId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                unitOfWork.Albums.RemovePhotoFromAlbum(albumId, photoId);
                unitOfWork.Complete();
            }
        }

        public void AddPhotoToAlbum(int albumId, int photoIds)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                unitOfWork.Albums.AddPhotoToAlbum(albumId, photoIds);
                unitOfWork.Complete();
            }
        }

        public static int GetAlbumsNumberForCurrentUser()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                return unitOfWork.Albums.GetAlbumsByUser(WebSecurityService.GetCurrentUser()).Count;
            }
        }
    }
}
