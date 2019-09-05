using ArticlesManager.GraphQl.types.models;
using ArticlesManager.Models;
using GraphQL.Types;

namespace ArticlesManager.GraphQl.types.output
{
    public class ArticleType : ObjectGraphType<Article>
    {
        public ArticleType ()
        {
            Field(t => t.Id, false, typeof(StringGraphType)).Description("The article Id, value AutoIncremented");
            Field(t => t.Title, false).Description("The Article Title");
            Field(t => t.Author).Description("The Article Author");
            Field(t => t.PublicationDate, true).Description("The Article Publication Date");
            Field(t => t.Url).Description("The Article URL");
           
            Field<ArticlesTypeEnumType>("Category", "The type of Article");
            
            Field(t => t.Publisher,false, typeof(PublisherType))
                .Description("The publisher of the article");
        }
 
    }
}