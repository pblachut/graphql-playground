namespace GraphqlApi.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public EmployeeDto[] Employees { get; set; }
    }
}