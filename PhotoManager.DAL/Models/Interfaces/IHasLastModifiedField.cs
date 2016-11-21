using System;

namespace PhotoManager.DAL.Models.Interfaces
{
    public interface IHasLastModifiedField
    {
        DateTime? LastModified { get; set; }
    }
}
