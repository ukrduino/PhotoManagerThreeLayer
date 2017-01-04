using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhotoManager.DAL.Models
{
    public class PhotoComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public Guid PhotoId { get; set; }
        public int UserId { get; set; }
    }
}
