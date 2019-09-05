using ArticlesManager.GraphQl.mutations;
using ArticlesManager.GraphQl.queries;
using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Repositories;

namespace ArticlesManager.GraphQl.schemas.schemaGroups
{
    public class ArticleSchema : ISchemaGroup
    {
        private readonly ArticlesRepository _repo;


        public ArticleSchema(ArticlesRepository repository)
        {
            _repo = repository;
        }
        public void SetGroup(RootQuery query)
        {
            query.Field<ArticleQuery>(
                name:"articles",
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<ArticleMutation>(
                "articles",
                resolve: context => new { });
        }
    }
}