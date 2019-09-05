using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArticlesManager.Contexts;
using ArticlesManager.Factory;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ArticlesManager.Controllers
{
    [Route("[controller]")]
    public class DataController : Controller
    {
        private readonly ArticlesContext _context;
        private readonly ArticlesPublishersDTO _articlesPublishersDto;
        
        public DataController(ArticlesContext context)
        {
            _context = context;
            _articlesPublishersDto = new ArticlesPublishersDTO();
        }

        [HttpGet("/generate/god")]
        public async Task GenerateGod()
        {
            var Factory = new PublisherFactory();
            var dto = Factory.GetDataObject(50000,60000);
            dto.Publisher.PublisherName = "GOD";
            await _context.Publishers.AddAsync(dto.Publisher);
            foreach (var article in dto.Articles) await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        [HttpGet("generate/{nr}")]
        public async Task GenerateData(int nr)
        {
            var Factory = new PublisherFactory();
            for (var i = 0; i < nr; i++)
            {
                var dto = Factory.GetDataObject();
                await _context.Publishers.AddAsync(dto.Publisher);
                foreach (var article in dto.Articles) await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
            }
        }
        
        
        [HttpGet("json")]
        public async Task LoadDataFromJson()
        {          
            _articlesPublishersDto.PublisherArticles = new List<Article>();
            await LoadJson();
            var publisher = await _context.Publishers
                .Where(p => p.PublisherName == _articlesPublishersDto.Publisher.PublisherName).FirstOrDefaultAsync();
            if (publisher == null)
            {
                await _context.Publishers.AddAsync(_articlesPublishersDto.Publisher);
                await _context.SaveChangesAsync();
                publisher = await _context.Publishers
                    .Where(p => p.PublisherName == _articlesPublishersDto.Publisher.PublisherName)
                    .FirstOrDefaultAsync();
            }

            foreach (var article in _articlesPublishersDto.PublisherArticles)
            {
                article.PublisherId = publisher.Id;
                if(await _context.Articles.Where(a => a.Url == article.Url).FirstOrDefaultAsync() == null) 
                    await _context.Articles.AddAsync(article);
            }
            await _context.SaveChangesAsync();
        }

        private async Task LoadJson()
        {
            using (var r = new StreamReader("jsons/DataFile1.json"))
            {
                var json = await r.ReadToEndAsync();
                var jPublisher = JObject.Parse(json);
                _articlesPublishersDto.Publisher = new Publisher
                {
                    PublisherName = jPublisher["PublisherName"].ToString()
                };
                foreach (var article in jPublisher["Articles"])
                {
                    ArticleTypesEnum categ; 
                    switch (article["Category"].ToString())
                    {
                        case "BUSINESS" : categ = ArticleTypesEnum.Bussiness; break;
                        case "WORLD": categ = ArticleTypesEnum.WorldNews; break;
                        case "SPORTS": categ = ArticleTypesEnum.Sport; break;
                        case "POLITICS": categ = ArticleTypesEnum.Politics; break;
                        default: throw new Exception("Invalid Category in Data File  " +  article["Category"]);
                    }
                    _articlesPublishersDto.PublisherArticles.Add(new Article
                    {
                        Url = article["Url"].ToString(),
                        Title = article["Title"].ToString(),
                        Author = article["Author"].ToString(),
                        PublicationDate = DateTime.Parse(article["PublicationDate"].ToString()),
                        Category = categ
                    });
                }
            }
        }
    }
}

class ArticlesPublishersDTO
{
    internal Publisher Publisher { get; set; }
    
    internal List<Article> PublisherArticles { get; set; }
}