
using EmployeeAPI.Models;

namespace EmployeeAPI.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;

        private static List<Employee> employees = new List<Employee>{
                new Employee { Id = 1, Name = "Alvaro", Surname = "Page", BirthDate = new DateOnly(1987,1,28), Department = "Modern Workplace"},
                new Employee { Id = 2, Name = "María Asunción", Surname = "Sánchez", BirthDate = new DateOnly(1981,1,21), Department = "Web development"}
            };

        public EmployeeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            serviceResponse.Data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            var employee = employees.Find(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetEmployeeDto>(employee);
            if(employee == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Employee not found";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            employees.Add(_mapper.Map<Employee>(newEmployee));
            serviceResponse.Data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = employees.FirstOrDefault(x => x.Id == id);
                if (employee is null)
                {
                    throw new Exception($"Employee with ID '{id.ToString()}' not found.");
                }
               
                employee.Name = updatedEmployee.Name;
                employee.Surname = updatedEmployee.Surname;
                employee.BirthDate = updatedEmployee.BirthDate;
                employee.Department = updatedEmployee.Department;
                serviceResponse.Data = _mapper.Map<GetEmployeeDto>(employee);
                serviceResponse.Message = "Employee has been updated";     
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();

            try
            {
                var employee = employees.FirstOrDefault(x => x.Id == id);
                if (employee is null)
                {
                    throw new Exception($"Employee with ID '{id.ToString()}' not found.");
                }

                employees.Remove(employee);
                serviceResponse.Data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
                serviceResponse.Message = "Employee has been deleted";
      
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
