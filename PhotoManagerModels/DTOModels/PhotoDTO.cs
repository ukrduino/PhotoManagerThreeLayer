using System;
using System.Collections.Generic;

namespace PhotoManagerModels.DTOModels
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TakenDate { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Camera { get; set; }
        public string FocalLength { get; set; }
        public string Aperture { get; set; }
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        public bool UsedFlash { get; set; }
        public byte[] OriginalSizeImageData { get; set; }
        public byte[] MiddleSizeImageData { get; set; }
        public byte[] SmallSizeImageData { get; set; }
        public List<AlbumDTO> Albums { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime CreatedDate { get; set; }

        public PhotoDTO()
        {
            Albums = new List<AlbumDTO>();
            Categories = new List<CategoryDTO>();
            Comments = new List<CommentDTO>();
        }
    }
}