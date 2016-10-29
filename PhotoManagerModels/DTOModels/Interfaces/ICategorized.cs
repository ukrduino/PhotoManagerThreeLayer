using System.Collections.Generic;

namespace PhotoManagerModels.DTOModels.Interfaces
{
    public interface ICategorizedDTO
    {
        List<CategoryDTO> Categories { get; set; }
    }
}
