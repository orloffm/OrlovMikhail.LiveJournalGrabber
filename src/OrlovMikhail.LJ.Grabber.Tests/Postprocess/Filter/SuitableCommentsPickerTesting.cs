using System.Collections.Generic;
using NUnit.Framework;
using OrlovMikhail.LJ.Grabber.Entities;
using OrlovMikhail.LJ.Grabber.Entities.Helpers;
using OrlovMikhail.LJ.Grabber.Helpers;
using OrlovMikhail.LJ.Grabber.PostProcess.Files;

namespace OrlovMikhail.LJ.Grabber.PostProcess.Filter
{
    [TestFixture]
    public class SuitableCommentsPickerTesting
    {
        [SetUp]
        public void Prepare()
        {
            _rh = new RepliesHelper(new EntryBaseHelper(new FileUrlExtractor()));
            _picker = new SuitableCommentsPicker(_rh);
        }

        private RepliesHelper _rh;
        private SuitableCommentsPicker _picker;

        [Test]
        [Combinatorial]
        public void AddsComplexTreeAsTwoThreads(
            [Values(true, false)] bool firstLeafIsAuthor
            , [Values(true, false)] bool firstReplyIsTheSamePerson
        )
        {
            EntryPage ep = new EntryPage();
            ep.Entry.Poster.Username = "A";

            // Tree. Should become (B A C A), (X A).
            Comment a = new Comment();
            TestingShared.SetIdAndUrls(a, 1, null);
            a.Poster.Username = "B";
            ep.Replies.Comments.Add(a);

            Comment aB = new Comment();
            TestingShared.SetIdAndUrls(aB, 2, a);
            aB.Poster.Username = "A";
            a.Replies.Comments.Add(aB);

            Comment aBC = new Comment();
            TestingShared.SetIdAndUrls(aBC, 3, aB);
            aBC.Poster.Username = firstReplyIsTheSamePerson ? "B" : "C";
            aB.Replies.Comments.Add(aBC);

            Comment aBCD = new Comment();
            TestingShared.SetIdAndUrls(aBCD, 4, aBC);
            aBCD.Poster.Username = firstLeafIsAuthor ? "A" : "R";
            aBC.Replies.Comments.Add(aBCD);

            Comment aBE = new Comment();
            TestingShared.SetIdAndUrls(aBE, 5, aB);
            aBE.Poster.Username = "X";
            aB.Replies.Comments.Add(aBE);

            Comment aBEF = new Comment();
            TestingShared.SetIdAndUrls(aBEF, 6, aBE);
            aBEF.Poster.Username = "A";
            aBE.Replies.Comments.Add(aBEF);

            List<Comment[]> result = _picker.Pick(ep);
            if (firstLeafIsAuthor)
            {
                Assert.AreEqual(2, result.Count);
                CollectionAssert.AreEqual(new[] {a, aB, aBC, aBCD}, result[0]);
                CollectionAssert.AreEqual(new[] {aBE, aBEF}, result[1]);
            }
            else
            {
                if (firstReplyIsTheSamePerson)
                {
                    // We take a_b_c.
                    Assert.AreEqual(2, result.Count);
                    CollectionAssert.AreEqual(new[] {a, aB, aBC}, result[0]);
                    CollectionAssert.AreEqual(new[] {aBE, aBEF}, result[1]);
                }
                else
                {
                    // We don't take it. We don't care what he wrote.
                    Assert.AreEqual(1, result.Count);
                    CollectionAssert.AreEqual(new[] {a, aB, aBE, aBEF}, result[0]);
                }
            }
        }

        [Description("Simple tree, one author comment.")]
        [Test]
        public void SelectsCommentsAsExpected()
        {
            EntryPage ep = TestingShared.GenerateEntryPage(true);

            List<Comment[]> result = _picker.Pick(ep);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(
                3
                , result[0]
                    .Length
            );
        }

        [Test]
        public void TopAuthorCommentIsAdded()
        {
            EntryPage ep = new EntryPage();
            ep.Entry.Poster.Username = "A";

            Comment a = new Comment();
            TestingShared.SetIdAndUrls(a, 1, null);
            a.Poster.Username = "A";
            ep.Replies.Comments.Add(a);

            Comment aB = new Comment();
            TestingShared.SetIdAndUrls(aB, 2, a);
            aB.Poster.Username = "B";
            a.Replies.Comments.Add(aB);

            List<Comment[]> result = _picker.Pick(ep);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(
                1
                , result[0]
                    .Length
            );
            Assert.AreSame(a, result[0][0]);
        }
    }
}