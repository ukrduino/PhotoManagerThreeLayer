using System;
using System.Collections.Generic;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class PhotoDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
        public List<string> Categories { get; set; }
        public List<PhotoCommentViewModel> Comments { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Camera { get; set; }
        public string FocalLength { get; set; }
        public string Aperture { get; set; }
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        public bool UsedFlash { get; set; }
        public byte[] OriginalSizeImageData { get; set; }
        public List<string> Albums { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool anyOneCanSee { get; set; }

        public PhotoDetailViewModel()
        {
            Albums = new List<string>();
            Categories = new List<string>();
            Comments = new List<PhotoCommentViewModel>();
        }
    }
}
