using System.IO;


namespace PhotoManager.BLL.Utils
{
    public static class StreamHelper
    {
        public static byte[] ReadAllBytes(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
