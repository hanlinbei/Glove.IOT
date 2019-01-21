using Glove.IOT.Common.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common.Email
{
    //internal class SMTPMailer : FTMailBase
    //{
    //    public override bool Send(string smtpServer, int port, string userName, string password, bool enableSSL)
    //    {
    //        bool isSended = false;
    //        MailMessage message = new MailMessage();
    //        To.ForEach(email =>
    //        {
    //            if (email.IsValidEmail())
    //                message.To.Add(email);
    //        });
    //        message.Sender = new MailAddress(userName, userName);
    //        if (CC != null && CC.Any())
    //            CC.ForEach(email =>
    //            {
    //                if (email.IsValidEmail())
    //                    message.CC.Add(email);
    //            });
    //        message.Subject = Encoding.Default.GetString(Encoding.Default.GetBytes(Subject));
    //        message.From = DisplayName.IsBlank() 
    //                      ? new MailAddress(From) 
    //                      : new MailAddress(From, Encoding.Default.GetString(Encoding.Default.GetBytes(DisplayName)));
    //        message.Body = Encoding.Default.GetString(Encoding.Default.GetBytes(Body));
    //        message.BodyEncoding = Encoding.GetEncoding("GBK");
    //        message.HeadersEncoding = Encoding.UTF8;
    //        message.SubjectEncoding = Encoding.UTF8;
    //        message.
    //    }
    //}
}
