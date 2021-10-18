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
using System.Net.Mail;
using System.Net;

namespace EmployeeLeaveManagement.ServiceLayer
{
    public interface ILeaveService
    {
        void InsertLeaveRequest(LeaveViewModel leaveRequest);
        MailViewModel UpdateLeaveStatusByLeaveID(LeaveViewModel leaveRequest);

        List<LeaveViewModel> GetLeaves();
        List<LeaveViewModel> GetLeaveByEmployeeID(int EmployeeID);

        void SendEmail(MailViewModel MailViewModel);


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

        public void SendEmail(MailViewModel MailViewModel)
        {
            try
            {
                var senderEmail = new MailAddress("mvcp990@gmail.com", "mvc");
                var receiverEmail = new MailAddress(MailViewModel.Email, "Receiver");
                var password = "mvcp1234";
                var sub = MailViewModel.LeaveStatus + " your leave request";
                var body = MailViewModel.FirstName + ", your leave request has been " + MailViewModel.LeaveStatus;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                var i = 1;
            }
        }
    }
}

