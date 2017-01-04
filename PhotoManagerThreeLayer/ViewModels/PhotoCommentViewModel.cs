using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoManagerThreeLayer.ViewModels
{
   public class PhotoCommentViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public string PhotoId { get; set; }
        public string Author { get; set; }
    }
}
