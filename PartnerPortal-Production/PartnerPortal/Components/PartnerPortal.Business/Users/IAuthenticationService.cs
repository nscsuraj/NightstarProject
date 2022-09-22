
namespace PartnerPortal.Business.Users
{
    /// <summary>
    /// IEncryptionService Service
    /// </summary>
    /// <remarks>
    ///     Date        Developer       Description
    ///     10/28/2014  Amit            Created
    public interface IAuthenticationService : IService
    {
        /// <summary>
        /// Validates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool Validate(string userName,string password);
    }
}
