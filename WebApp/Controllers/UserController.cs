using Application.ClientFeatures.User.Request;
using Application.ClientFeatures.User.Response;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    public class UserController(IUser user) : Controller
    {
        private readonly IUser _user = user;

        [HttpGet]
        public async Task<IActionResult> Users(CancellationToken cancellationToken)
        {
            var _Users = await _user.GetByStatuses(cancellationToken);
            return View(_Users);
        }

        [HttpGet]
        public async Task<IActionResult> NonActiveUsers(CancellationToken cancellationToken)
        {
            var _Users = await _user.GetNonActiveUsers(cancellationToken);
            return View(_Users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateRequest request)
        {
            request.CreatedBy = 1;
            request.Status = 1;
            var _response = await _user.Create(request);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.result;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.result;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var _User = await _user.GetById(Id);
            if (_User == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Users");
            }
            else
            {
                var user = new UserUpdateRequest
                {
                    Id = _User.Id,
                    ConfirmPassword = _User.Password,
                    Email = _User.Email,
                    FullName = _User.FullName,
                    ProfileImage = _User.Image,
                    Password = _User.Password,
                    Status = _User.Status,
                    UpdatedBy = _User.UpdatedBy,
                    Username = _User.Username
                };
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            request.UpdatedBy = 1;
            request.Status = 1;
            var _response = await _user.Update(request);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.result;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.result;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = new UserDeleteRequest
            {
                Id = Id,
                DeletedBy = 1,
                Status = 3
            };
            var _response = await _user.Delete(user);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.result;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.result;
                return RedirectToAction("Users");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int Id)
        {
            var _response = await _user.GetUserDetailId(Id);
            if (_response == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("NonActiveUsers");
            }
            return View(_response);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(int Id)
        {
            var _response = await _user.GetById(Id);
            if (_response == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction("Users");
            }
            return View(_response);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeTheUserToActive(UserResponseV2 user)
        {
            var userRequest = new UserStatusChangeRequest
            {
                Id = user.Id,
                Status = 1,
                UpdatedBy = 1
            };
            var _response = await _user.ChangeUserStatus(userRequest);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.result;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.result;
                return RedirectToAction("NonActiveUsers");
            }
        }
    }
}
