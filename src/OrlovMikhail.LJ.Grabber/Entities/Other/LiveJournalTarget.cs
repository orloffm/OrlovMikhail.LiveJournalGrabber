using System;
using System.Text;
using System.Text.RegularExpressions;

namespace OrlovMikhail.LJ.Grabber.Entities.Other
{
    /// <summary>
    ///     Basically represents a LiveJournal entry/comments
    ///     URL. Is a separate class for ease of usage.
    /// </summary>
    public sealed class LiveJournalTarget
    {
        private const string extractionRegexString =
            @"^(?:https?://)?(?<username>[^\.]+)\.livejournal\.com/(?<postId>[0-9]*)\.html\??(?<parameters>[^#/]*)?(?:#.*)?$";

        private long? _commentId;

        private LiveJournalTarget(bool useStyleMine = false)
        {
            UseStyleMine = useStyleMine;
        }

        public LiveJournalTarget(
            string userName
            , long postId
            , long? commentId = null
            , int? page = null
            , bool useStyleMine = true
        ) : this(useStyleMine)
        {
            Username = userName;
            PostId = postId;
            CommentId = commentId;
            Page = page;
        }

        public long? CommentId
        {
            get => _commentId;
            private set => _commentId = value == 0 ? null : value;
        }

        /// <summary>Whether to request the page with cuts expanded.</summary>
        public bool ExpandCut { get; private set; }

        public int? Page { get; private set; }

        public long PostId { get; private set; }

        public string Username { get; private set; }

        /// <summary>Whether to apply "style mine" to the url.</summary>
        public bool UseStyleMine { get; private set; }

        public static LiveJournalTarget FromString(string rawString)
        {
            Regex r = new Regex(extractionRegexString, RegexOptions.Compiled);
            Match m = r.Match(rawString);

            if (!m.Success)
            {
                throw new ArgumentException();
            }

            LiveJournalTarget ret = new LiveJournalTarget(false);
            ret.Username = m.Groups["username"]
                .Value;
            ret.PostId = long.Parse(
                m.Groups["postId"]
                    .Value
            );

            string arguments = m.Groups["parameters"]
                .Value;
            string[] kvps = arguments.Split('&', '=');

            for (int i = 0; i < kvps.Length - 1; i += 2)
            {
                string key = kvps[i];
                string value = kvps[i + 1];

                if (string.Equals(key, "style", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.Equals(value, "mine", StringComparison.OrdinalIgnoreCase))
                    {
                        ret.UseStyleMine = true;
                    }
                }
                else if (string.Equals(key, "thread", StringComparison.OrdinalIgnoreCase))
                {
                    // Comment id.
                    ret.CommentId = long.Parse(value);
                }
                else if (string.Equals(key, "page", StringComparison.OrdinalIgnoreCase))
                {
                    // Paget id.
                    ret.Page = int.Parse(value);
                }
            }

            return ret;
        }

        public Uri GetUri()
        {
            return new Uri(MakeUrl());
        }

        public bool SameItem(LiveJournalTarget b)
        {
            return PostId == b.PostId && CommentId == b.CommentId && Username == b.Username;
        }

        public string ToShortString()
        {
            string s = string.Format("{0}/{1}", Username, PostId);
            if ((Page ?? 1) > 1)
            {
                s += "#" + Page.Value;
            }

            return s;
        }

        /// <summary>Creates the URL string.</summary>
        public override string ToString()
        {
            string ret = MakeUrl();
            return ret;
        }

        public LiveJournalTarget WithCutExpand(bool value = true)
        {
            LiveJournalTarget ret = MemberwiseClone() as LiveJournalTarget;
            ret.ExpandCut = value;
            return ret;
        }

        public LiveJournalTarget WithStyleMine(bool value = true)
        {
            LiveJournalTarget ret = MemberwiseClone() as LiveJournalTarget;
            ret.UseStyleMine = value;
            return ret;
        }

        private string MakeUrl()
        {
            // http://galkovsky.livejournal.com/247911.html?thread=91572583#t91572583  ?page=2
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"http://{0}.livejournal.com/{1}.html", Username, PostId);

            bool addedSomething = false;

            if (CommentId.HasValue)
            {
                sb.Append("?");
                sb.AppendFormat("thread={0}", CommentId.Value);
                addedSomething = true;
            }
            else if (Page.HasValue)
            {
                sb.Append(addedSomething ? "&" : "?");
                sb.AppendFormat("page={0}", Page.Value);
                addedSomething = true;
            }

            if (UseStyleMine)
            {
                sb.Append(addedSomething ? "&" : "?");
                sb.Append("style=mine");
                addedSomething = true;
            }

            if (ExpandCut)
            {
                sb.Append(addedSomething ? "&" : "?");
                sb.Append("cut_expand=1");
                addedSomething = true;
            }

            string ret = sb.ToString();
            return ret;
        }
    }
}