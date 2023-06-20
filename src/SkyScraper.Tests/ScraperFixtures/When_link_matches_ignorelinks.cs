using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace SkyScraper.Tests.ScraperFixtures
{
    [TestFixture]
    class When_link_matches_ignorelinks : ConcernForScraper
    {
        readonly List<HtmlDoc> htmlDocs = new List<HtmlDoc>();
        string page;
        IObserver<HtmlDoc> observer; 

        protected override Scraper CreateClassUnderTest()
        {
            SUT = base.CreateClassUnderTest();
            observer = Substitute.For<IObserver<HtmlDoc>>();
            SUT.Subscribe(observer);
            SUT.IgnoreLinks = new Regex(@"link[\d]");
            return SUT;
        }

        protected override void Context()
        {
            base.Context();
            Uri = new Uri("http://test");
            page = @"<html>
                    <a href=""link1"">link1</a>
                    <a href=""link2"">link2</a>
                    </html>";
            HttpClient.GetString(Uri).Returns(Task.Factory.StartNew(() => page));
            HttpClient.GetString(Arg.Is<Uri>(x => x != Uri)).Returns(x => Task.Factory.StartNew(() => x.Arg<Uri>().PathAndQuery));
            OnNext = x => htmlDocs.Add(x);
        }

        [Test]
        public void Then_observer_should_be_notified_of_one_link()
        {
            observer.Received(1).OnNext(Arg.Any<HtmlDoc>());
        }

        [Test]
        public void Then_one_htmldoc_should_be_returned()
        {
            htmlDocs.Count.Should().Be(1);
        }
    }
}