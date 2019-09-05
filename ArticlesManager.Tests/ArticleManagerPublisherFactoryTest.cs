using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ArticlesManager.Factory;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace ArticlesManager.Tests
{
    public class ArticleManagerPublisherFactoryTest
    {
        [Fact] public void CorrectGeneratedPublisher()
        {
            var factory = new PublisherFactory();
            var publishers = new List<PublisherArticlesDTO>();
            for (var i = 0; i < 10; i++) publishers.Add(factory.GetDataObject());
            
            Assert.Equal(10, publishers.Count);
            Assert.Contains(publishers, dto => dto.Articles.Count > 20);
            Assert.Contains(publishers, dto => dto.Articles.Any(a => a.Url.Contains("www.") && a.Url.Contains(".com")));
            Assert.Contains(publishers, dto => dto.Articles.Any(a => (int)a.Category >= 0  && (int)a.Category <= 3));
        }
    }
}