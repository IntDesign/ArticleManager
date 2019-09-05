using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.types.input
{
    public class PublisherTypeInput : InputObjectGraphType<Publisher>
    {
        public PublisherTypeInput()
        {
            Field(t => t.PublisherName, false, typeof(NonNullGraphType<StringGraphType>))
                .Description("The Publisher name");
        }
    }
}