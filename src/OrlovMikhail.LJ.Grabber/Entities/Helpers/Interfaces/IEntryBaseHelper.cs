using System;
using System.Collections.Generic;

namespace OrlovMikhail.LJ.Grabber.Entities.Helpers.Interfaces
{
    public interface IEntryBaseHelper
    {
        /// <summary>Returns all files linked to, including images.</summary>
        /// <param name="source">Entry and/or comments.</param>
        /// <returns>Array of files.</returns>
        Uri[] GetFiles(IEnumerable<EntryBase> source);

        /// <summary>
        ///     Enumerates through all distinct userpics
        ///     present in the entry.
        /// </summary>
        /// <param name="source">Entry.</param>
        /// <returns>Pairs of usernames and userpics.</returns>
        Tuple<string, Userpic>[] GetUserpics(IEnumerable<EntryBase> source);

        bool UpdateStringProperty(string sourceValue, string targetValue, Action<string> targetSetter);

        bool UpdateWith(EntryBase target, EntryBase source);
    }
}