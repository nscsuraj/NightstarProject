
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PartnerPortal.Domain.SiteUtility;
using PartnerPortal.Repository;


namespace PartnerPortal.Business.Email
{
    /// <summary>
    /// Email Service
    /// </summary>
    /// <remarks>
    ///     Date        Developer       Description
    ///     10/28/2014  Amit            Created
    public class SMNEmailService : ISMNEmailService
    {

        private readonly IEFRepository<SystemConfig> _systemConfig;
        public SMNEmailService(IEFRepository<SystemConfig> systemConfig)
        {
            _systemConfig = systemConfig;
        }

        public void SendResetPassword(string body, string toAddress)
        {
            //Send email
            var smtpServer = GetSystemConfigValue("SmtpServer");
            var smtpEmail = GetSystemConfigValue("SmtpEmail");
            var smtpPassword = GetSystemConfigValue("SmtpPassword");
            var subject = "Password Reset Link";

            var isLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLocal"]);

            var msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["PartnerPortalAdmin"]);
            //toAddress = "amit@nightstarpartners.com";
            msg.To.Add(toAddress);
         
            msg.Body = body;

            msg.IsBodyHtml = true;
            msg.Subject = subject;

            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Host = smtpServer;
            mSmtpClient.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
            mSmtpClient.Port = 25;
            //if (isLocal)
            //{
            //    mSmtpClient.Port = 587;
            //    mSmtpClient.EnableSsl = true;
            //}
            try
            {
                
                mSmtpClient.Send(msg);
            }
            catch (Exception e)
            {
            }
        }

        public void SendEmail(string body, string toAddress,string subject)
        {
            //Send email
            var smtpServer = GetSystemConfigValue("SmtpServer");
            var smtpEmail = GetSystemConfigValue("SmtpEmail");
            var smtpPassword = GetSystemConfigValue("SmtpPassword");

            var isLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLocal"]);

            var msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["PartnerPortalAdmin"]);
            msg.To.Add(toAddress);
          //  msg.Bcc.Add("amit@nightstarpartners.com,tguella@starmicronics.com");
            msg.Body = body;

            msg.IsBodyHtml = true;
            msg.Subject = subject;

            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Host = smtpServer;
            mSmtpClient.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
            mSmtpClient.Port = 2525;
            //if (isLocal)
            //{
            //    mSmtpClient.Port = 587;
            //    mSmtpClient.EnableSsl = true;
            //}
            try
            {
                mSmtpClient.Send(msg);
            }
            catch (Exception e)
            {
            }
        }
        public void SendEmail(string body, string toAddress, string subject, List<string> attachments)
        {
            //Send email
            var smtpServer = GetSystemConfigValue("SmtpServer");
            var smtpEmail = GetSystemConfigValue("SmtpEmail");
            var smtpPassword = GetSystemConfigValue("SmtpPassword");

            var isLocal = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLocal"]);

            var msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["PartnerPortalAdmin"]);
            msg.To.Add(toAddress);
            msg.Bcc.Add("amit@nightstarpartners.com,tguella@starmicronics.com");
            msg.Body = body;

            foreach(var itm in attachments)
            {
                msg.Attachments.Add(new Attachment ( itm ));
            }

            msg.IsBodyHtml = true;
            msg.Subject = subject;

            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Host = smtpServer;
            mSmtpClient.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);
            mSmtpClient.Port = 2525;
            //if (isLocal)
            //{
            //    mSmtpClient.Port = 587;
            //    mSmtpClient.EnableSsl = true;
            //}
            try
            {
                mSmtpClient.Send(msg);
            }
            catch (Exception e)
            {
            }
        }
        //public void SendFromWarranty(string body, string toAddress, string ccAddress, string bccAddress, string subject)
        //{
        //    //Send email
        //    var smtpServer = GetSystemConfigValue("SmtpServer");
        //    var smtpEmail = GetSystemConfigValue("SmtpEmail");
        //    var smtpPassword = GetSystemConfigValue("SmtpPassword");

        //    var msg = new MailMessage();
        //    msg.From = new MailAddress(ConfigurationManager.AppSettings["WarrantyEmail"]);
        //    msg.To.Add(toAddress);
        //    if (!string.IsNullOrEmpty(ccAddress))
        //    {
        //        msg.CC.Add(ccAddress);
        //    }
        //    if (!string.IsNullOrEmpty(bccAddress))
        //    {
        //        msg.Bcc.Add(bccAddress);
        //    }
        //    msg.Body = body;

        //    msg.IsBodyHtml = true;
        //    msg.Subject = subject;

        //    SmtpClient mSmtpClient = new SmtpClient();
        //    mSmtpClient.Host = smtpServer;
        //    mSmtpClient.Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPassword);

        //    //PDF
        //    StringReader sr = new StringReader(body);

        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();
        //        byte[] bytes = memoryStream.ToArray();
        //        memoryStream.Close();


        //        msg.Attachments.Add(new Attachment(new MemoryStream(bytes), "Terms.pdf"));
        //    }
        //    try
        //    {
        //        mSmtpClient.Send(msg);
        //    }
        //    catch (Exception e)
        //    {
        //    }

        //}

        /// <summary>
        ///     Get System Config Value
        /// </summary>
        /// <returns>Result</returns>      
        private string GetSystemConfigValue(string configKey)
        {
            var configValue = _systemConfig.Get(x => x.ConfigKey == configKey);
            return configValue.ConfigValue;
        }
    }
}
