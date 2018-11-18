namespace GraphqlApi.SampleDomain
{
    public class Employee
    {
        public const string CollectionPrefix = "employees/";
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}