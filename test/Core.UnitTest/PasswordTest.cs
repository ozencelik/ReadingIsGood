using Core.Services.Customers;
using Xunit;

namespace Core.UnitTest
{
    public class PasswordTest
    {
        [Fact]
        public void Create_And_Verify_Password_Hash()
        {
            // Act
            var password = "ultimateSecurePassword";
            byte[] passwordHash;
            byte[] passwordSalt;

            CustomerService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Assert
            Assert.True(CustomerService.VerifyPasswordHash(password, passwordHash, passwordSalt));
        }
    }
}
