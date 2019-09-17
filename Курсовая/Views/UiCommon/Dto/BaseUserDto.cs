using Курсовая.Common;

namespace Курсовая.Views.UiCommon.Dto
{
    public class BaseUserDto : BaseUser
    {
        public Sex Sex { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
