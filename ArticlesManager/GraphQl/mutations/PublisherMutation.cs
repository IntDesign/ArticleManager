using System;
using ArticlesManager.GraphQl.types.input;
using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Models;
using ArticlesManager.Repositories;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.mutations
{
    public class PublisherMutation : ObjectGraphType
    {
        public PublisherMutation(PublisherRepository repo)
        {
            FieldAsync<PublisherType>(
                name: "createPublisher",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PublisherTypeInput>>
                {
                    Name = "publisher"
                }),
                resolve: async context =>
                {
                    var publisher = context.GetArgument<Publisher>("publisher");
                    return await context.TryAsyncResolve(
                        async c => await repo.AddPublisher(publisher)
                    );
                }
            );

            FieldAsync<PublisherType>(
            
                "deletePublisher",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
                {
                    Name = "publisherId"
                }),
                resolve: async context =>
                {
                    var id = context.GetArgument<string>("publisherId");
                    return await context.TryAsyncResolve(
                            async c => await repo.RemovePublisher(Guid.Parse((ReadOnlySpan<char>) id))
                    );
                }
            );
        }
    }
}