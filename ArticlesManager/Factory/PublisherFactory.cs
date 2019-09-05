using System;
using System.Collections.Generic;
using System.Text;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;

namespace ArticlesManager.Factory
{
    public class PublisherFactory
    {
        
        public PublisherArticlesDTO GetDataObject(int min=20, int max=50)
        {
            var dto = new PublisherArticlesDTO
            {
                Publisher = new Publisher {Id = Guid.NewGuid(), PublisherName = RandomString(5)},
                Articles = new List<Article>()
            };
            var articleFactory = new ArticleFactory();
            for (var i = 0; i < new Random().Next(min, max); i++) dto.Articles.Add(articleFactory.GenerateArticle(dto.Publisher.Id));
            return dto;
        }

        private static string RandomString(int size=1, bool lowerCase = false)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        private static DateTime RandomDateTime(DateTime start, DateTime end)
        {
            var range = (end - start).Days;
            return start.AddDays(new Random().Next(range));
        }
    }
}