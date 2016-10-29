using System.Collections.Generic;

namespace PhotoManagerModels.Models.Interfaces
{
    public interface ICommentable
    {
        List<Comment> Comments { get; set; }
    }
}
