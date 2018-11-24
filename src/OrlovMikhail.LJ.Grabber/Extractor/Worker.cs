using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using log4net;
using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Entities.Other;
using OrlovMikhail.LJ.Grabber.Extractor.FolderNamingStrategy;
using OrlovMikhail.LJ.Grabber.Extractor.Interfaces;
using OrlovMikhail.LJ.Grabber.LayerParser;
using OrlovMikhail.LJ.Grabber.PostProcess;
using OrlovMikhail.LJ.Grabber.PostProcess.Filter;

namespace OrlovMikhail.LJ.Grabber.Extractor
{
    public class Worker : IWorker
    {
        public const string DumpFileName = "dump.xml";

        private static readonly ILog log = LogManager.GetLogger(typeof(Worker));
        private readonly IExtractor _ext;

        private readonly IFileSystem _fs;
        private readonly ILayerParser _lp;
        private readonly IRelatedDataSaver _rds;
        private readonly ISuitableCommentsPicker _scp;

        public Worker(
            IFileSystem fs
            , IExtractor ext
            , ILayerParser lp
            , IRelatedDataSaver rds
            , ISuitableCommentsPicker scp
        )
        {
            _fs = fs;
            _ext = ext;
            _lp = lp;
            _rds = rds;
            _scp = scp;
        }

        public EntryPage Work(string URI, string rootLocation, IFolderNamingStrategy subFolderGetter, string cookie)
        {
            LiveJournalTarget t = LiveJournalTarget.FromString(URI);
            ILJClientData cookieData = _ext.Client.CreateDataObject(cookie);

            // Get fresh version.
            log.InfoFormat("Extracting {0}...", t);
            EntryPage freshSource = _ext.GetFrom(t, cookieData);

            string innerFolder;
            IEntryBase freshSourceEntry = freshSource.Entry;
            if (!subFolderGetter.TryGetSubfolderByEntry(freshSourceEntry, out innerFolder))
            {
                string error = string.Format(
                    "Cannot extract number from entry {0}, \"{1}\"."
                    , freshSourceEntry.Id
                    , freshSourceEntry.Subject
                );
                throw new NotSupportedException(error);
            }

            string subFolder = string.Format("{0}\\{1}", freshSource.Entry.Date.Value.Year, innerFolder);

            string workLocation = _fs.Path.Combine(rootLocation, subFolder);
            log.Info("Will work from " + workLocation);

            EntryPage ep = null;
            string dumpFile = _fs.Path.Combine(workLocation, DumpFileName);
            if (_fs.File.Exists(dumpFile))
            {
                log.Info("File " + DumpFileName + " exists, will load it...");
                ep = _lp.ParseAsAnEntryPage(_fs.File.ReadAllText(dumpFile));
            }
            else
            {
                log.Info("File " + DumpFileName + " does not exist.");
            }

            bool needsSaving = _ext.AbsorbAllData(freshSource, cookieData, ref ep);

            log.Info("Will save changes: " + needsSaving + ".");
            if (needsSaving)
            {
                // Save the content as is.
                string content = _lp.Serialize(ep);
                _fs.Directory.CreateDirectory(workLocation);

                UTF8Encoding enc = new UTF8Encoding(true);
                _fs.File.WriteAllText(dumpFile, content, enc);

                // Pick usable comments.
                List<Comment[]> comments = _scp.Pick(ep);
                log.Info("Picked threads: " + comments.Count + ".");

                // Everything we want to store.
                var allData = new List<EntryBase>();
                allData.Add(ep.Entry);
                allData.AddRange(comments.SelectMany(a => a));

                log.Info("Making sure everything is saved.");
                _rds.EnsureAllIsSaved(allData, rootLocation, workLocation);
            }

            log.Info("Finished.");
            return ep;
        }

        public EntryPage WorkInGivenTarget(string URI, string rootLocation, string innerFolder, string cookie)
        {
            SubfolderPassthroughNamingStrategy p = new SubfolderPassthroughNamingStrategy(innerFolder);
            EntryPage ret = Work(URI, rootLocation, p, cookie);
            return ret;
        }
    }
}