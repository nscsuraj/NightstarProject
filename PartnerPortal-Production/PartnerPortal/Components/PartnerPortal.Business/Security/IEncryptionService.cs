
namespace PartnerPortal.Business.Security
{
    /// <summary>
    /// IEncryptionService Service
    /// </summary>
    /// <remarks>
    ///     Date        Developer       Description
    ///     10/28/2014  Amit            Created
    public interface IEncryptionService : IService
    {

        /// <summary>
        /// Encrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        string Encrypt(string value);


        /// <summary>
        /// Determines whether the specified password is matched.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <returns></returns>
        string Decrypt(string password);
    }
}
