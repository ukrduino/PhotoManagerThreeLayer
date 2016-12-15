using AutoMapper;
using PhotoManager.DAL.Models;

namespace PhotoManager.BLL.DTOModels
{
    public class AutoMapperBllProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Photo, PhotoDTO>();
            CreateMap<PhotoDTO, Photo>();

            CreateMap<Album, AlbumDTO>();
            CreateMap<AlbumDTO, Album>().ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<PhotoComment, PhotoCommentDTO>();
            CreateMap<PhotoCommentDTO, PhotoComment>();
        }
    }
}
