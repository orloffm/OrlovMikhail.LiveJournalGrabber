using NUnit.Framework;
using OrlovMikhail.LJ.Grabber.Entities.Helpers.Interfaces;
using OrlovMikhail.LJ.Grabber.Helpers;
using Rhino.Mocks;

// ReSharper disable InvokeAsExtensionMethod

namespace OrlovMikhail.LJ.Grabber.Entities.Helpers
{
    [TestFixture]
    public sealed class EntryPageHelperTesting
    {
        [SetUp]
        public void BeforeTests()
        {
            _entryHelper = MockRepository.GenerateMock<IEntryHelper>();
            _repliesHelper = MockRepository.GenerateMock<IRepliesHelper>();

            // Replace with mocks.
            _eph = new EntryPageHelper(_entryHelper, _repliesHelper);
        }

        private EntryPageHelper _eph;
        private IEntryHelper _entryHelper;
        private IRepliesHelper _repliesHelper;

        [TestCase(true)]
        [TestCase(false)]
        public void EnsuresTargetCommentsPagesDataIsNull(bool startFromEmpty)
        {
            EntryPage target = startFromEmpty ? new EntryPage() : TestingShared.GenerateEntryPage();
            EntryPage source = TestingShared.GenerateEntryPage(true);

            _eph.AddData(target, source);

            Assert.IsTrue(CommentPages.IsEmpty(target.CommentPages));
        }

        [Test]
        public void CallMergeFunctions()
        {
            EntryPage target = new EntryPage();
            EntryPage source = new EntryPage();

            _entryHelper.Expect(z => z.UpdateWith(target.Entry, source.Entry))
                .Return(true);
            _repliesHelper.Expect(z => z.MergeFrom(target, source))
                .Return(true);

            _eph.AddData(target, source);

            _entryHelper.VerifyAllExpectations();
            _repliesHelper.VerifyAllExpectations();
        }

        [Test]
        public void ThrowsIfNoSourceSpecified()
        {
            EntryPage other = TestingShared.GenerateEntryPage();

            Assert.That(() => _eph.AddData(other, null), Throws.ArgumentNullException);
        }

        [Test]
        public void ThrowsIfNoTargetSpecified()
        {
            EntryPage other = TestingShared.GenerateEntryPage();

            Assert.That(() => _eph.AddData(null, other), Throws.ArgumentNullException);
        }
    }
}