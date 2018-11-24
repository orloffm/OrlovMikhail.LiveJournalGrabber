using System;
using NUnit.Framework;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [TestFixture]
    public sealed class CommentPagesTesting
    {
        [Test]
        public void SaysItsEmptyOnlyWhenItIsReallyEmpty()
        {
            CommentPages cp = new CommentPages();
            Assert.IsTrue(CommentPages.IsEmpty(cp));

            Action<CommentPages>[] changers = new Action<CommentPages>[]
            {
                z => z.Current = 2,
                z => z.Total = 2,
                z => z.FirstUrl = "http://1",
                z => z.LastUrl = "http://1",
                z => z.PrevUrl = "http://1",
                z => z.NextUrl = "http://1",
            };

            foreach(Action<CommentPages> changer in changers)
            {
                cp = new CommentPages();
                changer(cp);

                Assert.IsFalse(CommentPages.IsEmpty(cp));
            }
        }

        [Test]
        public void CommentPagesEmptyIsEmpty()
        {
            Assert.IsTrue(CommentPages.IsEmpty(CommentPages.Empty));
        }
    }
}
