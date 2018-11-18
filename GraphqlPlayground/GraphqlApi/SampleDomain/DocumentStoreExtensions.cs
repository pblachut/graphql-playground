using System.Collections.Generic;
using System.Linq;
using GraphqlApi.Dtos;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace GraphqlApi.SampleDomain
{
    public static class DocumentStoreExtensions
    {
        public static List<EmployeeDto> GetEmployees(this IDocumentStore documentStore, UserDto user)
        {
            if (user == null)
                return null;
            
            using (var session = documentStore.OpenSession())
            {
                return session.GetEmployees(user.Id);
            }
        }

        public static List<EmployeeDto> GetEmployees(this IDocumentSession session, string userId)
        {
            return session.Query<EmployeeQueryIndex.Result, EmployeeQueryIndex>()
                .Where(c => c.UserId == userId)
                .Skip(0)
                .Take(100)
                .OfType<EmployeeDto>()
                .ToList();
        }
        
        public static UserDto GetUser(this IDocumentStore documentStore, EmployeeDto employee)
        {
            if (employee == null)
                return null;
            
            using (var session = documentStore.OpenSession())
            {
                return session.GetUser(employee.UserId);
            }
        }
        
        public static UserDto GetUser(this IDocumentStore documentStore, string userId)
        {
            if (userId == null)
                return null;
            
            using (var session = documentStore.OpenSession())
            {
                var user = session.GetUser(userId);

                user.Employees = session.GetEmployees(userId).ToArray();

                return user;
            }
        }

        public static UserDto GetUser(this IDocumentSession session, string userId)
        {
            var user = session.Load<User>(userId);

            return new UserDto
            {
                Id = user.Id,
                UserName = user.Name,
            };
        }
        
        public static EmployeeDto GetEmployee(this IDocumentStore documentStore, string employeeId)
        {
            if (employeeId == null)
                return null;
            
            using (var session = documentStore.OpenSession())
            {
                var employee = session.Load<Employee>(employeeId);

                return new EmployeeDto
                {
                    Id = employee.Id,
                    EmployeeName = employee.Name,
                    UserId = employee.UserId,
                    User = session.GetUser(employee.UserId)
                };
            }
        }
    }
}