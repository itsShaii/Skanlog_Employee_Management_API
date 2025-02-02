using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skanlog_Employee_Management_API.Controllers;
using Skanlog_Employee_Management_API.DataAccess;
using Skanlog_Employee_Management_API.DTO;
using Skanlog_Employee_Management_API.Model;

namespace Skanlog_Employee_Management_API.Logic
{
    public class EmployeeLogicAndDbCalls
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IMapper _mapper;

        public EmployeeLogicAndDbCalls(EmployeeDbContext employeeDbContext, IMapper mapper)
        {
            _employeeDbContext = employeeDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _employeeDbContext.Employees
                .Where(e => !e.IsDeleted)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO?> GetEmployeeById(Guid id)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            return employee == null ? null : _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<EmployeeDTO?> CreateEmployee(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);

            _employeeDbContext.Employees.Add(employee);
            await _employeeDbContext.SaveChangesAsync();

            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<bool> UpdateEmployee(Guid id, EmployeeDTO employeeDTO)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            if (employee == null) return false;

            _mapper.Map(employeeDTO, employee);
            await _employeeDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteEmployee(Guid id)
        {
            var employee = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) return false;

            employee.IsDeleted = true;
            await _employeeDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckDuplicateEmail (Guid? id, string email)
        {
            var duplicateEmail = await _employeeDbContext.Employees
                .AnyAsync(e => e.Email == email && !e.IsDeleted && (id == null || e.Id != id));

            return duplicateEmail;
        }
    }
}
