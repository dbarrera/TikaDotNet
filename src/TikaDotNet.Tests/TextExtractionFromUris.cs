using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TikaDotNet.TextExtraction;

namespace TikaDotNet.Tests
{
    [TestClass]
    public class TextExtractionFromUris
    {
        private readonly TextExtractor _cut;

        public TextExtractionFromUris()
        {
            _cut = new TextExtractor();
        }

        [TestMethod]
        public void ShouldExtractFromRtf()
        {
            var result = ExtractFromCdn(@"https://jeroen.github.io/files/sample.rtf");

            result.Text.Should().Contain("It is an example test rtf-file to RTF2XML bean for testing");
            result.Text.Should().Contain("Bold text.");
            result.Text.Should().Contain("öt árvíztűrő ütvefúrógép");
        }

        [TestMethod]
        public void ShouldExtractFromJpg()
        {
            var result = ExtractFromCdn(@"https://cdn.staticaly.com/img/i.imgur.com/3QrPToe.jpg");

            result.Text.Trim().Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldExtractFromPDF()
        {
            var result = ExtractFromCdn(@"http://www.africau.edu/images/default/sample.pdf");

            result.Text.Should().Contain("A Simple PDF File");
            result.Text.Should().Contain("And more text. And more text.");

            result.Text.Should().Contain("Simple PDF File 2");
            result.Text.Should().Contain("...continued from page 1.");
        }

        [TestMethod]
        public void ShouldExtractFromDocx()
        {
            var result = ExtractFromCdn(@"https://calibre-ebook.com/downloads/demos/demo.docx");

            result.Text.Should().Contain("Demonstration of DOCX support in calibre");

            result.Text.Should().Contain("One, with a very long line to demonstrate that the hanging indent for the");
        }

        [TestMethod]
        public void ShouldExtractFromPptx()
        {
            var result = ExtractFromCdn(@"https://scholar.harvard.edu/files/torman_personal/files/samplepptx.pptx");

            result.Text.Should().Contain("Sample PowerPoint File");

            result.Text.Should().Contain("Here is an outline of bulleted points");
        }

        private TextExtractionResult ExtractFromCdn(string cdn)
        {
            var textExtractResult = _cut.Extract(new Uri(cdn));

            return textExtractResult;
        }
    }
}
