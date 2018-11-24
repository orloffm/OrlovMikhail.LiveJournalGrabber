using System.Collections.Generic;
using System.Linq;
using log4net;
using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Entities.Other;
using OrlovMikhail.LJ.Grabber.Extractor.Interfaces;
using OrlovMikhail.LJ.Grabber.LayerParser;

namespace OrlovMikhail.LJ.Grabber.Extractor
{
    public sealed class OtherPagesLoader : IOtherPagesLoader
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OtherPagesLoader));
        private readonly ILJClient _client;
        private readonly ILayerParser _parser;

        public OtherPagesLoader(ILayerParser parser, ILJClient client)
        {
            _parser = parser;
            _client = client;
        }

        public EntryPage[] LoadOtherCommentPages(CommentPages commentPages, ILJClientData clientData)
        {
            int initialIndex = commentPages.Current;
            int total = commentPages.Total;

            log.Info(
                string.Format(
                    "Loading other comment pages given page №{0} out of {1}."
                    , commentPages.Current
                    , commentPages.Total
                )
            );

            // We need to download these.
            int[] need = Enumerable.Range(1, total)
                .Where(i => i != initialIndex)
                .ToArray();
            IDictionary<int, LiveJournalTarget> targets = new SortedDictionary<int, LiveJournalTarget>();
            IDictionary<int, EntryPage> pages = new SortedDictionary<int, EntryPage>();
            EntryPage p;

            CommentPages latest = commentPages;
            while (pages.Count < need.Length)
            {
                int cur = latest.Current;

                if (cur != 1 && !string.IsNullOrWhiteSpace(latest.FirstUrl))
                {
                    targets[1] = LiveJournalTarget.FromString(latest.FirstUrl);
                }

                if (cur != total && !string.IsNullOrWhiteSpace(latest.LastUrl))
                {
                    targets[total] = LiveJournalTarget.FromString(latest.LastUrl);
                }

                if (!string.IsNullOrWhiteSpace(latest.PrevUrl))
                {
                    targets[cur - 1] = LiveJournalTarget.FromString(latest.PrevUrl);
                }

                if (!string.IsNullOrWhiteSpace(latest.NextUrl))
                {
                    targets[cur + 1] = LiveJournalTarget.FromString(latest.NextUrl);
                }

                // First target without a page.
                int keyToDownload = targets.Keys.First(z => z != initialIndex && !pages.ContainsKey(z));
                log.Info(string.Format("Will download page №{0}.", keyToDownload));
                LiveJournalTarget targetToDownload = targets[keyToDownload];

                // Download the page.
                string content = _client.GetContent(targetToDownload, clientData);
                p = _parser.ParseAsAnEntryPage(content);
                latest = p.CommentPages;
                pages[keyToDownload] = p;
                log.Info(string.Format("Parsed page №{0}.", keyToDownload));
            }

            EntryPage[] ret = pages.Values.ToArray();
            return ret;
        }
    }
}