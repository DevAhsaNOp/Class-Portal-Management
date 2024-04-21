using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DbEntities
{
    public sealed class tblClass : BaseEntity
    {
        public string ClassName { get; set; }
        public int GradeLevel { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string AgeGroups { get; set; }
        public decimal Fees { get; set; }
        public DateTime StartTiming { get; set; }
        public DateTime EndTiming { get; set; }
        public int MaxClassSize { get; set; }
        public int InstructorID { get; set; }

        [ForeignKey("InstructorID")]
        public tblInstructor Instructor { get; set; }

        public ICollection<tblEnrollment> Enrollments { get; set; }
    }
}
