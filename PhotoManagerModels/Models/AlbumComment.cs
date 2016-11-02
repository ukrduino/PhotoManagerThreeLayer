using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManagerModels.Models.Interfaces;

namespace PhotoManagerModels.Models
{
    public class AlbumComment : IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }
    }
}
