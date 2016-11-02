using System;
using PhotoManagerModels.Models;

namespace PhotoManagerModels.DTOModels
{
    public class AlbumCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}