using Application.ClientFeatures.Enrollment.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    public class EnrollmentController(IEnrollment enrollment, IClass _class, IUser _user) : Controller
    {
        private readonly IUser _user = _user;
        private readonly IClass _class = _class;
        private readonly IEnrollment _enrollment = enrollment;

        public async Task<IActionResult> Enrollments(CancellationToken cancellationToken)
        {
            var enrollments = await _enrollment.GetByStatuses(cancellationToken);
            return View(enrollments);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var enrollment = await _enrollment.GetById(id);

            if (enrollment is null)
            {
                TempData["Error"] = "Enrollment not found.";
                return RedirectToAction("Enrollments", "Enrollment");
            }

            return View(enrollment);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var classes = await _class.GetByStatuses();
            if (classes is null || classes.Count <= 0)
            {
                TempData["Error"] = "No class found. Please add class first.";
                return RedirectToAction("Classes", "Class");
            }
            ViewBag.Classes = classes;

            var users = await _user.GetByStatuses();
            if (users is null || users.Count <= 0)
            {
                TempData["Error"] = "No user found. Please add user first.";
                return RedirectToAction("Users", "User");
            }
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EnrollmentCreateRequest request)
        {
            request.CreatedBy = 1;
            request.Status = 1;
            var enrollment = await _enrollment.Create(request);
            if (enrollment.code == HttpStatusCode.OK)
            {
                TempData["Success"] = enrollment.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
            else
            {
                TempData["Error"] = enrollment.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollment.GetById(id);
            if (enrollment is null)
            {
                TempData["Error"] = "Enrollment not found.";
                return RedirectToAction("Enrollments", "Enrollment");
            }

            var classes = await _class.GetByStatuses();
            if (classes is null || classes.Count <= 0)
            {
                TempData["Error"] = "No class found. Please add class first.";
                return RedirectToAction("Classes", "Class");
            }
            ViewBag.Classes = classes;

            var users = await _user.GetByStatuses();
            if (users is null || users.Count <= 0)
            {
                TempData["Error"] = "No user found. Please add user first.";
                return RedirectToAction("Users", "User");
            }
            ViewBag.Users = users;

            var enrollmentUpdateRequest = new EnrollmentUpdateRequest
            {
                Id = enrollment.Id,
                UserID = enrollment.UserID,
                ClassID = enrollment.ClassID,
                EnrollmentDate = enrollment.EnrollmentDate,
                Status = enrollment.Status
            };

            return View(enrollmentUpdateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EnrollmentUpdateRequest request)
        {
            request.UpdatedBy = 1;
            request.Status = 1;
            var enrollment = await _enrollment.Update(request);
            if (enrollment.code == HttpStatusCode.OK)
            {
                TempData["Success"] = enrollment.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
            else
            {
                TempData["Error"] = enrollment.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollmentDeleteRequest = new EnrollmentDeleteRequest
            {
                Id = id,
                DeletedBy = 1,
                Status = 3
            };

            var response = await _enrollment.Delete(enrollmentDeleteRequest);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
            else
            {
                TempData["Error"] = response.result;
                return RedirectToAction("Enrollments", "Enrollment");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListOfClasses(CancellationToken cancellationToken)
        {
            var _Classes = await _class.GetByStatuses(cancellationToken);
            return View(_Classes);
        }

        [HttpGet]
        public async Task<IActionResult> EnrolledUserOfClass(int id)
        {
            var response = await _enrollment.GetAllEnrolledUserByClassId(id);
            if (response is null)
            {
                TempData["Error"] = "No user found.";
                return RedirectToAction("Enrollments", "Enrollment");
            }
            else
                return View(response);
        }
    }
}
