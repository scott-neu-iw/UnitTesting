using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unit.UserProcessing.Data.Models;

namespace Unit.UserProcessing.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAddressAsync(string emailAddress);
        Task AddAsync(User user);
        Task SaveAsync();
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAddressAsync(string emailAddress)
        {
            return await _context.User.FirstOrDefaultAsync(i => i.EmailAddress == emailAddress);
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
