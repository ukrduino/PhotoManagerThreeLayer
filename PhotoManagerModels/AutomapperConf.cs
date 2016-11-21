﻿using System.Threading;
using AutoMapper;
using PhotoManagerModels.DTOModels;
using PhotoManagerModels.Models;
using PhotoManagerModels.ViewModels;

namespace PhotoManagerModels
{
    public class AutoMapperConf : Profile
    {
        public AutoMapperConf()
        {
            CreateMap<Photo, PhotoDTO>();
            //CreateMap<PhotoDTO, Photo>();
            CreateMap<PhotoDTO, PhotoListViewModel>();
            //CreateMap<PhotoDTO, PhotoDetailViewModel>();

            CreateMap<Album, AlbumDTO>();
            CreateMap<AlbumDTO, Album>()
                .ForMember(dest=>dest.CoverImageData, opt => opt.Condition(src => src.CoverImageData != null && src.CoverImageData.Length > 0))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<AlbumDTO, AlbumListViewModel>();
            CreateMap<AlbumDTO, AlbumDetailViewModel>();
            CreateMap<AlbumDetailViewModel, AlbumDTO>();

            CreateMap<PhotoComment, PhotoCommentDTO>();
            //CreateMap<PhotoCommentDTO, PhotoComment>();
            CreateMap<PhotoCommentDTO, PhotoCommentViewModel>();
        }
    }
}
