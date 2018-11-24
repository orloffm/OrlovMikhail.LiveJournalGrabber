using System.IO.Abstractions;

namespace OrlovMikhail.LJ.Grabber.PostProcess.Userpics
{
    public class UserpicStorageFactory : IUserpicStorageFactory
    {
        private readonly IFileSystem _fs;

        public UserpicStorageFactory(IFileSystem fs)
        {
            _fs = fs;
        }

        public IUserpicStorage CreateOn(string basePath)
        {
            return new UserpicStorage(_fs, _fs.Path.Combine(basePath, "userpics"));
        }
    }
}
