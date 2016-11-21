using System;

namespace PhotoManagerThreeLayer.ViewModels
{
   public class PhotoCommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int PhotoId { get; set; }
    }
}
