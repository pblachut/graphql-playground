using System.Linq;
using Raven.Client.Documents.Indexes;

namespace GraphqlApi.SampleDomain
{
    public class EmployeeQueryIndex : AbstractIndexCreationTask<Employee>
    {
        public class Result
        {
            public string UserId { get; set; }
            public string Name { get; set; }
        }
        
        public EmployeeQueryIndex()
        {
            Map = employees => from employee in employees
                select new 
                {
                    UserId = employee.UserId,
                    Name = employee.Name
                };
        }
    }
}