namespace Application.Features.GenericFeatures
{
    public static class AppSetting
    {

        public static string Company { get; set; }
        public static string ApplicationUrl { get; set; }
        public static string DocumentPath { get; set; }
        public static string DocumentUrl { get; set; }
        public static string TemplatePath { get; set; }
        public static string ClientDbUser { get; set; }
        public static string ClientDbPassword { get; set; }
        public static string ClientDbServer { get; set; }
    }

    public static class SmptSetting
    {

        public static string smtpHost { get; set; }
        public static string smtpUser { get; set; }
        public static string smptPassword { get; set; }
        public static string smtpPort { get; set; }
        public static string noReplyEmail { get; set; }
    }
}
