using Esd.EnergyPec.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WindowsService.OperationNPOI.SendSms.Utilities
{

    public class SendEmailHelper
    {
        public static bool SendEmail(SmsContent content, bool f)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                //发送邮件的方式
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //指定邮件服务器
                smtp.Host = content.ServerHost;
                //Gmail QQ stmp ssl加密使用的端口
                smtp.Port = int.Parse(content.ServerPort);
                smtp.EnableSsl = f;//true 经过ssl加密
                //验证发件人的身份 用户名(邮件地址和密码)  
                smtp.Credentials = new NetworkCredential(content.From, content.Password);
                //初始化信息(来自 接收人)  
                MailMessage _mailmessage = new MailMessage();
                //_mailmessage.To = strto;  
                //发送抄送多个人 接收人邮件地址以,隔开  
                _mailmessage.From = new MailAddress(content.From);
                _mailmessage.To.Add(content.To);
                _mailmessage.CC.Add(content.CC);
                //如果发送失败，SMTP 服务器将发送 失败邮件通知  
                _mailmessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //优先级  
                _mailmessage.Priority = MailPriority.High;
                //发送主题  
                _mailmessage.Subject = content.Subject;
                //有附件则添加附件  
                if (!string.IsNullOrEmpty(content.Attachment))
                {
                    Attachment attch = new Attachment(content.Attachment);
                    _mailmessage.Attachments.Add(attch);
                }
                //邮件主题编码  
                _mailmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                //指定发送的格式 (Html)  
                _mailmessage.IsBodyHtml = true;
                //指定发送邮件的编码  
                _mailmessage.BodyEncoding = System.Text.Encoding.UTF8;
                //指定邮件内容  
                _mailmessage.Body = content.Body;
                //发送邮件  

                smtp.Send(_mailmessage);

                return true;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException ?? ex);
                return false;
            }
        }

    }
}
