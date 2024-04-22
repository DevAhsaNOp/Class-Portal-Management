using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    //[Authorize]
    public class AdminController(IUser _user, IClass _class) : Controller
    {
        private readonly IUser _user = _user;
        private readonly IClass _class = _class;

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var _UsersAndInstructorsDetails = await _user.GetNewUsersAndInstructors();
            return View(_UsersAndInstructorsDetails);
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
