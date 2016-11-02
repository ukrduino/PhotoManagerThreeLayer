using System.Collections.Generic;
using AutoMapper;
using PhotoManager.DAL;
using PhotoManager.DAL.Repositories;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManager.BLL.Services
{
    public class BllCategoryServices
    {
        public List<CategoryDTO> GetCategoriesByAlbum(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                List<Category> categories = unitOfWork.Categories.GetCategoriesByAlbum(id);
                return Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            }
        }
    }
}
