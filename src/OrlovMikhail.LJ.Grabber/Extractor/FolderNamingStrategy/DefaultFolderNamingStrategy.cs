using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.Extractor.FolderNamingStrategy
{
    /// <summary>Saves entries to numbered directories.</summary>
    public class DefaultFolderNamingStrategy : IFolderNamingStrategy
    {
        public static string GetDefaultFolderName(long entryId)
        {
            // Even pioneer-lj doesn't have more.
            const int totalPaddedSymbols = 7;

            string padded = entryId.ToString()
                .PadLeft(totalPaddedSymbols, '0');
            return padded;
        }

        public bool TryGetSubfolderByEntry(IEntryBase entry, out string sf)
        {
            sf = GetDefaultFolderName(entry.Id);
            return true;
        }
    }
}