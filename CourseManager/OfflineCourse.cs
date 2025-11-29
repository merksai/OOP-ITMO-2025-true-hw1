using System;

namespace CourseManager
{
    public class OfflineCourse : Course
    {
        public string Classroom { get; }
        public string Building { get; }

        public OfflineCourse(string title, string classroom, string building)
            : base(title)
        {
            if (string.IsNullOrWhiteSpace(classroom))
                throw new ArgumentException("Аудитория не может быть пустой", nameof(classroom));

            if (string.IsNullOrWhiteSpace(building))
                throw new ArgumentException("Корпус не может быть пустым", nameof(building));

            Classroom = classroom;
            Building = building;
        }

        protected override string GetCourseTypeName() => "Офлайн-курс";
    }
}