using PartnerPortal.Business.Security;
using PartnerPortal.Repository;
using PartnerPortal.Domain.Admin;

namespace PartnerPortal.Business.Users
{
    /// <summary>
    /// UserService Service
    /// </summary>
    /// <remarks>
    ///     Date        Developer       Description
    ///     10/28/2014  Amit            Created
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Member variables
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        private readonly IEFRepository<UserProfile> _user;

        public AuthenticationService(IEncryptionService encryptionService,
            IEFRepository<UserProfile> user)
        {
            _encryptionService = encryptionService;
            _user = user;
        }

        public bool Validate(string userName, string password)
        {
            if (_user.Get(x => x.LoginId == userName) != null)
            {
                return _encryptionService.Decrypt(_user.Get(x => x.LoginId == userName).LoginPassword) == password;
            }
            return false;
        }
    }
}
