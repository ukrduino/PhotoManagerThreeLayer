using System;
using PhotoManagerModels.Models;

namespace PhotoManagerModels.DTOModels
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