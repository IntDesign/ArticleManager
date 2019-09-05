using System.Collections.Generic;
using ArticlesManager.Models;

namespace ArticlesManager.Factory
{
    public class PublisherArticlesDTO
    {
        public Publisher Publisher { get; set; }
        
        public List<Article> Articles { get; set; }
    }
}