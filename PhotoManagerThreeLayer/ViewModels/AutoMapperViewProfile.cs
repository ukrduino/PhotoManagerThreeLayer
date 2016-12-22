using AutoMapper;
using PhotoManager.BLL.DTOModels;
using PhotoManager.BLL.Services;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class AutoMapperViewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PhotoDTO, PhotoListViewModel>().ForMember(dest => dest.CommentsNumber,
                opt => opt.MapFrom(src => BllCommentServices.GetCommentsNumberByPhoto(src.Id)));
            CreateMap<PhotoDTO, PhotoDetailViewModel>();
            CreateMap<PhotoDetailViewModel, PhotoDTO>();

            CreateMap<AlbumDTO, AlbumListViewModel>();
            CreateMap<AlbumDTO, AlbumDetailViewModel>().ForMember(dest=>dest.Author,
                opt => opt.MapFrom(src => WebSecurityService.GetUserNameById(src.UserId)));
            CreateMap<AlbumDetailViewModel, AlbumDTO>();

            CreateMap<PhotoCommentDTO, PhotoCommentViewModel>().ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => WebSecurityService.GetUserNameById(src.UserId)));
            CreateMap<PhotoCommentViewModel, PhotoCommentDTO>();
        }
    }
}