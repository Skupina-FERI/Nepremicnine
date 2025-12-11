using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using RZ_nepremicnine.Models;
using RZ_nepremicnine.Pages.Account;
using RZ_nepremicnine.ViewModels;
using Microsoft.AspNetCore.Http;

namespace RZ_Nepremicine.test
{
    [TestClass]
    public class UnitTestLogin
    {
        private Mock<SignInManager<Uporabniki>> CreateMockSignInManager()
        {
            var userStoreMock = new Mock<IUserStore<Uporabniki>>();
            var mockUserManager = new Mock<UserManager<Uporabniki>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<Uporabniki>>();

            return new Mock<SignInManager<Uporabniki>>(
                mockUserManager.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null, null, null, null);
        }

        private void SetupUrlHelper(Login loginModel)
        {
            var urlHelperMock = new Mock<IUrlHelper>();
            urlHelperMock.Setup(x => x.Content(It.IsAny<string>())).Returns((string s) => s);
            urlHelperMock.Setup(x => x.IsLocalUrl(It.IsAny<string>())).Returns(true);
            loginModel.Url = urlHelperMock.Object;
        }

        [TestMethod]
        public void OnGetTestSetsReturnUrl()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            var returnUrl = "/Properties/Browse";

            loginModel.OnGet(returnUrl);

            Assert.AreEqual(returnUrl, loginModel.ReturnUrl);
        }

        [TestMethod]
        public void OnGetTestNullReturnUrl()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);

            loginModel.OnGet(null);

            Assert.IsNull(loginModel.ReturnUrl);
        }
        
        [TestMethod]
        public async Task OnPostAsyncTestSuccessfulLoginWithReturnUrl()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);
            var returnUrl = "/Properties/Details?id=1";

            loginModel.Input = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                RememberMe = true
            };

            mockSignInManager
                .Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await loginModel.OnPostAsync(returnUrl);

            var redirectResult = result as LocalRedirectResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(returnUrl, redirectResult.Url);
        }

        [TestMethod]
        public async Task OnPostAsyncTestInvalidCredentials()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);

            loginModel.Input = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "WrongPassword",
                RememberMe = false
            };

            mockSignInManager
                .Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var result = await loginModel.OnPostAsync(null);

            var pageResult = result as PageResult;
            Assert.IsNotNull(pageResult);
            Assert.IsFalse(loginModel.ModelState.IsValid);
            Assert.IsTrue(loginModel.ModelState.ContainsKey(string.Empty));
        }

        [TestMethod]
        public async Task OnPostAsyncTestLockedOutAccount()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);

            loginModel.Input = new LoginViewModel
            {
                Email = "locked@example.com",
                Password = "Test123!",
                RememberMe = false
            };

            mockSignInManager
                .Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.LockedOut);

            var result = await loginModel.OnPostAsync(null);

            var pageResult = result as PageResult;
            Assert.IsNotNull(pageResult);
            Assert.IsFalse(loginModel.ModelState.IsValid);
            var errors = loginModel.ModelState[string.Empty].Errors;
            Assert.IsTrue(errors.Any(e => e.ErrorMessage.Contains("locked")));
        }

        [TestMethod]
        public async Task OnPostAsyncTestInvalidModelState()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);

            loginModel.Input = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                RememberMe = false
            };

            loginModel.ModelState.AddModelError("Email", "Email is required");

            var result = await loginModel.OnPostAsync(null);

            var pageResult = result as PageResult;
            Assert.IsNotNull(pageResult);
            mockSignInManager.Verify(x => x.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public async Task OnPostAsyncTestRememberMeTrue()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);

            loginModel.Input = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                RememberMe = true
            };

            mockSignInManager
                .Setup(x => x.PasswordSignInAsync(
                    "test@example.com",
                    "Test123!",
                    true,
                    true))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            await loginModel.OnPostAsync(null);

            mockSignInManager.Verify(x => x.PasswordSignInAsync(
                "test@example.com",
                "Test123!",
                true,
                true), Times.Once);
        }

        [TestMethod]
        public async Task OnPostAsyncTestEmptyReturnUrl()
        {
            var mockSignInManager = CreateMockSignInManager();
            var loginModel = new Login(mockSignInManager.Object);
            SetupUrlHelper(loginModel);

            loginModel.Input = new LoginViewModel
            {
                Email = "test@example.com",
                Password = "Test123!",
                RememberMe = false
            };

            mockSignInManager
                .Setup(x => x.PasswordSignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await loginModel.OnPostAsync(string.Empty);

            var redirectResult = result as RedirectToPageResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("/Index", redirectResult.PageName);
        }
    }
}