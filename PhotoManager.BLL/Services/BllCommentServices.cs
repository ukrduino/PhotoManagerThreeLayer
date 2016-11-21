using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.DAL;
using PhotoManager.DAL.Models;
using PhotoManager.DAL.Repositories;

namespace PhotoManager.BLL.Services
{
    public class BllCommentServices
    {
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
