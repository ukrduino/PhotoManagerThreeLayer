using System;

namespace PhotoManager.DAL.Models.Interfaces
{
    public interface IHasModifiedField
    {
        DateTime? Modified { get; set; }
    }
}
