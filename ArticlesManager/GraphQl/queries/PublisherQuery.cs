using System;
using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Repositories;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.queries
{
    public class PublisherQuery : ObjectGraphType
    {
        public PublisherQuery(PublisherRepository repository)
        {
            FieldAsync<ListGraphType<PublisherType>>(
                name: "publishers",
                resolve: async context => await repository.GetAll()
            );
            FieldAsync<PublisherType>(
                name: "publisher",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                    Name = "Id",
                    Description = "The id of the publisher you want to return"
                }),
                resolve: async context =>
                {
                    var id = context.GetArgument<string>("Id");
                    return await repository.GetById(Guid.Parse((ReadOnlySpan<char>) id));
                }
            );
        }
    }
}