namespace Products.Backend.Helpers
{
    using System;
    using System.IO;
    using System.Web;
    public class FilesHelper
    {
        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            try
            {
                if (file != null)
                {
                    var guid = Guid.NewGuid().ToString();
                   // pic = Path.GetFileName(file.FileName);
                    pic = string.Format("{0}.png", guid);
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                    file.SaveAs(path);
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    file.InputStream.CopyTo(ms);
                    //    byte[] array = ms.GetBuffer();
                    //}
                }
                return pic;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}