using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;
using System.Text.RegularExpressions;
using System.Activities;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        
        [Test, 
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidateEmail(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
            TestCase("Passworddd",false ),
            TestCase("PASSWORDDDD", false),
            TestCase("password123",false ),
            TestCase("JElszo12345", true),
            TestCase("Password123", true)
            ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidatePassword(password);

            Assert.AreEqual(expectedResult, actualResult);

        }

        public bool ValidPassW(string password)
        {
            var kisb = new Regex(@"[a-z]+");
            var nagyb = new Regex(@"[A-Z]+");
            var szam = new Regex(@"[0-9]+");
            var hosszusag = new Regex(@".{8,}");
            return kisb.IsMatch(password) && nagyb.IsMatch(password) && szam.IsMatch(password) && hosszusag.IsMatch(password);
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            
            var accountController = new AccountController();

            
            var actualResult = accountController.Register(email, password);

            
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            var accountController = new AccountController();

            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            
        }

        /*
        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234")
        ]

        
        public void TestRegisterApplicationException(string newEmail, string newPassword)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Throws<ApplicationException>();
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;

            // Act
            try
            {
                var actualResult = accountController.Register(newEmail, newPassword);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ApplicationException>(ex);
            }

            // Assert
        }
        */
        
    }
}
