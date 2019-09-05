using System;
using ArticlesManager.GraphQl.types.models;

namespace ArticlesManager.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        
        public string Url { get; set; }
        
        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public DateTime PublicationDate { get; set; }
        
        
        public ArticleTypesEnum Category{ get; set; }
        
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}