using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeLeaveManagement.ViewModels;
using EmployeeLeaveManagement.ServiceLayer;
using EmployeeLeaveManagement.CustomFilters;
using System.Net.Mail;
using System.Net;

namespace EmployeeLeaveManagement.Controllers
{
    public class LeaveController : Controller
    {
        ILeaveService leaveService;
        IEmployeeService employeeService;


        public LeaveController(ILeaveService leaveService, IEmployeeService employeeService)
        {
            this.leaveService = leaveService;
            this.employeeService = employeeService;
        }
        // GET: Leave
        [UserAuthorizationFilter]
        public ActionResult LeaveRequest()
        {

            LeaveViewModel leaveReq = new LeaveViewModel();
            return View(leaveReq);
        }


        [HttpPost]
        [UserAuthorizationFilter]
        public ActionResult LeaveRequest(LeaveViewModel leaveReq)
        {
            leaveReq.EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
            leaveReq.LeaveStatus = "Pending";

            this.leaveService.InsertLeaveRequest(leaveReq);
            return RedirectToAction("Index", "Home");

        }

        [UserAuthorizationFilter]
        public ActionResult LeaveStatus()
        {
            int EmployeeID = Convert.ToInt32(Session["CurrentUserID"]);
            List<LeaveViewModel> leaves = this.leaveService.GetLeaveByEmployeeID(EmployeeID);
            return View(leaves);
        }

        [HRandPMAuthorizationFilter]
        public ActionResult LeaveUpdation()
        {
            List<LeaveViewModel> leaves = this.leaveService.GetLeaves();
            return View(leaves);
        }

        [HttpPost]
        [HRandPMAuthorizationFilter]
        public ActionResult LeaveUpdation(LeaveViewModel updateLeave)
        {
            
            MailViewModel MailViewModel = this.leaveService.UpdateLeaveStatusByLeaveID(updateLeave);
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
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("LeaveUpdation", "Leave");


        }

        
    }
}