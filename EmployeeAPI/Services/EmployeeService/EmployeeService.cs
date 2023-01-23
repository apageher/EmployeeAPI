
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployeeService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployees()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var dbEmployees = await _context.Employees.ToListAsync();
            serviceResponse.Data = dbEmployees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeById(int id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
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
            _context.Employees.Add(_mapper.Map<Employee>(newEmployee));
            await _context.SaveChangesAsync();
            var dbEmployees = await _context.Employees.ToListAsync();
            serviceResponse.Data = dbEmployees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee is null)
                {
                    throw new Exception($"Employee with ID '{id.ToString()}' not found.");
                }
               
                employee.Name = updatedEmployee.Name;
                employee.Surname = updatedEmployee.Surname;
                employee.BirthDate = updatedEmployee.BirthDate;
                employee.Department = updatedEmployee.Department;

                await _context.SaveChangesAsync();

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
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee is null)
                {
                    throw new Exception($"Employee with ID '{id.ToString()}' not found.");
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                var dbEmployees = await _context.Employees.ToListAsync();
                serviceResponse.Data = dbEmployees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
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
