using System.Collections.Generic;
using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.PostProcess.Filter
{
    /// <summary>Returns comments as arrays of branches
    /// containing journal owner.</summary>
   public interface ISuitableCommentsPicker
   {
       /// <summary>Do the conversion. Replace WWW links
       /// with local links and leave only meaningful comments.</summary>
       List<Comment[]> Pick(EntryPage ep);
   }
}
