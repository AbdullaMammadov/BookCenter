using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Net.Mail;
using FinalProject.Models;
using FinalProject.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Helpers;

using FinalProject.ViewModel;
using FinalProject.Helpers;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {

                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }

            return Content("okayy");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser();
            appUser.UserName = registerVM.Username;
            appUser.FirstName = registerVM.FirstName;
            appUser.LastName = registerVM.LastName;
            appUser.Email = registerVM.Email;
            appUser.IsActive = false;
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();


            }
            await _userManager.AddToRoleAsync(appUser, Roles.User.ToString());


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var link = Url.Action(nameof(VerifyEmail), "Account", new { token, email = appUser.Email }, Request.Scheme, Request.Host.ToString());
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("mammadova.nazrin2004@gmail.com", "Verify Email");
            mailMessage.To.Add(new MailAddress(appUser.Email));
            mailMessage.Subject = "Verfiy email";
            string body = String.Empty;

            using (StreamReader streamReader = new StreamReader("wwwroot/theme/htmlpage.html"))
            {
                body = streamReader.ReadToEnd();

            }


            body = body.Replace("{{link}}", link);
            body = body.Replace("{{userName}}", appUser.UserName);
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;


            smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "qoec hyzg wlwv rdqf");
            smtpClient.Send(mailMessage);


            await _userManager.AddToRoleAsync(appUser, "User");
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.ConfirmEmailAsync(user, token);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            AppUser appUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);

            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "Something Went wrong");
                    return View();
                }
            }

            if (appUser.IsActive)
            {
                ModelState.AddModelError("", "you are blocked");
                return View(loginVM);
            }
            var resoult = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.Remember, true);
            if (resoult.IsLockedOut)
            {
                ModelState.AddModelError("", "User is blocked");
                return View(loginVM);
            }
            if (!appUser.EmailConfirmed)
            {
                ModelState.AddModelError("", "verify ol!");
                return View(loginVM);
            }
            if (!resoult.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(loginVM);
            }






            await _signInManager.SignInAsync(appUser, loginVM.Remember);

            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }



            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/home/index");
        }
        public IActionResult ForgetPassword()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string? email)
        {
            if (email != null)
            {


                AppUser user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "bele mail movcud deil");
                    return View();
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress("mammadova.nazrin2004@gmail.com", "Email form Baku Book Center");
                mailMessage.To.Add(new MailAddress(user.Email));
                mailMessage.Subject = "reset password";
                mailMessage.Body = $"<a href={link}>Please click here to reset your password</a>";
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;


                smtpClient.Credentials = new NetworkCredential("mammadova.nazrin2004@gmail.com", "qoec hyzg wlwv rdqf");
                smtpClient.Send(mailMessage);

                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("Email", "zehmet olmasa mail dahil edin");
                return View();
            }
        }
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, ResetPasswordVM resetPasswordVM)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (!ModelState.IsValid) return View();

            await _userManager.ResetPasswordAsync(appUser, token, resetPasswordVM.Password);
            await _userManager.UpdateSecurityStampAsync(appUser);

            return RedirectToAction("Login", "Account");

        }

    }

}