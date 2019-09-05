using System;
using System.Collections.Generic;

namespace ArticlesManager.Models
{
    public class Publisher
    {
        public Guid Id { get; set; }
        
        public string PublisherName { get; set; }
        public ICollection<Article> PublisherArticles { get; set; }
    }
}