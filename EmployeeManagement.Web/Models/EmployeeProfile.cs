using AutoMapper;
using EmployeeManagement.Models;

namespace EmployeeManagement.Web.Models
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EditEmployeeModel>().ForMember((target) => target.ConfirmEmail, (opt) => opt.MapFrom((source) => source.Email));
            CreateMap<EditEmployeeModel, Employee>();
        }
    }
}
