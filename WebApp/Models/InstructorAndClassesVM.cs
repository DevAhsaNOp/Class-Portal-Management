using Application.ClientFeatures.Class.Response;
using Application.ClientFeatures.Instructor.Response;

namespace WebApp.Models
{
    public class InstructorAndClassesVM
    {
        public List<ClassResponse> Classes { get; set; }
        public List<InstructorResponseV2> Instructors { get; set; }
    }
}
