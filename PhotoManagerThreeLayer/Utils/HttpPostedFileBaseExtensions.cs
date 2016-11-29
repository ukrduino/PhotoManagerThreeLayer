using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public static class HttpPostedFileBaseExtensions
{
    public const int ImageMinimumBytes = 512;

    public static bool IsJpgImage(this HttpPostedFileBase postedFile)
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (postedFile.ContentType.ToLower() != "image/jpg" &&
                    postedFile.ContentType.ToLower() != "image/jpeg"
                    //&& postedFile.ContentType.ToLower() != "image/pjpeg"
                    //&& postedFile.ContentType.ToLower() != "image/gif"
                    //&& postedFile.ContentType.ToLower() != "image/x-png"
                    //&& postedFile.ContentType.ToLower() != "image/png"
                    )
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg"
            //&& Path.GetExtension(postedFile.FileName).ToLower() != ".png"
            //&& Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
            )
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            if (!postedFile.InputStream.CanRead)
            {
                return false;
            }

            if (postedFile.ContentLength < ImageMinimumBytes)
            {
                return false;
            }

            byte[] jpgHeader = { 0xFF, 0xD8, 0xFF, 0xE1 };
            byte[] buffer = new byte[jpgHeader.Length];
            postedFile.InputStream.Read(buffer, 0, jpgHeader.Length);
            return jpgHeader.SequenceEqual(buffer);

        }
        catch (Exception)
        {
            return false;
        }
    }
}