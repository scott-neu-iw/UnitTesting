using Moq;
using System;
using Unit.UserProcessing.Core.Services;
using Unit.UserProcessing.Data.Repositories;
using Xunit;

namespace Unit.Tests.Other
{
    public class MoqExceptions
    {
        private readonly Mock<IAdRepository> _adRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserCreationDiService _userCreationDiService;

        public MoqExceptions()
        {
            _adRepository = new Mock<IAdRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userCreationDiService = new UserCreationDiService(_adRepository.Object, _userRepository.Object);
        }

        [Fact]
        public async void CreateAsync_WhenUserRepositoryThrowsException_ShoulBubbleUpException()
        {
            // arrange
            var user = new UserProcessing.Core.Models.User { FirstName = "test", LastName = "last", EmailAddress = "test@test.com", IsActive = true };
            _userRepository
                .Setup(i => i.GetUserByEmailAddressAsync(user.EmailAddress))
                .ThrowsAsync(new Exception());

            //assert
            await Assert.ThrowsAsync<Exception>(() => _userCreationDiService.CreateAsync(user));
        }

        [Fact]
        public async void CreateAsync_WhenUserRepositoryThrowsException_Tech2_ShoulBubbleUpException()
        {
            // arrange
            var user = new UserProcessing.Core.Models.User { FirstName = "test", LastName = "last", EmailAddress = "test@test.com", IsActive = true };
            _userRepository
                .Setup(i => i.GetUserByEmailAddressAsync(user.EmailAddress))
                .ThrowsAsync(new Exception("message to test"));

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _userCreationDiService.CreateAsync(user));
            Assert.Contains("message to test", ex.Message);
        }

        [Fact]
        public async void CreateAsync_WhenUserRepositoryThrowsException_Tech3_ShoulBubbleUpException()
        {
            // arrange
            var user = new UserProcessing.Core.Models.User { FirstName = "test", LastName = "last", EmailAddress = "test@test.com", IsActive = true };
            _userRepository
                .Setup(i => i.GetUserByEmailAddressAsync(user.EmailAddress))
                .ThrowsAsync(new Exception());

            Exception ex = null;
            try
            {
                var result = await _userCreationDiService.CreateAsync(user);
            }
            catch (Exception e)
            {
                ex = e;
            }

            //assert
            Assert.NotNull(ex);
        }
    }
}
