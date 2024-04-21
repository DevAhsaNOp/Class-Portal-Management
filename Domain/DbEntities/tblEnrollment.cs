using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DbEntities
{
    public sealed class tblEnrollment : BaseEntity
    {
        public int UserID { get; set; }
        public int ClassID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        [ForeignKey("UserID")]
        public tblUser User { get; set; }

        [ForeignKey("ClassID")]
        public tblClass Class { get; set; }
    }
}
