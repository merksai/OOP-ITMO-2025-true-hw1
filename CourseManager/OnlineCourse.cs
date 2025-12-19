using System;

namespace CourseManager
{
    public class OnlineCourse : Course
    {
        public string Platform { get; }
        public string Url { get; }

        public OnlineCourse(string title, string platform, string url) : base(title)
        {
            Platform = platform;
            Url = url;
        }

        public override string GetCourseTypeName()
        {
            return "Онлайн-курс";
        }
    }
}
