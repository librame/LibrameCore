using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;
    using Resources;

    public class ViewModelsResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IEnhancedStringLocalizer<ViewModelsResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IEnhancedStringLocalizer<ViewModelsResource> localizer, string cultureName)
        {
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            var email = localizer[r => r.Email];
            Assert.False(email.ResourceNotFound);

            var password = localizer[r => r.Password];
            Assert.False(password.ResourceNotFound);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.False(confirmPassword.ResourceNotFound);

            var newPassword = localizer[r => r.NewPassword];
            Assert.False(newPassword.ResourceNotFound);

            var confirmNewPassword = localizer[r => r.ConfirmNewPassword];
            Assert.False(confirmNewPassword.ResourceNotFound);

            var oldPassword = localizer[r => r.OldPassword];
            Assert.False(oldPassword.ResourceNotFound);

            var phoneNumber = localizer[r => r.PhoneNumber];
            Assert.False(phoneNumber.ResourceNotFound);

            var rememberBrowser = localizer[r => r.RememberBrowser];
            Assert.False(newPassword.ResourceNotFound);

            var rememberMe = localizer[r => r.RememberMe];
            Assert.False(rememberMe.ResourceNotFound);
        }

    }
}