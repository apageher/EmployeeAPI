﻿namespace EmployeeAPI.Dtos.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}