using AutoMapper;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;

namespace PhotoManagerModels
{
    public class AutoMapperConf : Profile
    {
        public AutoMapperConf()
        {
            CreateMap<Photo, PhotoDTO>();
            CreateMap<PhotoDTO, Photo>();

            CreateMap<Album, AlbumDTO>();
            CreateMap<AlbumDTO, Album>();

            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>().ForMember(dest => dest.Albums, opt => opt.AllowNull());
        }
    }
}
