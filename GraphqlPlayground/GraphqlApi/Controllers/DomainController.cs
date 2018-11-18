using System.Threading.Tasks;
using GraphqlApi.SampleDomain;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace GraphqlApi.Controllers
{
    [ApiController]
    public class DomainController: ControllerBase
    {
        private readonly IDocumentStore _documentStore;

        public DomainController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }
        
        [HttpPost]
        [Route("api/users")]
        public async Task CreateUser([FromBody] User user)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                await session.StoreAsync(user);
                await session.SaveChangesAsync();
            }
        }
        
        [HttpPost]
        [Route("api/employees")]
        public async Task CreateEmployee([FromBody] Employee employee)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                await session.StoreAsync(employee);
                await session.SaveChangesAsync();
            }
        }
    }
}