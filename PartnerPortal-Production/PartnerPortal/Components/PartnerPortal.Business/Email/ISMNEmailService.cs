using System.Collections.Generic;
namespace PartnerPortal.Business.Email
{
    /// <summary>
    /// IEncryptionService Service
    /// </summary>
    /// <remarks>
    ///     Date        Developer       Description
    ///     10/28/2014  Amit            Created
    public interface ISMNEmailService : IService
    {
        //void SendFromWarranty(string body, string toAddress, string ccAddress, string bccAddress, string subject);
        void SendResetPassword(string body, string toAddress);
        void SendEmail(string body, string toAddress, string subject);
        void SendEmail(string body, string toAddress, string subject, List<string> attachments);
    }
}
