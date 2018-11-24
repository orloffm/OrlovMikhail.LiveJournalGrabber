using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    public sealed class Userpic : IEquatable<Userpic>
    {
        [XmlAttribute("height")]
        [DefaultValue(null)]
        public string Height { get; set; }

        [XmlIgnore]
        public bool HeightSpecified => !string.IsNullOrWhiteSpace(Height);

        [XmlAttribute("url")]
        [DefaultValue(null)]
        public string Url { get; set; }

        [XmlIgnore]
        public bool UrlSpecified => !string.IsNullOrWhiteSpace(Url);

        [XmlAttribute("width")]
        [DefaultValue(null)]
        public string Width { get; set; }

        [XmlIgnore]
        public bool WidthSpecified => !string.IsNullOrWhiteSpace(Width);

        public Uri GetUri()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return null;
            }

            return new Uri(Url);
        }

        public override string ToString()
        {
            return Url;
        }

        #region equality

        public static bool AreEqual(Userpic a, Userpic b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return string.Equals(a.Url, b.Url, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Userpic other)
        {
            return AreEqual(this, other);
        }

        public override bool Equals(object obj)
        {
            return AreEqual(this, obj as Userpic);
        }

        public override int GetHashCode()
        {
            if (Url == null)
            {
                return 0;
            }

            return Url.GetHashCode();
        }

        #endregion
    }
}