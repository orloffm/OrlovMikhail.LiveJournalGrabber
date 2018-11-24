using System;
using log4net;
using OrlovMikhail.LJ.Grabber.Entities.Helpers.Interfaces;

namespace OrlovMikhail.LJ.Grabber.Entities.Helpers
{
    public class EntryHelper : IEntryHelper
    {
        private readonly IEntryBaseHelper _ebh;
        static readonly ILog log = LogManager.GetLogger(typeof(EntryHelper));

        public EntryHelper(IEntryBaseHelper ebh)
        {
            _ebh = ebh;
        }

        /// <summary>Updates entry with newer information
        /// or just puts data if it was empty.</summary>
        /// <param name="target">Entry to update.</param>
        /// <param name="source">Entry to get data from.</param>
        public bool UpdateWith(Entry target, Entry source)
        {
            log.DebugFormat("Merging entry {0} with another source.", target);

            bool updated = false;

            if (target == null || source == null)
                throw new ArgumentNullException();

            updated |= _ebh.UpdateWith(target, source);

            updated |= _ebh.UpdateStringProperty(source.NextUrl, target.NextUrl, s => target.NextUrl = s);
            updated |= _ebh.UpdateStringProperty(source.PreviousUrl, target.PreviousUrl, s => target.PreviousUrl = s);


            return updated;
        }
    }
}
