using ArticlesManager.GraphQl.mutations;
using ArticlesManager.GraphQl.queries;
using GraphQL;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.schemas
{
    public class RootSchema : Schema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
        }
    }
}