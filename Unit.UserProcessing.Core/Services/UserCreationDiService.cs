using System.Threading.Tasks;
using Unit.UserProcessing.Core.Models;
using Unit.UserProcessing.Data.Repositories;

namespace Unit.UserProcessing.Core.Services
{
    public class UserCreationDiService
    {
        private readonly IAdRepository _adRepository;
        private readonly IUserRepository _userRepository;

        public UserCreationDiService(IAdRepository adRepository, IUserRepository userRepository)
        {
            _adRepository = adRepository;
            _userRepository = userRepository;
        }

        public async Task<TaskResult> CreateAsync(User user)
        {
            // get DB user
            var dbUser = await _userRepository.GetUserByEmailAddressAsync(user.EmailAddress);
            if (dbUser != null)
            {
                return dbUser.IsActive
                    ? new TaskResult(false, "The user exits and is active")
                    : new TaskResult(false, "The user exists but is inactive");
            }

            // get AD user
            var adUser = _adRepository.GetUserByEmailAddress(user.EmailAddress);

            if (adUser == null) new TaskResult(false, "The AD user not found");
            if (adUser.Enabled == false) new TaskResult(false, "The AD user is not active");

            // add DB User if needed
            await _userRepository.AddAsync(user.ToDataModel());
            await _userRepository.SaveAsync();

            return new TaskResult(true);
        }
    }
}
