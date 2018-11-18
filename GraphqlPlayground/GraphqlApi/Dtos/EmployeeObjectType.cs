using GraphqlApi.SampleDomain;
using GraphQL.Types;
using Raven.Client.Documents;

namespace GraphqlApi.Dtos
{
    public class EmployeeObjectType: ObjectGraphType<EmployeeDto>
    {
        public EmployeeObjectType(IDocumentStore documentStore)
        {
            Name = "Employee";
            
            Field(u => u.Id);
            Field(u => u.EmployeeName);
            
            Field<UserObjectType>(
                "user",
                resolve: context => documentStore.GetUser(context.Source)
            );
        }

        
    }
}