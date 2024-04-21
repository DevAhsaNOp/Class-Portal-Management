using Application.ClientFeatures.User.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController(IUser user) : Controller
    {
        private readonly IUser _user = user;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var user = new UserCreateRequest();
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateRequest user)
        {
            if (ModelState.IsValid)
            {
                user.Status = 2;
                user.CreatedBy = 1;
                var response = await _user.Create(user);
                if (response.code == HttpStatusCode.OK)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(string.Empty, response.message);

                return View(user);
            }

            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            else
            {
                var user = new UserLoginRequest();
                return View(user);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var response = await _user.CheckLogin(user);
                if (response.code == HttpStatusCode.OK)
                {
                    var userResponse = response.result;
                    if (userResponse is not null)
                    {
                        var claims = new List<Claim>
                        {
                            new(ClaimTypes.GivenName, userResponse.FullName),
                            new(ClaimTypes.Name, userResponse.Username),
                            new(ClaimTypes.Email, userResponse.Email),
                            new(ClaimTypes.Role, userResponse.Role),
                            new("ProfileImage", userResponse.Image),
                            new("UserId", userResponse.Id.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var props = new AuthenticationProperties
                        {
                            IsPersistent = user.RememberMe,
                            AllowRefresh = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Your account is not active.");
                }
                else
                {
                    TempData["Error"] = response.result;
                    ModelState.AddModelError(string.Empty, response.message);
                }
            }

            return View(user);
        }

        public async Task<JsonResult> IsEmailExist(string Email)
        {
            var result = await _user.IsEmailExits(Email);
            return Json(!result);
        }

        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            var user = await _user.GetById(1);
            if (user == null)
                return NotFound();
            else
            {
                UserUpdateView userResponseV2 = new()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Email = user.Email,
                    ProfileImage = user.Image,
                    Status = user.Status,
                };
                return View(userResponseV2);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MyAccount(UserUpdateView user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = 1;
                user.Status = 1;
                var response = await _user.Update(user);
                if (response.code == HttpStatusCode.OK)
                {
                    TempData["Success"] = response.result;
                    return RedirectToAction("MyAccount", "Account");
                }
                else
                {
                    TempData["Error"] = response.result;
                    ModelState.AddModelError(string.Empty, response.message);
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var user = new UserChangePasswordRequest
            {
                Id = 1
            };
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequest user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = 1;
                user.Status = 1;
                var response = await _user.ChangePassword(user);
                if (response.code == HttpStatusCode.OK)
                {
                    TempData["Success"] = response.result;
                    // Logout the user
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.Session.Clear();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["Error"] = response.result;
                }
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
