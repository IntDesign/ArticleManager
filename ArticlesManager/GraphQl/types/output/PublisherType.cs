using ArticlesManager.Models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.types.output
{
    public class PublisherType : ObjectGraphType<Publisher>
    {
        public PublisherType()
        {
            Field(t => t.Id, false, typeof(StringGraphType)).Description("The Article Id, value AutoIncremented");
            Field(t => t.PublisherName, false).Description("The Publisher name");
            Field(t => t.PublisherArticles, false, typeof(ListGraphType<ArticleType>))
                .Description("The articles of the publisher");
        }
    }
}