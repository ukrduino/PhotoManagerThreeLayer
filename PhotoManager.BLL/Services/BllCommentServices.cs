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
        public IEnumerable<CommentDTO> GetAllComments()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Comment> comments = unitOfWork.Comments.GetAll();
                return Mapper.Map<IEnumerable<CommentDTO>>(comments);
            }
        }
    }
}
