namespace GraphqlApi.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public UserDto User { get; set; }
        public string UserId { get; set; }
    }
}