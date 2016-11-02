using System;

namespace PhotoManagerModels.DTOModels
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] CoverImageData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}