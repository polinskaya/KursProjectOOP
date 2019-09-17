namespace Курсовая.Views.UiCommon.Dto
{
    public class FacultyDto
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public override string ToString()
        {
            return ShortName;
        }
    }
}
