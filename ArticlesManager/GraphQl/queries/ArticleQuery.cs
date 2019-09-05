using System;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Repositories;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.queries
{
    public class ArticleQuery : ObjectGraphType
    {
        public ArticleQuery(ArticlesRepository repository)
        {
            FieldAsync<ListGraphType<ArticleType>>(
            "articles", 
             resolve: async context => await repository.GetAll()
            );
            FieldAsync<ArticleType>(
                name:"article",
                arguments:new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                  Name  = "id",
                  Description = "The id of the article you want to see"
                }),
                resolve: async context =>
                {
                    var id = context.GetArgument<string>("id");
                    return await context.TryAsyncResolve(async c => await repository.GetById(Guid.Parse((ReadOnlySpan<char>) id)));
                }
            );
            FieldAsync<ListGraphType<ArticleType>>(
                "recentArticlesByCategory",
                arguments:new QueryArguments(new QueryArgument<NonNullGraphType<ArticlesTypeEnumType>>
                {
                    Name = "category"
                }),
                resolve: async context =>
                {
                    var type = context.GetArgument<ArticleTypesEnum>("category");
                    return await context.TryAsyncResolve(
                        async c => await repository.GetLatestArticlesForCategory(type));
                }
            );
            FieldAsync<ListGraphType<ArticleType>>(
                "articlesForPublisherName",
                arguments:new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
                {
                   Name = "publisherName"
                }),
                resolve: async context =>
                {
                    var pub = context.GetArgument<string>("publisherName");
                    return await context.TryAsyncResolve(
                        async c => await repository.GetArticlesOfPublisher(pub)
                    );
                }
                );
        }
    }
}