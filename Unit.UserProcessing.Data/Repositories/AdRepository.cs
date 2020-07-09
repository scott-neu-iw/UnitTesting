using System.DirectoryServices.AccountManagement;
using Unit.UserProcessing.Data.Models;

namespace Unit.UserProcessing.Data.Repositories
{
    public interface IAdRepository
    {
        IUserPrincipal GetUserByEmailAddress(string emailAddress);
    }

    public class AdRepository : IAdRepository
    {
        private readonly ActiveDirectorySettings _activeDirectorySettings;

        public AdRepository(ActiveDirectorySettings activeDirectorySettings)
        {
            _activeDirectorySettings = activeDirectorySettings;
        }

        public IUserPrincipal GetUserByEmailAddress(string emailAddress)
        {
            using (var adContext = new PrincipalContext(ContextType.Domain, _activeDirectorySettings.Domain))
            using (var searcher = new PrincipalSearcher())
            {
                searcher.QueryFilter = new UserPrincipal(adContext) { EmailAddress = emailAddress };
                return (IUserPrincipal)(UserPrincipal)searcher.FindOne();
            }
        }
    }
}
