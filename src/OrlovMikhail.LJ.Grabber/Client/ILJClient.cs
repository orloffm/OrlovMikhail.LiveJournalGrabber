using System;
using OrlovMikhail.LJ.Grabber.Entities.Other;

namespace OrlovMikhail.LJ.Grabber.Client
{
    /// <summary>Actually downloads stuff from LiveJournal.</summary>
    public interface ILJClient
    {
        /// <summary>
        ///     Creates data object based on a string.
        ///     This can have various meanings based on the implementers.
        /// </summary>
        ILJClientData CreateDataObject(string input);

        byte[] DownloadFile(Uri target);

        string GetContent(LiveJournalTarget target, ILJClientData data);
    }
}