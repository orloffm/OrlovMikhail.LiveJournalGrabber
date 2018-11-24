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
            this.Replies = new Replies();
            this.Entry = new Entry();
            this.CommentPages = new CommentPages();
        }

        [XmlElement("entry")]
        public Entry Entry { get; set; }

        [XmlElement("comments")]
        public Replies Replies { get; set; }

        [XmlElement("commentpages")]
        public CommentPages CommentPages { get; set; }

        public bool ShouldSerializeCommentPages()
        {
            return this.CommentPages != null && !CommentPages.IsEmpty(this.CommentPages);
        }
    }
}
