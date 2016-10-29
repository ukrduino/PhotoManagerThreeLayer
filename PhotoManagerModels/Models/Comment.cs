using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoManagerModels.Models.Interfaces;

namespace PhotoManagerModels.Models
{
    public class Comment: IHasLastModifiedField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public int CommentableId { get; set; }
        public ICommentable Commentable { get; set; }


        public Comment(string text, string author)
        {
            Text = text;
            Author = author;
        }
    }
}