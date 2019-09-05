using System;
using ArticlesManager.Contexts;
using ArticlesManager.Controllers;
using ArticlesManager.Factory;
using ArticlesManager.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ArticlesManager.Tests
{
    public class ArticleManagerArticleControllerTest
    {
        private readonly DbContextOptions<ArticlesContext> options;
        public ArticleManagerArticleControllerTest()
        {
            options = new DbContextOptionsBuilder<ArticlesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;   
            
        }
        
        [Fact] public async void TestSuccesfullAddArticles()
        {
            var articleContext = new ArticlesContext(options);
            var controller = new ArticlesController(articleContext);
            var articleFactory = new ArticleFactory();
            var publisherFactory = new PublisherFactory();
            var pub = publisherFactory.GetDataObject(0, 0).Publisher;
            await articleContext.Publishers.AddAsync(pub);
            await controller.AddArticles(articleFactory.GenerateArticle(pub.Id));
            var articles = await controller.GetArticles();
            Assert.True(await articleContext.Publishers.CountAsync() == 1);
            Assert.True(articles.Value.Count == 1);
        }
    }
}