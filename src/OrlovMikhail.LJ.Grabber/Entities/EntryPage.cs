using System;
using System.Xml.Serialization;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    [XmlRoot("entrypage")]
    public sealed class EntryPage : IHasReplies
    {
        public EntryPage()
        {
            Replies = new Replies();
            Entry = new Entry();
            CommentPages = new CommentPages();
        }

        [XmlElement("entry")]
        public Entry Entry { get; set; }

        [XmlElement("comments")]
        public Replies Replies { get; set; }

        [XmlElement("commentpages")]
        public CommentPages CommentPages { get; set; }

        public bool ShouldSerializeCommentPages()
        {
            return CommentPages != null && !CommentPages.IsEmpty(CommentPages);
        }
    }
}