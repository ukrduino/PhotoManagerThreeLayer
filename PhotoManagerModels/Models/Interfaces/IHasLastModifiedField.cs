using System;

namespace PhotoManagerModels.Models.Interfaces
{
    public interface IHasLastModifiedField
    {
        DateTime? LastModified { get; set; }
    }
}
