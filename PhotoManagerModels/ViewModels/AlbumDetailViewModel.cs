using System;
using System.Collections.Generic;
using System.ComponentModel;
using PhotoManagerModels.Models;

namespace PhotoManagerModels.ViewModels
{
    public class AlbumDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<AlbumCommentViewModel> Comments { get; set; }
        public List<PhotoListViewModel> Photos { get; set; }
        [DisplayName("Album cover")]
        public byte[] CoverImageData { get; set; }
        [DisplayName("Album created")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Album modified")]
        public DateTime? LastModified { get; set; }

        public AlbumDetailViewModel()
        {
            Photos = new List<PhotoListViewModel>();
            Categories = new List<CategoryViewModel>();
            Comments = new List<AlbumCommentViewModel>();
            CreatedDate = DateTime.Now;
        }
    }
}
