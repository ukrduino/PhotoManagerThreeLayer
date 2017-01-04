using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoManagerThreeLayer.ViewModels
{
    public class AlbumDetailViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        [DisplayName("Album created")]
        public DateTime Created { get; set; }
        [DisplayName("Album modified")]
        public DateTime Modified { get; set; }

        public AlbumDetailViewModel()
        {

        }
    }
}
