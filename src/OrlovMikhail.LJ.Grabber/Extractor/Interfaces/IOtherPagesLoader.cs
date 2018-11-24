using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities;

namespace OrlovMikhail.LJ.Grabber.Extractor.Interfaces
{
    /// <summary>Loads all other pages in addition to the source one.</summary>
    public interface IOtherPagesLoader
    {
        EntryPage[] LoadOtherCommentPages(CommentPages commentPages, ILJClientData clientData);
    }
}