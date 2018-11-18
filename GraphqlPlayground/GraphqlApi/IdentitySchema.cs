using GraphQL;
using GraphQL.Types;

namespace GraphqlApi
{
    public class IdentitySchema: Schema
    {
        public IdentitySchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<IdentitySchemaQuery>();
        }
    }
}