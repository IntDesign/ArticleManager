using System;
using ArticlesManager.GraphQl.types.input;
using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Models;
using ArticlesManager.Repositories;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.mutations
{
    public class ArticleMutation : ObjectGraphType
    {
        public ArticleMutation(ArticlesRepository repo)
        {
            FieldAsync<ArticleType>(
                "createArticle",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ArticleTypeInput>>
                {
                    Name = "article"
                }),
                resolve: async context =>
                {
                    var article = context.GetArgument<Article>("article");
                    return await context.TryAsyncResolve(
                        async c => await repo.AddArticle(article));
                }
            );
            FieldAsync<ArticleType>(
                "removeArticle",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
                {
                    Name = "id"
                }),
                resolve: async context =>
                {
                    var id = context.GetArgument<string>("id");
                    return await context.TryAsyncResolve(
                        async _ => await repo.RemoveArticle(Guid.Parse((ReadOnlySpan<char>) id))
                    );
                }
            );
        }
    }
}