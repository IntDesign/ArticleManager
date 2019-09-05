using System.Collections.Generic;
using ArticlesManager.GraphQl.types.models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IEnumerable<ISchemaGroup> queries)
        {
            Name = "Query";
            foreach (var query in queries) query.SetGroup(this);
        }
    }
    
}