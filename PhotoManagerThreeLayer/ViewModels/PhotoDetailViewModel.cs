using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class PhotoDetailViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("Date")]
        public DateTime TakenDate { get; set; }
        public string Place { get; set; }
        public string Camera { get; set; }
        [DisplayName("Focal length")]
        public string FocalLength { get; set; }
        public string Aperture { get; set; }
        [DisplayName("Camera lock speed")]
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        [DisplayName("Used flash")]
        public bool UsedFlash { get; set; }
        public bool AnyOneCanSee { get; set; }
        public List<PhotoCommentViewModel> Comments { get; set; }
        public int SmallImageId { get; set; }
        public List<string> Albums { get; set; }
        [DisplayName("Modified")]
        public DateTime Modified { get; set; }
        [DisplayName("Uploaded")]
        public DateTime Created { get; set; }
        [DisplayName("Public")]
        public int Views { get; set; }

        public PhotoDetailViewModel()
        {
            Albums = new List<string>();
            Comments = new List<PhotoCommentViewModel>();
        }
    }
}
