using GraphQL;
using GraphQL.Types;

namespace GraphqlApi
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<StarWarsQuery>();
        }
    }
}
