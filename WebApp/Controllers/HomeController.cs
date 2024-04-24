using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController(IInstructor instructor,
        IClass _class) : Controller
    {
        private readonly IClass _class = _class;
        private readonly IInstructor _instructor = instructor;

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var instructorsAndClasses = new InstructorAndClassesVM
            {
                Classes = await _class.GetByStatuses(cancellationToken),
                Instructors = await _instructor.GetByStatuses(cancellationToken)
            };

            return View(instructorsAndClasses);
        }

        [HttpGet]
        public async Task<IActionResult> About(CancellationToken cancellationToken)
        {
            var Instructors = await _instructor.GetByStatuses(cancellationToken);
            return View(Instructors);
        }

        [HttpGet]
        public async Task<IActionResult> Class(CancellationToken cancellationToken)
        {
            var Classes = await _class.GetByStatuses(cancellationToken);
            return View(Classes);
        }

        [HttpGet]
        public async Task<IActionResult> Teachers(CancellationToken cancellationToken)
        {
            var Instructors = await _instructor.GetByStatuses(cancellationToken);
            return View(Instructors);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
