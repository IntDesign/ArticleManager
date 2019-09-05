using GraphQL.Types;

namespace ArticlesManager.GraphQl.types.models
{
    public enum ArticleTypesEnum
    {
        Bussiness=0,
        Sport=1,
        Politics=2,
        WorldNews=3
    }

    public class ArticlesTypeEnumType : EnumerationGraphType<ArticleTypesEnum>
    {
        public ArticlesTypeEnumType()
        {
            Name = "Type";
            Description = "Type of article";
        }
    }
}