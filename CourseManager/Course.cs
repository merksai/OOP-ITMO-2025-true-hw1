using System;
using System.Collections.Generic;

namespace CourseManager
{
    public abstract class Course
    {
        public Guid Id { get; }
        public string Title { get; }
        public Teacher Teacher { get; private set; }

        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        protected Course(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название курса не может быть пустым", nameof(title));

            Id = Guid.NewGuid();
            Title = title;
        }

        public void AssignTeacher(Teacher teacher)
        {
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
        }

        public void EnrollStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (!_students.Contains(student))
                _students.Add(student);
        }

        public override string ToString()
        {
            return $"{Title} ({GetCourseTypeName()})";
        }

        protected abstract string GetCourseTypeName();
    }
}
