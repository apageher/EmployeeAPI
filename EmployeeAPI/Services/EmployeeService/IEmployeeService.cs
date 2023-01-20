namespace EmployeeAPI.Services.EmployeeService
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee? GetEmployeeById(int id);
        List<Employee> AddEmployee(Employee newEmployee);
        List<Employee>? UpdateEmployee(int id, Employee updatedEmployee);
        List<Employee>? DeleteEmployee(int id);
    }
}
