using System.Collections.Generic;

namespace OrlovMikhail.LJ.Grabber.Entities.Helpers.Interfaces
{
    public interface IRepliesHelper
    {
        IEnumerable<Comment> EnumerateAll(Replies target);

        IEnumerable<Comment> EnumerateFull(Replies target);

        /// <summary>All non-deleted comments that don't have content.</summary>
        IEnumerable<Comment> EnumerateRequiringFullUp(Replies target);

        /// <summary>
        ///     Merges comments tree. Objects should point to
        ///     equal items. They both should be entry pages or the same comment.
        ///     If they are comments, its content is also merged.
        /// </summary>
        /// <param name="target">Tree point from which we want to update.</param>
        /// <param name="fullVersion">Tree point from other source with full or extra data.</param>
        bool MergeFrom<T>(T target, T fullVersion) where T : IHasReplies;
    }
}