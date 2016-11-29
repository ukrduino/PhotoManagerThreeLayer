using AutoMapper;
using PhotoManager.BLL.Services;
using PhotoManager.DAL.Models;

namespace PhotoManager.BLL.DTOModels
{
    public class AutoMapperBllProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photo, PhotoDTO>();
            //CreateMap<PhotoDTO, Photo>();

            CreateMap<Album, AlbumDTO>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<AlbumDTO, Album>().ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            CreateMap<PhotoComment, PhotoCommentDTO>();
            //CreateMap<PhotoCommentDTO, PhotoComment>();
        }
    }
}
