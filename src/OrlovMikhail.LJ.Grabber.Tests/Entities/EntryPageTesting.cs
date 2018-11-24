using System;
using System.Linq;
using NUnit.Framework;

namespace OrlovMikhail.LJ.Grabber.Entities
{
    [TestFixture]
    public sealed class EntryPageTesting
    {
        [Test]
        public void CommentsAreSerializedOnlyWhenAreNotEmpty()
        {
            // Does the function exist with the proper name?
            Type t = typeof(EntryPage);

            string propertyName = t.GetProperties()
                .Single(z => z.PropertyType == typeof(CommentPages))
                .Name;
            bool methodExists = t.GetMethods()
                .Any(z => z.Name == "ShouldSerialize" + propertyName);
            if (!methodExists)
            {
                throw new Exception("No serialization method.");
            }

            // Non-empty
            EntryPage ep = new EntryPage();
            ep.CommentPages.Total = 10;
            Assert.IsTrue(ep.ShouldSerializeCommentPages());

            ep = new EntryPage();
            Assert.IsFalse(ep.ShouldSerializeCommentPages());
        }
    }
}