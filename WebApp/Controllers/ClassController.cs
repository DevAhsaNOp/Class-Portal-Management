using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClassController(IClass _class) : Controller
    {
        private readonly IClass _class = _class;

        [HttpGet]
        public async Task<IActionResult> Classes(CancellationToken cancellationToken)
        {
            var _Classes = await _class.GetByStatuses(cancellationToken);
            return View(_Classes);
        }
    }
}
