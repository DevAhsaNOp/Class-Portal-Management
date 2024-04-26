using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController(IInstructor instructor,
        IClass _class, IEnrollment _enrollment) : Controller
    {
        private readonly IClass _class = _class;
        private readonly IInstructor _instructor = instructor;
        private readonly IEnrollment _enrollment = _enrollment;

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var classes = await _class.GetByStatuses(cancellationToken);
            var instructors = await _instructor.GetByStatuses(cancellationToken);

            var instructorAndClasses = new InstructorAndClassesAndEnrollmentVM
            {
                Classes = classes,
                Instructors = instructors
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
                var enrollments = await _enrollment.GetAllEnrolledClassByUserId(userId, cancellationToken);
                instructorAndClasses.Enrollments = enrollments;
            }

            return View(instructorAndClasses);
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
            var enrollmentsAndClasses = new EnrollmentAndClassesVM
            {
                Classes = await _class.GetByStatuses(cancellationToken),
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.FindFirstValue("UserId"));
                var enrollments = await _enrollment.GetAllEnrolledClassByUserId(userId, cancellationToken);
                enrollmentsAndClasses.Enrollments = enrollments;
            }

            return View(enrollmentsAndClasses);
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
