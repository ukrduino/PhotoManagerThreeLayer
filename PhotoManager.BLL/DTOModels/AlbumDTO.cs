using System;

namespace PhotoManager.BLL.DTOModels
{
    public class AlbumDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TitleSlug { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}