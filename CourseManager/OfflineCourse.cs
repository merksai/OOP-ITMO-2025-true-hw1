using System;

namespace CourseManager
{
    public class OfflineCourse : Course
    {
        public string Classroom { get; }
        public string Building { get; }

        public OfflineCourse(string title, string classroom, string building) : base(title)
        {
            Classroom = classroom;
            Building = building;
        }

        public override string GetCourseTypeName()
        {
            return "Офлайн-курс";
        }
    }
}
