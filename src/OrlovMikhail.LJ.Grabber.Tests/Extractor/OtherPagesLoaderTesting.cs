using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OrlovMikhail.LJ.Grabber.Client;
using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Entities.Other;
using OrlovMikhail.LJ.Grabber.LayerParser;
using Rhino.Mocks;

namespace OrlovMikhail.LJ.Grabber.Extractor
{
    [TestFixture]
    public class OtherPagesLoaderTesting
    {
        [Test]
        public void DownloadsCommentPagesCorrectly()
        {
            int source = 4;
            int total = 10;

            ILJClient clientMock = MockRepository.GenerateMock<ILJClient>();
            ILayerParser parserMock = MockRepository.GenerateMock<ILayerParser>();

            // Creates a comment pages object by page index.
            Func<int, CommentPages> createCpByPage = cpi =>
            {
                CommentPages c = new CommentPages();
                c.Current = cpi;
                c.Total = total;
                if (c.Current != 1)
                {
                    c.FirstUrl = new LiveJournalTarget("galkovsky", 1, page: 1).ToString();
                    c.PrevUrl = new LiveJournalTarget("galkovsky", 1, page: c.Current - 1).ToString();
                }

                if (c.Current != total)
                {
                    c.LastUrl = new LiveJournalTarget("galkovsky", 1, page: total).ToString();
                    c.NextUrl = new LiveJournalTarget("galkovsky", 1, page: c.Current + 1).ToString();
                }

                return c;
            };

            clientMock.Expect(z => z.GetContent(Arg<LiveJournalTarget>.Is.NotNull, Arg<ILJClientData>.Is.Null))
                .Return(null)
                .WhenCalled(
                    _ =>
                    {
                        LiveJournalTarget t = (LiveJournalTarget) _.Arguments[0];
                        int page = t.Page.Value;
                        _.ReturnValue = page.ToString();
                    }
                );

            parserMock.Expect(z => z.ParseAsAnEntryPage(Arg<string>.Is.Anything))
                .Return(null)
                .WhenCalled(
                    _ =>
                    {
                        string req = (string) _.Arguments[0];

                        EntryPage ep = new EntryPage();
                        ep.CommentPages = createCpByPage(int.Parse(req));
                        _.ReturnValue = ep;
                    }
                );

            OtherPagesLoader opl = new OtherPagesLoader(parserMock, clientMock);

            // This is the source object we get from an entry page.
            CommentPages cp = createCpByPage(source);
            EntryPage[] others = opl.LoadOtherCommentPages(cp, null);

            Assert.AreEqual(total - 1, others.Length);

            IEnumerable<int> numbersWeExpect = Enumerable.Range(1, total)
                .Where(z => z != source);
            IEnumerable<int> numbersWeHave = others.Select(z => z.CommentPages.Current);
            CollectionAssert.AreEqual(numbersWeExpect, numbersWeHave);
        }
    }
}