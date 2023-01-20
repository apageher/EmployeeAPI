namespace EmployeeAPI.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private static List<Employee> employees = new List<Employee>{
                new Employee { Id = 1, Name = "Alvaro", Surname = "Page", BirthDate = new DateOnly(1987,1,28), Department = "Modern Workplace"},
                new Employee { Id = 2, Name = "María Asunción", Surname = "Sánchez", BirthDate = new DateOnly(1981,1,21), Department = "Web development"}
            };

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public Employee? GetEmployeeById(int id)
        {
            var employee = employees.Find(x => x.Id == id);
            if (employee == null)
                return null;
            return employee;
        }

        public List<Employee> AddEmployee(Employee newEmployee)
        {
            employees.Add(newEmployee);
            return employees;
        }

        public List<Employee>? UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = employees.Find(x => x.Id == id);
            if (employee == null)
                return null;

            employee.Name = updatedEmployee.Name;
            employee.Surname = updatedEmployee.Surname;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.Department = updatedEmployee.Department;

            return employees;
        }

        public List<Employee>? DeleteEmployee(int id)
        {
            var employee = employees.Find(x => x.Id == id);
            if (employee == null)
                return null;

            employees.Remove(employee);

            return employees;
        }
    }
}
