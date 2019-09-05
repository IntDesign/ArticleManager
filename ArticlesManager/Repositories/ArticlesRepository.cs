using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesManager.Contexts;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesManager.Repositories
{
    public class ArticlesRepository
    {
        private readonly ArticlesContext _dbContext;

        public ArticlesRepository(ArticlesContext context) => _dbContext = context;

        public async Task<IEnumerable<Article>> GetAll()
            => await _dbContext.Articles.AsQueryable()
                .Include(a => a.Publisher)
                .ThenInclude(p => p.PublisherArticles)
                .ToListAsync();

        public async Task<Article> GetById(Guid id)
            => await _dbContext.Articles.Where(a => a.Id == id).Include(a => a.Publisher)
                .ThenInclude(p => p.PublisherArticles).FirstOrDefaultAsync();


        public async Task<Article> AddArticle(Article article)
        {
            article.PublicationDate = DateTime.Now;
            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
            return article;
        }

        public async Task<Article> RemoveArticle(Guid id)
        {
            var article = await _dbContext.Articles.Where(a => a.Id == id).FirstOrDefaultAsync();
            _dbContext.Remove(article);
            await _dbContext.SaveChangesAsync();
            return article;
        }


        public async Task<IEnumerable<Article>> GetLatestArticlesForCategory(ArticleTypesEnum type)
        {
            var articles = await _dbContext.Articles.Where(a => a.Category == type).ToListAsync();
            articles.Sort((a,b)=>a.PublicationDate.CompareTo(b.PublicationDate));
            articles.Reverse();
            return articles.Take(5);
        }

        public async Task<IEnumerable<Article>> GetArticlesOfPublisher(string publisher)
        {
            return await _dbContext.Articles.Where(a => a.Publisher.PublisherName == publisher)
                .Include(a => a.Publisher)
                .ToListAsync();
        }
    }
}