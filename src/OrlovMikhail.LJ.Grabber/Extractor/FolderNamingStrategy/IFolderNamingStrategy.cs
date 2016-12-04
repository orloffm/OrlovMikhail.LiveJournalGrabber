using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrlovMikhail.LJ.Grabber
{
    /// <summary>Gets numbering by entry.</summary>
    public interface IFolderNamingStrategy
    {
        /// <summary>Try to extract the name of the subfolder
        /// inside the year folder that the entry should be put to.</summary>
        bool TryGetSubfolderByEntry(IEntryBase entry, out string sf);
    }
}
