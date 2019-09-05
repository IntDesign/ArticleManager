using System;
using System.Text;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;

namespace ArticlesManager.Factory
{
    public class ArticleFactory
    {
        public Article GenerateArticle( Guid publisherId)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Url = $"www.{RandomString(8)}.com",
                Title = RandomString(10),
                Author = RandomString(6),
                PublicationDate = RandomDateTime(new DateTime(2010,1,1), DateTime.Today ),
                Category = (ArticleTypesEnum)new Random().Next(4),
                PublisherId = publisherId
            };
            return article;
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