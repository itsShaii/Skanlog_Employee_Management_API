﻿namespace Skanlog_Employee_Management_API.DTO
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string SSSNumber { get; set; }
        public string PagIbigNumber { get; set; }
    }
}
