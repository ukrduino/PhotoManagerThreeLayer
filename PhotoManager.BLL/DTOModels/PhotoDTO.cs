using System;

namespace PhotoManager.BLL.DTOModels
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? TakenDate { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Camera { get; set; }
        public string FocalLength { get; set; }
        public string Aperture { get; set; }
        public string CameraLockSpeed { get; set; }
        public string ISO { get; set; }
        public bool UsedFlash { get; set; }
        public byte[] OriginalSizeImageData { get; set; }
        public byte?[] MiddleSizeImageData { get; set; }
        public byte?[] SmallSizeImageData { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Uploaded { get; set; }
        public bool AnyOneCanSee { get; set; }
        public int Views { get; set; }
    }
}