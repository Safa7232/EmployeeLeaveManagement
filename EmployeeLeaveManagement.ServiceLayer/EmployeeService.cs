using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveManagement.DomainModels;
using EmployeeLeaveManagement.ViewModels;
using EmployeeLeaveManagement.Repositories;
using AutoMapper;
using AutoMapper.Configuration;


namespace EmployeeLeaveManagement.ServiceLayer
{
    public interface IEmployeeService
    {
        int InsertEmployee(RegisterViewModel regViewModel);
        void UpdateEmployeeDetails(EditEmployeeDetailsViewModel employeeViewModel);
        void UpdateEmployeePassword(EditEmployeePasswordViewModel employeeViewModel);
        void DeleteUser(int empId);
       
        List<EmployeeViewModel> GetEmployee();
        EmployeeViewModel GetEmploeesByEmailAndPassword(string Email, string Password);
        EmployeeViewModel GetEmployeeByEmail(string Email);
        EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID);

        EmployeeViewModel GetEmployeesByRoleID(int EmployeeID);
    }
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository empRep;

        public EmployeeService()
        {
            empRep = new EmployeeRepository();
        }
        public int InsertEmployee(RegisterViewModel regViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee emp = mapper.Map<RegisterViewModel, Employee>(regViewModel);
            emp.PasswordHash = SHA256HashGenerator.GenerateHash(regViewModel.Password);
            empRep.InsertEmployee(emp);
            int empID = empRep.GetLatestEmployeeID();
            return empID;
        }

        public void UpdateEmployeeDetails(EditEmployeeDetailsViewModel employeeViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeeDetailsViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee emp = mapper.Map<EditEmployeeDetailsViewModel, Employee>(employeeViewModel);
            empRep.UpdateEmployeeDetails(emp);

        }

       

        public void UpdateEmployeePassword(EditEmployeePasswordViewModel employeeViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditEmployeePasswordViewModel, Employee>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Employee emp = mapper.Map<EditEmployeePasswordViewModel, Employee>(employeeViewModel);
            emp.PasswordHash = SHA256HashGenerator.GenerateHash(employeeViewModel.Password);
            empRep.UpdateEmployeePassword(emp);
        }

        public void DeleteUser(int empId)
        {
            empRep.DeleteEmployee(empId);
        }

        public List<EmployeeViewModel> GetEmployee()
        {
            List<Employee> emp = empRep.GetEmployee();
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Question, QuestionViewModel>();
            //    cfg.CreateMap<User, UserViewModel>();
            //    cfg.CreateMap<Category, CategoryViewModel>();
            //    cfg.CreateMap<Answer, AnswerViewModel>();
            //    cfg.CreateMap<Vote, VoteViewModel>();
            //    cfg.IgnoreUnmapped();
            //});
            var config= new MapperConfiguration(cfg => { cfg.CreateMap<Employee,EmployeeViewModel > (); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<EmployeeViewModel> employeeViewModel = mapper.Map<List<Employee>, List<EmployeeViewModel>>(emp);
            return employeeViewModel;
        }

        public EmployeeViewModel GetEmploeesByEmailAndPassword(string Email, string Password)
        {
            Employee emp = empRep.GetEmployeesByEmailAndPassword (Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            EmployeeViewModel employeeViewModel = null;
            if (emp != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                employeeViewModel = mapper.Map<Employee, EmployeeViewModel>(emp);
            }
            return employeeViewModel;
        }

        public EmployeeViewModel GetEmployeeByEmail(string Email)
        {
            Employee emp = empRep.GetEmployeesByEmail(Email).FirstOrDefault();
            EmployeeViewModel employeeViewModel = null;
            if (emp != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                employeeViewModel = mapper.Map<Employee, EmployeeViewModel>(emp);
            }
            return employeeViewModel;
        }

        public EmployeeViewModel GetEmployeesByEmployeeID(int EmployeeID)
        {
            Employee emp = empRep.GetEmployeesByEmployeeID(EmployeeID).FirstOrDefault();
            EmployeeViewModel employeeViewModel = null;
            if (emp!= null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                employeeViewModel = mapper.Map<Employee, EmployeeViewModel>(emp);
            }

            return employeeViewModel;
        }

        public EmployeeViewModel GetEmployeesByRoleID(int RoleID)
        {
            Employee emp = empRep.GetEmployeesByRoleID(RoleID).FirstOrDefault();
            EmployeeViewModel employeeViewModel = null;
            if (emp != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, EmployeeViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                employeeViewModel = mapper.Map<Employee, EmployeeViewModel>(emp);
            }

            return employeeViewModel;
        }
    }
}
