using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace EmailSender.Models
{
    class EmailSendService
    {
        public string Status { get; private set; }
        public string ErrorInfo { get; private set; }

        public bool Send(EmailInfo mailInfo)
        {            
            try
            {
                MailMessage mailMessage = new MailMessage(mailInfo.Sender, mailInfo.Target);
                mailMessage.Subject = mailInfo.Subject;
                mailMessage.Body = mailInfo.Body;
                mailMessage.IsBodyHtml = false;

                SmtpClient sc = new SmtpClient(mailInfo.SmtpClient, mailInfo.Port);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(mailInfo.Sender, mailInfo.Password);
                sc.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Status = ex.StackTrace;
                ErrorInfo = ex.Message;
                return false;
            }
            Status = "OK";
            return true;
        }
    }
}
