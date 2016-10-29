using System.Collections.Generic;

namespace PhotoManagerModels.Models.Interfaces
{
    public interface ICategorized
    {
        List<Category> Categories { get; set; }
    }
}
