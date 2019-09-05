using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesManager.Contexts;
using ArticlesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticlesManager.Controllers
{

    [Route("fromctrl/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly ArticlesContext _context;
        
        public ArticlesController(ArticlesContext context) => _context = context;

        [HttpPost] public async Task AddArticles([FromBody] Article article)
        {
            await _context.Articles.AddAsync(article);
            var publishers = await _context.Publishers.ToListAsync();
            if (publishers.Find(p => p.PublisherName == article.Publisher.PublisherName) == null)
                await _context.Publishers.AddAsync(article.Publisher);
            await _context.SaveChangesAsync();
        }
        
        [HttpGet] public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }
    }
}