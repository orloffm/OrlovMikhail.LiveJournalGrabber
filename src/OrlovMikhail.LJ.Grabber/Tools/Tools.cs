using System;
using System.Linq;

namespace OrlovMikhail.LJ.Grabber.Tools
{
    public static class Tools
    {
        private static readonly string[] extensions = {"mp3", "pdf"};
        private static readonly string[] imageExtensions = {"jpg", "jpeg", "gif", "png", "bmp"};

        public static bool IsAnImage(string url)
        {
            string ext = GetExtension(url);
            if (string.IsNullOrWhiteSpace(ext))
            {
                return false;
            }

            return imageExtensions.Any(e => string.Equals(e, ext, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetExtension(string url)
        {
            int dotIndex = url.LastIndexOf('.');
            if (dotIndex < 0 || url.Length == dotIndex + 1)
            {
                return null;
            }

            string ext = url.Substring(dotIndex + 1);
            return ext;
        }
    }
}