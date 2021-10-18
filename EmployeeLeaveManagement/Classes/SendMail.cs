using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeLeaveManagement.ViewModels;
using System.Net.Mail;
using System.Net;


namespace EmployeeLeaveManagement.Classes
{
    public interface ISendMail
    {
        void SendEmail(MailViewModel MailViewModel);
    }

    public class SendMail : ISendMail
    {

        public void SendEmail (MailViewModel MailViewModel)
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