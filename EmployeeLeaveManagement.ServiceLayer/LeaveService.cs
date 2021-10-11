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
    public interface ILeaveService
    {
        void InsertLeaveRequest(LeaveViewModel leaveRequest);
        MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel leaveRequest);

        List<LeaveViewModel> GetLeaves();
        List<LeaveViewModel> GetLeaveByEmployeeID(int EmployeeID);


    }
    public class LeaveService : ILeaveService
    {
        ILeaveRepository leaveRep;
        public LeaveService()
        {
            leaveRep = new LeaveRepository();
        }

        public void InsertLeaveRequest(LeaveViewModel leaveRequest)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave leave = mapper.Map<LeaveViewModel, Leave>(leaveRequest);
            leaveRep.InsertLeaveRequest(leave);

            
        }
        public MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel leaveRequest)
        {

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<LeaveViewModel, Leave>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Leave leave = mapper.Map<LeaveViewModel, Leave>(leaveRequest);
            Employee emp = leaveRep.UpdateLeaveStatusByLeaveID(leave);

            var config1 = new MapperConfiguration(cfg => { cfg.CreateMap<Employee, MailViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper1 = config1.CreateMapper();
            MailViewModel mvm = mapper1.Map<Employee, MailViewModel>(emp);

            mvm.LeaveStatus = leaveRequest.LeaveStatus;

            return mvm;


        }

        public List<LeaveViewModel> GetLeaves()
        {
            List<Leave> leave = leaveRep.GetLeaves();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> leaveViewModels = mapper.Map<List<Leave>, List<LeaveViewModel>>(leave);
            return leaveViewModels;

        }
        public List<LeaveViewModel> GetLeaveByEmployeeID(int EmployeeID)
        {
            List<Leave> leave = leaveRep.GetLeaveByEmployeeID(EmployeeID);
            //LeaveViewModel leaveViewModel = null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<LeaveViewModel> leaveViewModels = mapper.Map<List<Leave>, List<LeaveViewModel>>(leave);
            return leaveViewModels;
            //if (leave != null)
            //{
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Leave, LeaveViewModel>(); cfg.IgnoreUnmapped(); });
            //    IMapper mapper = config.CreateMapper();
            //    leaveViewModel = mapper.Map<Leave, LeaveViewModel>(leave);
            ////}

            //return leaveViewModel ;
        }
    }
}
