namespace SteelStudio.Web.Helpers
{
    using System;
    using System.IO;
    using System.Web;
    public static class FileHelper
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;
            string ext = string.Empty;
            string guid = string.Empty;
            string filename = string.Empty;

            if (file != null)
            {
                ext = Path.GetExtension(file.FileName);
                guid = Guid.NewGuid().ToString();
                filename = $"{guid}{ext}";
                pic = Path.GetFileName(filename);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
            }

            return pic;
        }
    }
}