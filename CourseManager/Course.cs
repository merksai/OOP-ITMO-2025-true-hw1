using System;
using System.Collections.Generic;

namespace CourseManager
{
    public abstract class Course
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; }
        public Teacher Teacher { get; private set; }

        public List<Student> Students { get; } = new List<Student>();

        public Course(string title)
        {
            Title = title;
        }

        public void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }

        public void EnrollStudent(Student student)
        {
            if (!Students.Contains(student))
                Students.Add(student);
        }

        public override string ToString()
        {
            return Title + " (" + GetCourseTypeName() + ")";
        }

        public abstract string GetCourseTypeName();
    }
}
