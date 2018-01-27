using System.Collections.Generic;
using System.Linq;
using GW.Site.Data;
using GW.Site.Pages.Account;
using GW.Site.Tests.Fakes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace GW.Site.Tests.Pages.Account
{
    public class RegisterTests
    {

        [Fact]
        public void RegisterUser_CreateSecondUser_RedirectToUserAlreadyExistsPage()
        {
            // Arrange
            var fakeUserManager = new FakeUserManager();
            fakeUserManager.SetUsers(new List<ApplicationUser>(new ApplicationUser[] {
                        new ApplicationUser
                        {
                            UserName = "Test User 1",
                        }
                    }).AsQueryable());

            var registerModel = new RegisterModel(fakeUserManager, null, null, null);
            registerModel.PageContext = new PageContext();

            // Act
            var result = registerModel.OnPostAsync().Result;

            // Assert
            Assert.IsType(typeof(RedirectToPageResult), result); 
        }

        [Fact]
        public void RegisterUser_FirstUser_LocalRedirect()
        {
            // Arrange
            var fakeUserManager = new FakeUserManager();
            fakeUserManager.SetUsers(new List<ApplicationUser>(new ApplicationUser[] {}).AsQueryable());

            var fakeSignInManager = new FakeSignInManager();

            var registerModel = new RegisterModel(fakeUserManager, fakeSignInManager, new FakeLogger<LoginModel>(), null);
            registerModel.PageContext = new PageContext();

            registerModel.Input = new RegisterModel.InputModel
            {
                Email = "test@test.com",
                Password = "TestPassword.1",
            };

            // Act
            var result = registerModel.OnPostAsync().Result;

            // Assert
            Assert.IsType(typeof(LocalRedirectResult), result);
        }
    }
}
