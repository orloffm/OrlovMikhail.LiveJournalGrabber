using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.Extractor.FolderNamingStrategy
{
    /// <summary>Gets numbering by entry.</summary>
    public interface IFolderNamingStrategy
    {
        /// <summary>
        ///     Try to extract the name of the subfolder
        ///     inside the year folder that the entry should be put to.
        /// </summary>
        bool TryGetSubfolderByEntry(IEntryBase entry, out string sf);
    }
}