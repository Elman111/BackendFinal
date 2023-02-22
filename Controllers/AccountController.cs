using BackFinal.Helpers;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackFinal.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
       
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser()
            {
                UserName = register.UserName,
                Email = register.Email,
                FullName = register.FullName
            };

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View(register);
            }

            await _userManager.AddToRoleAsync(user, RolesEnum.Admin.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token },
                Request.Scheme, Request.Host.ToString());

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("e_nagdaliyev@mail.ru", "VerifyEmail");
            mailMessage.To.Add(new MailAddress(user.Email));
            mailMessage.Subject = "Verify Email";
            string body = string.Empty;
            using (StreamReader streamReader = new StreamReader("wwwroot/Template/htmlpage.html"))
            {
                body = streamReader.ReadToEnd();
            }
            mailMessage.Body = body.Replace("{{link}}", url);
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("e_nagdaliyev@mail.ru", "humiccezclrltgkj");

            smtpClient.Send(mailMessage);



            return RedirectToAction("Login");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return NotFound();
            await _userManager.ConfirmEmailAsync(appUser, token);
            await _signInManager.SignInAsync(appUser, true);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM login, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong email or password");
                return View(login);
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("Verify", "Confirm your email address");
                return View();
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account blocked");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong email or address");
                return View(login);
            }

            await _signInManager.SignInAsync(user, login.RememberMe);


            var isAdminRole = await _userManager.IsInRoleAsync(user, RolesEnum.Admin.ToString());


            if (isAdminRole)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "AdminArea" });

            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }


            return RedirectToAction("index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> AddRoles()
        //{
        //    foreach (var role in Enum.GetValues(typeof(RolesEnum)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }

        //    }
        //    return Content("role elave olundu");

        //}
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(forgetPasswordVM.AppUser.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("Error", "Bele bir email yoxdur");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var link = Url.Action(nameof(ResetPassword), "Account", new
            {
                email = appUser.Email,
                token
            }, Request.Scheme, Request.Host.ToString());
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("e_nagdaliyev@mail.ru", "CodeAcademy Coni");
            mailMessage.To.Add(new MailAddress(appUser.Email));
            mailMessage.Subject = "Reset Password";
            mailMessage.Body = $"<a href={link}>Please Click Here</a>";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("e_nagdaliyev@mail.ru", "humiccezclrltgkj");

            smtpClient.Send(mailMessage);


            return RedirectToAction("Index", "Home");
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string token, string email, ForgetPasswordVM forgetPasswordVM)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            if (!ModelState.IsValid) return View();

            await _userManager.ResetPasswordAsync(user, token, forgetPasswordVM.Password);

            return RedirectToAction("Index", "Home");
        }

    }
}
