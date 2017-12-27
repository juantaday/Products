using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Products.API.Helpers
{
    using System.IO;
    using System.Security;
    using System.Security.Permissions;
    using System.Web;
    public static class FilesHelper
    {
        public static bool UpLoadPhoto(
            MemoryStream stream,
            string folder,
            string name)
        {

            try
            {
                stream.Position = 0;
                var phat = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                   phat = string.Format("C:\\inetpub\\wwwroot\\ProductsApi\\Content\\Images\\Products\\{0}", name);

                //http://192.168.0.100/ProductsApi/Content/Images/Products/
                FileIOPermission f = new FileIOPermission(PermissionState.None);
             
                f.AddPathList(FileIOPermissionAccess.AllAccess, phat);

                try
                {
                    f.Demand();
                    File.WriteAllBytes(phat, stream.ToArray());
                    return true;
                }
                catch (SecurityException s)
                {
                    throw new System.ArgumentException(s.Message, s.StackTrace);
                }

            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message, ex.StackTrace);
            }
        }
    }
}