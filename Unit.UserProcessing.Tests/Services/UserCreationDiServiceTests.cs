using System;
using Moq;
using Unit.UserProcessing.Core.Services;
using Unit.UserProcessing.Data.Models;
using Unit.UserProcessing.Data.Repositories;
using Xunit;

namespace Unit.UserProcessing.Tests.Services
{
    public class UserCreationDiServiceTests
    {

        private readonly Mock<IAdRepository> _adRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserCreationDiService _userCreationDiService;

        public UserCreationDiServiceTests()
        {
            _adRepository = new Mock<IAdRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userCreationDiService = new UserCreationDiService(_adRepository.Object, _userRepository.Object);
        }

        [Fact]
        public async void CreateAsync_WhenUserExistsInDbAndIsActive_ShouldReturnUnsuccessfulWithMessage()
        {
            // arrange
            var user = new Core.Models.User { FirstName = "test", LastName = "last", EmailAddress = "test@test.com", IsActive = true };
            var dbUser = new Data.Models.User { UserId = 1, FirstName = user.FirstName, LastName = user.LastName, EmailAddress = user.EmailAddress, IsActive = true };
            _userRepository
                .Setup(i => i.GetUserByEmailAddressAsync(user.EmailAddress))
                .ReturnsAsync(dbUser);

            // act
            var result = await _userCreationDiService.CreateAsync(user);

            //assert
            Assert.False(result.IsSuccessful, "result.IsSuccessful");
            Assert.True(result.ErrorMessage == "The user exits and is active", "result.ErrorMessage");
        }

        [Fact]
        public async void CreateAsync_WhenUserExistsInDbAndIsNotActive_ShouldReturnUnsuccessfulWithMessage()
        {
            // fill in this test using the previous one as an example
            throw new NotImplementedException();

        }

        [Fact]
        public async void CreateAsync_WhenUserDoesNotExistsInDbAndDoesNotExistInAd_ShouldReturnUnsuccessfulWithMessage()
        {
            // fill in this test using the previous one as an example
            throw new NotImplementedException();

        }

        [Fact]
        public async void CreateAsync_WhenUserDoesNotExistsInDbAndDoesExistInAdAndIsInactive_ShouldReturnUnsuccessfulWithMessage()
        {
            // fill in this test using the previous one as an example
            throw new NotImplementedException();

        }

        [Fact]
        public async void CreateAsync_WhenUserDoesNotExistsInDbAndDoesExistInAdAndIsActive_SaveUserToDatabseAndReturnSuccessful()
        {
            // arrange
            var user = new Core.Models.User { FirstName = "test", LastName = "last", EmailAddress = "test@test.com", IsActive = true };
            var adUser = new UserPrincipalFake
            {
                EmailAddress = user.EmailAddress,
                Enabled = true
            };

            _userRepository
                .Setup(i => i.GetUserByEmailAddressAsync(user.EmailAddress))
                .ReturnsAsync((Data.Models.User)null);

            _adRepository
                .Setup(i => i.GetUserByEmailAddress(user.EmailAddress))
                .Returns(adUser);

            // act
            var result = await _userCreationDiService.CreateAsync(user);

            //assert
            _userRepository.Verify(i => i.AddAsync(It.IsAny<Data.Models.User>()), "_userRepository.AddAsync");
            _userRepository.Verify(i => i.SaveAsync(), "_userRepository.SaveAsync");
            Assert.True(result.IsSuccessful, "result.IsSuccessful");
            Assert.True(result.ErrorMessage == "", "result.ErrorMessage");
        }

        private class UserPrincipalFake : IUserPrincipal
        {
            public string EmailAddress { get; set; }
            public string GivenName { get; set; }
            public string EmployeeId { get; set; }
            public string MiddleName { get; set; }
            public string Surname { get; set; }
            public bool? Enabled { get; set; }
        }
    }
}
