using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhotoManager.DAL.Models
{
    public class PhotoComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PhotoId { get; set; }
    }
}
