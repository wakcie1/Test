using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    public class EmailHelper
    {
        public static EmailSendResult Send(EmailSendModel send)
        {
            var result = new EmailSendResult() { IsSuccessful = false };

            var appSettings = ConfigurationManager.AppSettings;
            var smtpHost = string.IsNullOrEmpty(appSettings["SmtpHost"]) ? string.Empty : appSettings["SmtpHost"];
            var smtpPort = string.IsNullOrEmpty(appSettings["SmtpPort"]) ? 25 : Convert.ToInt32(appSettings["SmtpPort"]);
            var account = string.IsNullOrEmpty(appSettings["EmailAccount"]) ? string.Empty : appSettings["EmailAccount"];
            var password = string.IsNullOrEmpty(appSettings["EmailPw"]) ? string.Empty : appSettings["EmailPw"];
            //简单邮件传输协议类
            SmtpClient client = new SmtpClient();
            client.Host = smtpHost;//邮件服务器
            client.Port = smtpPort;//smtp主机上的端口号,默认是25.
            client.UseDefaultCredentials = false;
            //if (!string.IsNullOrEmpty(password))
            //{
            //邮件发送方式:通过网络发送到SMTP服务器
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //凭证,发件人登录邮箱的用户名和密码
            client.Credentials = new NetworkCredential(account, password);
            //}
            client.EnableSsl = false;
            //电子邮件信息类
            MailAddress fromAddress = new MailAddress(account, "QRQC System");//发件人Email,在邮箱是这样显示的,[发件人:小明<panthervic@163.com>;]
            MailAddress toAddress = new MailAddress(send.MailAddress, string.Empty);//收件人Email,在邮箱是这样显示的, [收件人:小红<43327681@163.com>;]
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);//创建一个电子邮件类
            mailMessage.Subject = send.MailTitle;
            //string filePath = Server.MapPath("/index.html");//邮件的内容可以是一个html文本.
            //System.IO.StreamReader read = new System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding("GB2312"));
            //string mailBody = read.ReadToEnd();
            //read.Close();
            mailMessage.Body = send.MailContent;//可为html格式文本
            //mailMessage.Body = "邮件的内容";//可为html格式文本
            mailMessage.SubjectEncoding = Encoding.UTF8;//邮件主题编码
            mailMessage.BodyEncoding = Encoding.UTF8;//邮件内容编码
            mailMessage.IsBodyHtml = true;//邮件内容是否为html格式
            mailMessage.Priority = MailPriority.High;//邮件的优先级,有三个值:高(在邮件主题前有一个红色感叹号,表示紧急),低(在邮件主题前有一个蓝色向下箭头,表示缓慢),正常(无显示).
            try
            {
                client.Send(mailMessage);//发送邮件
                result.IsSuccessful = true;
                //client.SendAsync(mailMessage, "ojb");异步方法发送邮件,不会阻塞线程.
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.ExceptionMessage = ex.ToString();
            }
            return result;
        }

        public static EmailSendResult SendToSystemAdmin(EmailSendModel send)
        {
            var result = new EmailSendResult() { IsSuccessful = false };
            var appSettings = ConfigurationManager.AppSettings;
            send.MailAddress = string.IsNullOrEmpty(appSettings["SystemAdminEmail"]) ? string.Empty : appSettings["SystemAdminEmail"];
            if (string.IsNullOrEmpty(send.MailAddress))
            {
                result.Message = "please config the System Admin Email";
                return result;
            }
            result = Send(send);
            return result;
        }
    }
}
