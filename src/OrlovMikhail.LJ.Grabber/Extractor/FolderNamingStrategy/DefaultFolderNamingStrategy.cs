using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrlovMikhail.LJ.Grabber
{
    /// <summary>Saves entries to numbered directories.</summary>
    public class DefaultFolderNamingStrategy : IFolderNamingStrategy
    {
        public bool TryGetSubfolderByEntry(IEntryBase entry, out string sf)
        {
            sf = GetDefaultFolderName(entry.Id);
            return true;
        }

        public static string GetDefaultFolderName(long entryId)
        {
            // Even pioneer-lj doesn't have more.
            const int totalPaddedSymbols = 7;

            string padded = entryId.ToString().PadLeft(totalPaddedSymbols, '0');
            return padded;
        }
    }
}
