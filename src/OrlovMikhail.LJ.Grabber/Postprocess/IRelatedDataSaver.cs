using System.Collections.Generic;
using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.PostProcess
{
    /// <summary>Encapsulates all functionality related
    /// to saving linked files and userpics.</summary>
    public interface IRelatedDataSaver
    {
        /// <summary>Ensures that all extra data related to the post
        /// is saved.</summary>
        /// <param name="ep">Whole post.</param>
        /// <param name="bookRoot">Root of the project.</param>
        /// <param name="dumpLocation">It's location. Where the file will be stored.</param>
        void EnsureAllIsSaved(IEnumerable<EntryBase> ep, string bookRoot, string dumpLocation);
    }
}