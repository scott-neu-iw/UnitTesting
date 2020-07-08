using System.DirectoryServices.AccountManagement;
using Unit.UserProcessing.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User = Unit.UserProcessing.Core.Models.User;
using DbUser = Unit.UserProcessing.Data.Models.User;
using Unit.UserProcessing.Core.Models;

namespace Unit.UserProcessing.Core.Services
{
    public class UserCreationProceduralService
    {
        private readonly DataContext _context;
        private readonly ActiveDirectorySettings _activeDirectorySettings;

        public UserCreationProceduralService(DataContext context, ActiveDirectorySettings activeDirectorySettings)
        {
            _context = context;
            _activeDirectorySettings = activeDirectorySettings;
        }

        public async Task<TaskResult> CreateAsync(User user)
        {
            // get DB user
            var dbUser = await _context.User.FirstOrDefaultAsync(i => i.EmailAddress == user.EmailAddress);
            if (dbUser != null)
            {
                return dbUser.IsActive
                    ? new TaskResult(false, "The user exits and is active")
                    : new TaskResult(false, "The exists but is inactive");
            }

            // get AD user
            UserPrincipal adUser;
            using (var adContext = new PrincipalContext(ContextType.Domain, _activeDirectorySettings.Domain))
            using (var searcher = new PrincipalSearcher())
            {
                searcher.QueryFilter = new UserPrincipal(adContext) { EmailAddress = user.EmailAddress };
                adUser = (UserPrincipal)searcher.FindOne();
            }

            if (adUser == null) new TaskResult(false, "The AD user not found");
            if (adUser.Enabled == false) new TaskResult(false, "The AD user is not active");

            // add DB User if needed
            await _context.User.AddAsync(new DbUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsActive = true
            });

            await _context.SaveChangesAsync();

            return new TaskResult(true);
        }
    }
}
