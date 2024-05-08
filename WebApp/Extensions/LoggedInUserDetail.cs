namespace WebApp.Extensions
{
    public static class LoggedInUserDetail
    {
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static string Image { get; set; }
        public static string Username { get; set; }
        public static string Email { get; set; }
        public static string Role { get; set; }

        // On User Login set the User Details
        public static void SetUserDetails(int userId, string fullName, string image, string username, string email, string role)
        {
            UserId = userId;
            FullName = fullName;
            Image = image;
            Username = username;
            Email = email;
            Role = role;
        }

        // On User Logout clear the User Details
        public static void ClearUserDetails()
        {
            UserId = 0;
            FullName = string.Empty;
            Image = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
        }
    }
}
