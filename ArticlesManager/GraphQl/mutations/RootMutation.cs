using System.Collections.Generic;
using ArticlesManager.GraphQl.types.models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(IEnumerable<ISchemaGroup> mutations)
        {
            Name = "Mutations";
            foreach (var mutation in mutations) mutation.SetGroup(this);
        }
    }
}