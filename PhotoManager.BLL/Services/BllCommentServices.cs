using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManager.BLL.Services
{
    public class BllCommentServices
    {
        public List<AlbumCommentDTO> GetCommentsByAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<AlbumComment> comments = unitOfWork.AlbumComments.GetCommentsByAlbum(id);
                return Mapper.Map<List<AlbumComment>, List<AlbumCommentDTO>>(comments);
            }
        }
        public List<PhotoCommentDTO> GetCommentsByPhoto(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<PhotoComment> comments = unitOfWork.PhotoComments.GetCommentsByPhoto(id);
                return Mapper.Map<List<PhotoComment>, List<PhotoCommentDTO>>(comments);
            }
        }
    }
}
