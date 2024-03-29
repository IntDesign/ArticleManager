﻿// <auto-generated />
using System;
using ArticlesManager.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArticlesManager.Migrations
{
    [DbContext(typeof(ArticlesContext))]
    [Migration("20190730113405_Update2")]
    partial class Update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArticlesManager.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Category")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<DateTime>("PublicationDate");

                    b.Property<Guid>("PublisherId");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ArticlesManager.Models.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PublisherName");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PublisherName");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("ArticlesManager.Models.Article", b =>
                {
                    b.HasOne("ArticlesManager.Models.Publisher", "Publisher")
                        .WithMany("PublisherArticles")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
