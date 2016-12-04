using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrlovMikhail.LJ.Grabber
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
