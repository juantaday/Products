namespace Products.Helpers
{
    using System.IO;
    public class FilesHelper
    {
        public static byte [] ReadFully(Stream  imput)
        {
            using (MemoryStream ms = new MemoryStream ())
            {
                imput.CopyTo(ms);
                return ms.ToArray ();
                // 49
            }
        }
    }
}
