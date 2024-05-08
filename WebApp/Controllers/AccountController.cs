using Application.ClientFeatures.User.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebApp.Extensions;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController(IUser user, IEnrollment enrollment) : Controller
    {
        private readonly IUser _user = user;
        private readonly IEnrollment _enrollment = enrollment;

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
                {
                    TempData["Success"] = response.result;
                    return RedirectToAction("Index", "Home");
                }
                else
                    TempData["Error"] = response.result;

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

                        LoggedInUserDetail.SetUserDetails(userResponse.Id, userResponse.FullName, userResponse.Image, userResponse.Username,
                            userResponse.Email, userResponse.Role);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                        if (userResponse.Role == "Admin")
                            return RedirectToAction("Dashboard", "Admin");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                        TempData["Error"] = "Invalid username or password";
                }
                else
                {
                    TempData["Error"] = response.result;
                }
            }

            return View(user);
        }

        [AllowAnonymous]
        public async Task<JsonResult> IsEmailExist(string Email)
        {
            var result = await _user.IsEmailExits(Email);
            return Json(!result);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyAccount()
        {
            var user = await _user.GetById(LoggedInUserDetail.UserId);
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
                LoggedInUserDetail.SetUserDetails(user.Id, user.FullName, user.Image, user.Username, user.Email, user.Role);
                return View(userResponseV2);
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyAccount(UserUpdateView user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = user.Id;
                user.Status = 1;
                var response = await _user.Update(user);
                if (response.code == HttpStatusCode.OK)
                {
                    TempData["Success"] = response.result;
                    return RedirectToAction("MyAccount", "Account");
                }
                else
                    TempData["Error"] = response.result;
            }
            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword()
        {
            var user = new UserChangePasswordRequest
            {
                Id = LoggedInUserDetail.UserId
            };
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword(UserChangePasswordRequest user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedBy = user.Id;
                user.Status = 1;
                var response = await _user.ChangePassword(user);
                if (response.code == HttpStatusCode.OK)
                {
                    TempData["Success"] = response.result;
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.Session.Clear();
                    return RedirectToAction("Login", "Account");
                }
                else
                    TempData["Error"] = response.result;
            }
            return View(user);
        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyClasses(string returnUrl, CancellationToken cancellationToken)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("User"))
                {
                    var enrollments = await _enrollment.GetAllEnrolledClassByUserId(LoggedInUserDetail.UserId, cancellationToken);
                    return View(enrollments);
                }
                else
                {
                    TempData["Error"] = "You are not authorized";
                    return Redirect(returnUrl);
                }
            }
            else
            {
                TempData["Error"] = "You are not authenticated";
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Logout()
        {
            LoggedInUserDetail.ClearUserDetails();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
