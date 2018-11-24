using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    [XmlRoot("commentpages")]
    public sealed class CommentPages
    {
        public CommentPages()
        {
            Current = 1;
            Total = 1;
        }

        [XmlAttribute("current")]
        [DefaultValue(1)]
        public int Current { get; set; }

        public static CommentPages Empty => new CommentPages();

        [XmlElement("url_first")]
        public string FirstUrl { get; set; }

        [XmlElement("url_last")]
        public string LastUrl { get; set; }

        [XmlElement("url_prev")]
        public string PrevUrl { get; set; }

        [XmlElement("url_next")]
        public string NextUrl { get; set; }

        [XmlAttribute("total")]
        [DefaultValue(1)]
        public int Total { get; set; }

        /// <summary>Checks that the instance has no data.</summary>
        public static bool IsEmpty(CommentPages value)
        {
            if (!string.IsNullOrWhiteSpace(value.FirstUrl))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(value.LastUrl))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(value.PrevUrl))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(value.NextUrl))
            {
                return false;
            }

            if (value.Current > 1 || value.Total > 1)
            {
                return false;
            }

            return true;
        }
    }
}