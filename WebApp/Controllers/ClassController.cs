using Application.ClientFeatures.Class.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApp.Extensions;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController(IClass _class, IInstructor _instructor) : Controller
    {
        private readonly IClass _class = _class;
        private readonly IInstructor _instructor = _instructor;

        [HttpGet]
        public async Task<IActionResult> Classes(CancellationToken cancellationToken)
        {
            var _Classes = await _class.GetByStatuses(cancellationToken);
            return View(_Classes);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var instructors = await _instructor.GetAllActive();
            ViewBag.Instructors = instructors;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClassCreateRequest classRequest)
        {
            classRequest.CreatedBy = LoggedInUserDetail.UserId;
            classRequest.Status = 1;
            var response = await _class.Create(classRequest);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Classes", "Class");
            }
            else
            {
                TempData["Error"] = response.result;
                return View(classRequest);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var classRequest = await _class.GetById(id);
            if (classRequest == null)
            {
                TempData["Error"] = "Class not found";
                return RedirectToAction("Classes", "Class");
            }
            else
            {
                var instructors = await _instructor.GetAllActive();
                ViewBag.Instructors = instructors;

                var classUpdateRequest = new ClassUpdateRequest
                {
                    AgeGroups = classRequest.AgeGroups,
                    ClassName = classRequest.ClassName,
                    Description = classRequest.Description,
                    GradeLevel = classRequest.GradeLevel,
                    EndTiming = classRequest.EndTiming,
                    StartTiming = classRequest.StartTiming,
                    Id = classRequest.Id,
                    Fees = classRequest.Fees,
                    ProfileImage = classRequest.Image,
                    InstructorID = classRequest.InstructorID,
                    MaxClassSize = classRequest.MaxClassSize,
                    Status = classRequest.Status,
                };
                return View(classUpdateRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClassUpdateRequest request)
        {
            request.UpdatedBy = LoggedInUserDetail.UserId;
            request.Status = 1;
            var response = await _class.Update(request);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Classes", "Class");
            }
            else
            {
                TempData["Error"] = response.result;
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var classDeleteRequest = new ClassDeleteRequest
            {
                Id = id,
                DeletedBy = LoggedInUserDetail.UserId,
                Status = 3
            };
            var response = await _class.Delete(classDeleteRequest);

            if (response.code == HttpStatusCode.OK)
                TempData["Success"] = response.result;
            else
                TempData["Error"] = response.result;

            return RedirectToAction("Classes", "Class");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var classRequest = await _class.GetById(id);
            if (classRequest == null)
            {
                TempData["Error"] = "Class not found";
                return RedirectToAction("Classes", "Class");
            }
            else
            {
                return View(classRequest);
            }
        }
    }
}
