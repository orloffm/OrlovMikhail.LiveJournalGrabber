using System;
using System.IO.Abstractions;

namespace OrlovMikhail.LJ.Grabber.Postprocess
{
    public abstract class StorageBase : IStorage
    {
        public abstract FileInfoBase EnsureStored(Uri url, byte[] data);

        public abstract FileInfoBase TryGet(Uri url);

        public FileInfoBase TryGet(string url)
        {
            if (String.IsNullOrEmpty(url))
                return null;

            try
            {
                return TryGet(new Uri(url));
            }
            catch
            {
                return null;
            }
        }

        public virtual void Dispose()
        {

        }
    }
}
