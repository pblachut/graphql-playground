using GraphqlApi.SampleDomain;
using GraphQL.Types;
using Raven.Client.Documents;

namespace GraphqlApi.Dtos
{
    public class UserObjectType : ObjectGraphType<UserDto>
    {
        public UserObjectType(IDocumentStore documentStore)
        {
            Name = "User";

            Field(u => u.Id);
            Field(u => u.UserName);
            
            Field<ListGraphType<EmployeeObjectType>>(
                "employees",
                resolve: context => documentStore.GetEmployees(context.Source)
            );
        }

        
        
    }
}