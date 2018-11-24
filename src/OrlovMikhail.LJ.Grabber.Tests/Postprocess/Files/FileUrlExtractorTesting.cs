using System;
using NUnit.Framework;
using OrlovMikhail.LJ.Grabber.Helpers;

namespace OrlovMikhail.LJ.Grabber.Postprocess.Files
{
    [TestFixture]
    public class FileUrlExtractorTesting
    {
        private string _html;
        private IFileUrlExtractor _ex;

        private const string Url1 = @"http://www.samisdat.com/picture/LJ3/3409.jpg";
        private const string Url2 = @"http://www.samisdat.com/picture/LJ3/3423.jpg";

        [SetUp]
        public void LoadHtml()
        {
            string content = TestingShared.GetFileContent("testpage_247911.xml");

            LayerParser.LayerParser p = new LayerParser.LayerParser();
            _html = p.ParseAsAnEntryPage(content).Entry.Text;
            _ex = new FileUrlExtractor();
        }

        [Test]
        public void ParsesFromContent()
        {
            string[] urls = _ex.GetImagesURLs(_html);

            Assert.IsTrue(urls.Length > 5);
            CollectionAssert.Contains(urls, Url1);
            CollectionAssert.Contains(urls, Url2);
        }

        [Test]
        public void ProvidesUrlsForReplacing()
        {
            int called = 0;
            Func<string, string> matcher = s =>
            {
                called++;
                if(s == Url1)
                    return "ABC";
                else if(s == Url2)
                    return null;
                else
                    return s;
            };

            string result = _ex.ReplaceFileUrls(_html, matcher);
            Assert.IsFalse(result.Contains(Url1));
            Assert.IsTrue(result.Contains(Url2));
            Assert.IsTrue(result.Contains("<img src=\"ABC\""));
        }
    }
}
