namespace EmployeeAPI.Dtos.Employee
{
    public class AddEmployeeDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}
