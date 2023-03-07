using System;
using System.IO;

namespace FOBOS_API.Utils
{
    public static class ServerApp
    {
        public static string MapPath(string path)
        {
            return Path.Combine(
                (string)AppDomain.CurrentDomain.GetData("ContentRootPath"),
                path);
        }
    }
}
