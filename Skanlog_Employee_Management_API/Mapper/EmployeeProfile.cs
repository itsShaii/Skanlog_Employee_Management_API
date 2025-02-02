using Microsoft.EntityFrameworkCore.Infrastructure;
using Skanlog_Employee_Management_API.Model;
using Skanlog_Employee_Management_API.DTO;
using AutoMapper;

namespace Skanlog_Employee_Management_API.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
