using System.IO.Abstractions;

namespace OrlovMikhail.LJ.Grabber.PostProcess.Files
{
    public class FileStorageFactory : IFileStorageFactory
    {
        private readonly IFileSystem _fs;

        public FileStorageFactory(IFileSystem fs)
        {
            _fs = fs;
        }

        public IFileStorage CreateOn(string dumpLocation)
        {
            return new FileStorage(_fs, dumpLocation);
        }
    }
}
