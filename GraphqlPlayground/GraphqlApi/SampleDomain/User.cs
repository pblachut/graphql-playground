namespace GraphqlApi.SampleDomain
{
    public class User
    {
        public const string CollectionPrefix = "users/";
        
        public string Id { get; set; }
        public string Name { get; set; }
    }
}