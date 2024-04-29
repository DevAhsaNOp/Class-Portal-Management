using Application.ClientFeatures.Instructor.Request;
using Application.Interfaces.ClientInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorController(IInstructor instructor) : Controller
    {
        private readonly IInstructor _instructor = instructor;

        [HttpGet]
        public async Task<IActionResult> Instructors(CancellationToken cancellationToken)
        {
            var _Instructors = await _instructor.GetByStatuses(cancellationToken);
            return View(_Instructors);
        }

        [HttpGet]
        public async Task<IActionResult> Instructor(int id, CancellationToken cancellationToken)
        {
            var _Instructor = await _instructor.GetById(id, cancellationToken);
            return View(_Instructor);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InstructorCreateRequest instructor, CancellationToken cancellationToken)
        {
            instructor.CreatedBy = 1;
            instructor.Status = 1;
            var response = await _instructor.Create(instructor, cancellationToken);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Instructors");
            }
            else
            {
                TempData["Error"] = response.result;
                return RedirectToAction("Instructors");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var _Instructor = await _instructor.GetById(id);
            if (_Instructor == null)
            {
                TempData["Error"] = "Instructor not found";
                return RedirectToAction("Instructors");
            }
            else
            {
                var instructor = new InstructorUpdateRequest
                {
                    Id = _Instructor.Id,
                    Age = _Instructor.Age,
                    ConfirmPassword = _Instructor.Password,
                    DateOfJoined = _Instructor.DateOfJoined,
                    Email = _Instructor.Email,
                    FullName = _Instructor.FullName,
                    Gender = _Instructor.Gender,
                    ProfileImage = _Instructor.Image,
                    Qualification = _Instructor.Qualification,
                    Username = _Instructor.Username,
                    Password = _Instructor.Password,
                    Status = _Instructor.Status
                };
                return View(instructor);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorUpdateRequest instructor, CancellationToken cancellationToken)
        {
            instructor.UpdatedBy = 1;
            instructor.Status = 1;
            var response = await _instructor.Update(instructor, cancellationToken);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Instructors");
            }
            else
            {
                TempData["Error"] = response.result;
                return RedirectToAction("Instructors");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteRequest = new InstructorDeleteRequest
            {
                Id = id,
                DeletedBy = 1,
                Status = 3
            };
            var response = await _instructor.Delete(deleteRequest);
            if (response.code == HttpStatusCode.OK)
            {
                TempData["Success"] = response.result;
                return RedirectToAction("Instructors");
            }
            else
            {
                TempData["Error"] = response.result;
                return RedirectToAction("Instructors");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _instructor.GetById(id);
            if (response == null)
            {
                TempData["Error"] = "Instructor not found";
                return RedirectToAction("Instructors");
            }
            else
            {
                return View(response);
            }
        }
    }
}