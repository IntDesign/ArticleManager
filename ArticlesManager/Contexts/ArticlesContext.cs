using System;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesManager.Contexts
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions opt) : base(opt) {}

        public DbSet<Article> Articles { get; set; }
        
        public DbSet<Publisher> Publishers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured) return;
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CS") ?? throw new Exception("Invalid CS"));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>().HasKey(a => a.Id);
            
            builder.Entity<Article>().Property(a => a.Category)
                .HasConversion(
                    t => t.ToString(),
                    t => (ArticleTypesEnum) Enum.Parse(typeof(ArticleTypesEnum), t))
                .IsUnicode(false);
            
            builder.Entity<Article>().HasIndex(a => a.PublisherId);
            
            
            builder.Entity<Article>()
                .HasOne(a=> a.Publisher)
                .WithMany(p => p.PublisherArticles)
                .HasForeignKey(a => a.PublisherId)
                .IsRequired();

            
            builder.Entity<Publisher>().HasKey(p => p.Id);
            
            builder.Entity<Publisher>().HasIndex(p => p.Id);

            builder.Entity<Publisher>().HasIndex(p => p.PublisherName);
            
            base.OnModelCreating(builder);
        }
    }
}