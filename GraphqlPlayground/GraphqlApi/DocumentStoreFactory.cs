using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace GraphqlApi
{
    public static class DocumentStoreFactory
    {
        public static IDocumentStore Create(string url, string database)
            => new DocumentStore
            {
                Urls = new[] {url},
                Database = database
            };

        public static void EnsureDatabaseExist(this IDocumentStore documentStore)
        {
            if (documentStore.Maintenance.Server.Send(new GetDatabaseRecordOperation(documentStore.Database)) == null)
                documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(documentStore.Database)));
        }
    }
}