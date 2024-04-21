using Domain.Common;

namespace Domain.DbEntities
{
    public sealed class tblInstructor : BaseEntity
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DateOfJoined { get; set; }
        public string Qualification { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<tblClass> Classes { get; set; }
    }
}
