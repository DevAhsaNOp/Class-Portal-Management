using Application.ClientFeatures.User.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    //[Authorize]
    public class AdminController(IAdmin admin, IUser _user, IInstructor _instructor, IClass _class) : Controller
    {
        private readonly IUser _user = _user;
        private readonly IAdmin _admin = admin;
        private readonly IClass _class = _class;
        private readonly IInstructor _instructor = _instructor;

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var _UsersAndInstructorsDetails = await _admin.GetNewUsersAndInstructors();
            return View(_UsersAndInstructorsDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Users(CancellationToken cancellationToken)
        {
            var _Users = await _user.GetByStatuses(cancellationToken);
            return View(_Users);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateRequest request)
        {
            request.CreatedBy = 1;
            request.Status = 1;
            var _response = await _user.Create(request);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.message;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int Id)
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
        public async Task<IActionResult> EditUser(UserUpdateRequest request)
        {
            var _response = await _user.Update(request);
            if (_response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = _response.message;
                return RedirectToAction("Users");
            }
            else
            {
                TempData["Error"] = _response.message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Instructors(CancellationToken cancellationToken)
        {
            var _Instructors = await _instructor.GetByStatuses(cancellationToken);
            return View(_Instructors);
        }

        [HttpGet]
        public async Task<IActionResult> Classes(CancellationToken cancellationToken)
        {
            var _Classes = await _class.GetByStatuses(cancellationToken);
            return View(_Classes);
        }

        public async Task<JsonResult> IsEmailExist(string Email)
        {
            var result = await _user.IsEmailExits(Email);
            return Json(!result);
        }
    }
}
