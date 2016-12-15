using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.BLL.Services;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class AutoMapperViewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PhotoDTO, PhotoListViewModel>();
            CreateMap<PhotoDTO, PhotoDetailViewModel>();
            CreateMap<PhotoDetailViewModel, PhotoDTO>();

            CreateMap<AlbumDTO, AlbumListViewModel>();
            CreateMap<AlbumDTO, AlbumDetailViewModel>().ForMember(dest=>dest.Author,
                opt => opt.MapFrom(src => WebSecurityService.GetUserNameById(src.UserId)));
            CreateMap<AlbumDetailViewModel, AlbumDTO>();

            CreateMap<PhotoCommentDTO, PhotoCommentViewModel>();
        }
    }
}