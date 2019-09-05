using ArticlesManager.GraphQl.types.models;
using ArticlesManager.GraphQl.types.output;
using ArticlesManager.Models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.types.input
{
    public class ArticleTypeInput : InputObjectGraphType<Article>
    {
        public ArticleTypeInput()
        {
            Field(t => t.Title, false, typeof(NonNullGraphType<StringGraphType>))
                .Description("The Article Title");
            Field(t => t.Author,false, typeof(NonNullGraphType<StringGraphType>))
                .Description("The Article Author");
            Field(t => t.PublicationDate,true,typeof(DateTimeGraphType))
                .Description("The Article Publication Date");
            Field(t => t.Url,false, typeof(NonNullGraphType<StringGraphType>))
                .Description("The Article URL");
            Field(t => t.Category, false, typeof(NonNullGraphType<ArticlesTypeEnumType>));
            Field(T => T.PublisherId, false, typeof(NonNullGraphType<StringGraphType>));
        }   
    }
}