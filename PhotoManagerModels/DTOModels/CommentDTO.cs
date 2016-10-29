using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManagerModels.DTOModels.Interfaces;

namespace PhotoManagerModels.DTOModels
{
    public class CommentDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public ICommentableDTO Commentable { get; set; }


        public CommentDTO()
        {
        }
    }
}