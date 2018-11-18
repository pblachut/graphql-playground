using GraphqlApi.Dtos;
using GraphqlApi.SampleDomain;
using GraphQL.Types;
using Raven.Client.Documents;

namespace GraphqlApi
{
    public class IdentitySchemaQuery : ObjectGraphType<object>
    {
        public IdentitySchemaQuery(IDocumentStore documentStore)
        {
            Name = "Query";

            Field<EmployeeObjectType>(
                "employee",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id"}
                ),
                resolve: context => documentStore.GetEmployee(context.GetArgument<string>("id"))
            );
            
            Field<UserObjectType>(
                "user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id"}
                ),
                resolve: context => documentStore.GetUser(context.GetArgument<string>("id"))
            );

        }
    }
}