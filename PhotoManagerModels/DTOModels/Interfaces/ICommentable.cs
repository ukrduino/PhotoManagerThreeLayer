using System.Collections.Generic;

namespace PhotoManagerModels.DTOModels.Interfaces
{
    public interface ICommentableDTO
    {
        List<CommentDTO> Comments { get; set; }
    }
}
