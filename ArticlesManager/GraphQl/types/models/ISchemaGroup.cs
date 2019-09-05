using ArticlesManager.GraphQl.mutations;
using ArticlesManager.GraphQl.queries;
using ArticlesManager.GraphQl.schemas;

namespace ArticlesManager.GraphQl.types.models
{
    public interface ISchemaGroup
    {
        void SetGroup(RootQuery query);

        void SetGroup(RootMutation mutation);
    }
}