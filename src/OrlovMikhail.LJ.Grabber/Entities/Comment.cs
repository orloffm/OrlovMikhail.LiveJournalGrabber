using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [Serializable]
    [DebuggerDisplay("{Id} {Poster.Username}: {Text}")]
    public sealed class Comment : EntryBase, IHasReplies, IEquatable<Comment>
    {
        public Comment()
        {
            IsFull = true;
            Policy = UsagePolicy.Default;
            Replies = new Replies();
        }

        [XmlAttribute("depth")]
        public int Depth { get; set; }

        [XmlAttribute("deleted")]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [XmlIgnore]
        public bool IsDeletedSpecified => IsDeleted;

        [XmlAttribute("frozen")]
        [DefaultValue(false)]
        public bool IsFrozen { get; set; }

        [XmlIgnore]
        public bool IsFrozenSpecified => IsFrozen;

        [XmlAttribute("full")]
        [DefaultValue(true)]
        public bool IsFull { get; set; }

        [XmlIgnore]
        public bool IsFullSpecified => !IsFull;

        [XmlAttribute("screened")]
        [DefaultValue(false)]
        public bool IsScreened { get; set; }

        [XmlIgnore]
        public bool IsScreenedSpecified => IsScreened;

        [XmlAttribute("suspended")]
        [DefaultValue(false)]
        public bool IsSuspendedUser { get; set; }

        [XmlIgnore]
        public bool IsSuspendedUserSpecified => IsSuspendedUser;

        [XmlElement("comments")]
        public Replies Replies { get; set; }

        [XmlElement("parent")]
        public string ParentUrl { get; set; }

        [XmlIgnore]
        public bool ParentUrlSpecified => !string.IsNullOrWhiteSpace(ParentUrl);

        /// <summary>
        ///     Allows to override the usage of the
        ///     comment when writing the target file.
        /// </summary>
        [XmlAttribute("policy")]
        [DefaultValue(UsagePolicy.Default)]
        public UsagePolicy Policy { get; set; }

        [XmlIgnore]
        public bool PolicySpecified => Policy != UsagePolicy.Default;

        public Comment MakeClone()
        {
            return MemberwiseClone() as Comment;
        }

        #region equality

        public bool Equals(Comment other)
        {
            return other != null && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Comment);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }

    public enum UsagePolicy
    {
        Default = 0
        , Ignore
        , Forced
    }
}