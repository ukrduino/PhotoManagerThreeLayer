using System;

namespace PhotoManager.BLL.DTOModels
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}