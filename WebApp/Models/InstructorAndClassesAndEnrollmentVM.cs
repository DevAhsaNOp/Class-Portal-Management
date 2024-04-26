using Application.ClientFeatures.Class.Response;
using Application.ClientFeatures.Enrollment.Response;
using Application.ClientFeatures.Instructor.Response;

namespace WebApp.Models
{
    public class InstructorAndClassesAndEnrollmentVM
    {
        public List<ClassResponse> Classes { get; set; }
        public List<EnrollmentResponse> Enrollments { get; set; }
        public List<InstructorResponseV2> Instructors { get; set; }
    }

    public class EnrollmentAndClassesVM
    {
        public List<ClassResponse> Classes { get; set; }
        public List<EnrollmentResponse> Enrollments { get; set; }
    }
}
