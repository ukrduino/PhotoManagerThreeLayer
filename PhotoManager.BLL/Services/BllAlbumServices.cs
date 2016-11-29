﻿using System.Collections.Generic;
using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public class BllAlbumServices
    {
        public List<AlbumDTO> GetAllAlbums()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Album> albums = unitOfWork.Albums.GetAlbumsByUser(WebSecurityService.GetCurrentUser());
                return Mapper.Map<IEnumerable<Album>, List<AlbumDTO>>(albums);
            }
        }
        public AlbumDTO GetAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = unitOfWork.Albums.GetAlbumById(id);
                return Mapper.Map<AlbumDTO>(album);
            }
        }

        public void CreateAlbum(AlbumDTO albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {

                Album album = Mapper.Map<Album>(albumDto);
                album.UserId = WebSecurityService.GetCurrentUser().UserId;
                unitOfWork.Albums.Add(album);
                unitOfWork.Complete();
            }
        }
        public void UpdateAlbum(AlbumDTO albumDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                Album album = Mapper.Map<Album>(albumDto);
                unitOfWork.Albums.UpdateAlbum(album);
                unitOfWork.Complete();
            }
        }
        public void RemovePhotosFromAlbum(int albumId, List<int> photoIds)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                unitOfWork.Albums.RemovePhotosFromAlbum(albumId, photoIds);
                unitOfWork.Complete();
            }
        }
    }
}
