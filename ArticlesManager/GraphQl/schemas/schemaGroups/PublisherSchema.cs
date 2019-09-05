using ArticlesManager.GraphQl.mutations;
using ArticlesManager.GraphQl.queries;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Repositories;

namespace ArticlesManager.GraphQl.schemas.schemaGroups
{
    public class PublisherSchema : ISchemaGroup
    {
        public void SetGroup(RootQuery query)
        {
            query.Field<PublisherQuery>(
                "publishers",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<PublisherMutation>(
                "publishers",
                resolve: context => new { });
        }
    }
}