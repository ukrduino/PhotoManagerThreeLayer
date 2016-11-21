using System;
using PhotoManager.DAL.Models;

namespace PhotoManager.BLL.DTOModels
{
    public class PhotoCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
    }
}