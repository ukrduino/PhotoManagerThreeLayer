using System;

namespace PhotoManagerModels.ViewModels
{
   public class AlbumCommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int AlbumId { get; set; }
    }
}
