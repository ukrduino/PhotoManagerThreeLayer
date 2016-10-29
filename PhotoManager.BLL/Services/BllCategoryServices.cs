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
        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PhotoManagerDbContext()))
            {
                IEnumerable<Category> categories = unitOfWork.Categories.GetAll();
                return Mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }
        }
    }
}
