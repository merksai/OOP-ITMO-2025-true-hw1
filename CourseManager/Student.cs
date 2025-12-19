using System;

namespace CourseManager
{
    public class Student
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string FullName { get; }

        public Student(string fullName)
        {
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            if (other == null) return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
