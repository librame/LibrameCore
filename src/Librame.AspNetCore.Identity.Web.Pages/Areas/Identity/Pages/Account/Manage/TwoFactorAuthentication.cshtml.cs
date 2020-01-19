#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Web;
    using Extensions;
    using Resources;

    /// <summary>
    /// ˫������֤ҳ��ģ�͡�
    /// </summary>
    [GenericApplicationModel(typeof(TwoFactorAuthenticationPageModel<>))]
    public class TwoFactorAuthenticationPageModel : PageModel
    {
        /// <summary>
        /// ������֤����
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        /// ʣ��ָ�������
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        /// �Ƿ�����˫������֤��
        /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        /// <summary>
        /// �Ƿ��ס���豸��
        /// </summary>
        public bool IsMachineRemembered { get; set; }

        /// <summary>
        /// ״̬��Ϣ��
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// �ύ������
        /// </summary>
        /// <returns>����һ�� <see cref="Task{IActionResult}"/>��</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();

    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class TwoFactorAuthenticationPageModel<TUser> : TwoFactorAuthenticationPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationPageModel> _logger;
        private readonly IStringLocalizer<StatusMessageResource> _statusLocalizer;


        public TwoFactorAuthenticationPageModel(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            ILogger<TwoFactorAuthenticationPageModel> logger,
            IStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _statusLocalizer = statusLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAndResultAsync() != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync();
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user).ConfigureAndResultAsync();
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user).ConfigureAndResultAsync();

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _signInManager.ForgetTwoFactorClientAsync().ConfigureAndWaitAsync();

            StatusMessage = _statusLocalizer.GetString(r => r.TwoFactorAuthentication)?.ToString();

            return RedirectToPage();
        }
    }
}