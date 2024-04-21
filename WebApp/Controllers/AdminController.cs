using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    //[Authorize]
    public class AdminController(IAdmin admin) : Controller
    {
        private readonly IAdmin _admin = admin;

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var _UsersAndInstructorsDetails = await _admin.GetNewUsersAndInstructors();
            return View(_UsersAndInstructorsDetails);
        }
    }
}
