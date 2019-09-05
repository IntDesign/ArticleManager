using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesManager.Contexts;
using ArticlesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesManager.Repositories
{
    public class PublisherRepository
    {
        private readonly ArticlesContext _dbContext;
        public PublisherRepository(ArticlesContext context) => _dbContext = context;

        public async Task<IEnumerable<Publisher>> GetAll()
            => await _dbContext.Publishers.AsQueryable()
                .Include(p => p.PublisherArticles)
                .ToListAsync();

        public async Task<Publisher> GetById(Guid id)
            => await _dbContext.Publishers.Where(p => p.Id == id)
                .Include(p => p.PublisherArticles)
                .FirstOrDefaultAsync();

        public async Task<Publisher> AddPublisher(Publisher publisher)
        {
            await _dbContext.Publishers.AddAsync(publisher);
            await _dbContext.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> RemovePublisher(Guid id)
        {
            var pub = await _dbContext.Publishers.Where(p => p.Id == id).FirstOrDefaultAsync();
            var articles = await _dbContext.Articles.Where(a => a.PublisherId == id).ToListAsync();
            _dbContext.Publishers.Remove(pub);
            foreach (var a in articles) _dbContext.Articles.Remove(a);
            await _dbContext.SaveChangesAsync();
            return pub;
        }
    }
}