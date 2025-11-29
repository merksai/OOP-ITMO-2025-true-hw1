using System;

namespace CourseManager
{
    public class OnlineCourse : Course
    {
        public string Platform { get; }
        public string Url { get; }

        public OnlineCourse(string title, string platform, string url)
            : base(title)
        {
            if (string.IsNullOrWhiteSpace(platform))
                throw new ArgumentException("Платформа не может быть пустой", nameof(platform));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL не может быть пустым", nameof(url));

            Platform = platform;
            Url = url;
        }

        protected override string GetCourseTypeName() => "Онлайн-курс";
    }
}