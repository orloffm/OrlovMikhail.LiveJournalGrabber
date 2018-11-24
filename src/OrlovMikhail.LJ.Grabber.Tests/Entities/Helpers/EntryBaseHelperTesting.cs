using System;
using NUnit.Framework;
using OrlovMikhail.LJ.Grabber.PostProcess.Files;
using Rhino.Mocks;

// ReSharper disable InvokeAsExtensionMethod

namespace OrlovMikhail.LJ.Grabber.Entities.Helpers
{
    [TestFixture]
    public sealed class EntryBaseHelperTesting
    {
        [SetUp]
        public void BeforeTests()
        {
            _fileUrlExtractor = MockRepository.GenerateMock<IFileUrlExtractor>();
        }

        private IFileUrlExtractor _fileUrlExtractor;

        [Test]
        public void RemovesDuplicateUserpicsProperly()
        {
            Userpic a = new Userpic {Url = "ABC"};
            Tuple<string, Userpic> ta = Tuple.Create("User", a);

            EntryPage source = new EntryPage();

            EntryBaseHelper eh = MockRepository.GeneratePartialMock<EntryBaseHelper>(_fileUrlExtractor);
            eh.Expect(z => z.CreateUserpicTuple(Arg<EntryBase>.Is.Anything))
                .Return(ta);

            // Should return only single userpic as their URL are the same.
            Tuple<string, Userpic>[] result = eh.GetUserpics(new EntryBase[] {null, null});
            Assert.AreEqual(1, result.Length);

            eh.VerifyAllExpectations();
        }
    }
}