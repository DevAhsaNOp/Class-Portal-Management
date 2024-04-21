using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Application.Common.Behaviors
{
    public class Statuses
    {
        public enum SystemStatus
        {
            [Display(Name = "Active")]
            Active = 1,
            [Display(Name = "InActive")]
            InActive = 2,
            [Display(Name = "Deleted")]
            Deleted = 3
        }

        public static string GetSystemStatusDisplayName(int status)
        {
            switch (status)
            {
                case (int)SystemStatus.Active:
                    return GetDisplayName(SystemStatus.Active);
                case (int)SystemStatus.InActive:
                    return GetDisplayName(SystemStatus.InActive);
                case (int)SystemStatus.Deleted:
                    return GetDisplayName(SystemStatus.Deleted);
                default:
                    return "N/A";
            }
        }

        public static string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public enum TemplatesName
        {
            PASSWORD_RESET = 1,
            PASSWORD_CHANGED
        }
    }
}
