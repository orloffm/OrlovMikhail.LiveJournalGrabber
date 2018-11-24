using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using OrlovMikhail.LJ.Grabber.Entities.Other;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    [DebuggerDisplay("{Id}: {Text}")]
    public abstract class EntryBase : IEntryBase
    {
        private string _url;

        public EntryBase()
        {
            Poster = new UserLite();
            PosterUserpic = new Userpic();
        }

        [XmlIgnore]
        public DateTime? Date { get; set; }

        [XmlElement("user")]
        public UserLite Poster { get; set; }

        [XmlElement("userpic")]
        [DefaultValue(null)]
        public Userpic PosterUserpic { get; set; }

        [XmlElement("date")]
        public string DateValue
        {
            get => Date.HasValue ? Date.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            set => Date = ParseDateTimeFromString(value);
        }

        [XmlIgnore]
        public bool DateValueSpecified => Date.HasValue;

        [XmlAttribute("id")]
        public long Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified => Id != 0;

        [XmlElement("url")]
        public string Url
        {
            get => _url;
            set
            {
                _url = value;

                if (!string.IsNullOrEmpty(value) && Id == 0)
                {
                    // Auto-set Id from Post Id.
                    LiveJournalTarget t = LiveJournalTarget.FromString(value);
                    if ((t.CommentId ?? 0) < 1)
                    {
                        Id = t.PostId;
                    }
                }
            }
        }

        [XmlIgnore]
        public bool PosterUserpicSpecified
        {
            get
            {
                if (PosterUserpic == null)
                {
                    return false;
                }

                bool hasAnything = !string.IsNullOrWhiteSpace(PosterUserpic.Height) ||
                                   !string.IsNullOrWhiteSpace(PosterUserpic.Width) ||
                                   !string.IsNullOrWhiteSpace(PosterUserpic.Url);

                return hasAnything;
            }
        }

        [XmlElement("text")]
        [DefaultValue(null)]
        public string Text { get; set; }

        [XmlElement("subject")]
        [DefaultValue(null)]
        public string Subject { get; set; }

        [XmlIgnore]
        public bool SubjectSpecified => !string.IsNullOrWhiteSpace(Subject);

        [XmlIgnore]
        public bool TextSpecified => !string.IsNullOrWhiteSpace(Text);

        public static DateTime? ParseDateTimeFromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string[] s = value.Split(new[] {'-', ':', ' '}, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 6)
            {
                return null;
            }

            DateTime result = new DateTime(
                int.Parse(s[0])
                , int.Parse(s[1])
                , int.Parse(s[2])
                , int.Parse(s[3])
                , int.Parse(s[4])
                , int.Parse(s[5])
            );
            return result;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}