using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.Extractor.FolderNamingStrategy
{
    public class SubfolderPassthroughNamingStrategy : IFolderNamingStrategy
    {
        private readonly string _innerFolder;

        public SubfolderPassthroughNamingStrategy(string innerFolder)
        {
            _innerFolder = innerFolder;
        }

        public bool TryGetSubfolderByEntry(IEntryBase eb, out string sf)
        {
            sf = _innerFolder;
            return true;
        }
    }
}
