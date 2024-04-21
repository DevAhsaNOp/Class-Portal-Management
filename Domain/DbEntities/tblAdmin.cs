using Domain.Common;

namespace Domain.DbEntities
{
    public sealed class tblAdmin : BaseEntity
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
