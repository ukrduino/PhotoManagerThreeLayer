using AutoMapper;
using PhotoManager.BLL.DTOModels;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class AutoMapperViewProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PhotoDTO, PhotoListViewModel>();
            CreateMap<PhotoDTO, PhotoDetailViewModel>();

            CreateMap<AlbumDTO, AlbumListViewModel>();
            CreateMap<AlbumDTO, AlbumDetailViewModel>();
            CreateMap<AlbumDetailViewModel, AlbumDTO>();

            CreateMap<PhotoCommentDTO, PhotoCommentViewModel>();
        }
    }
}