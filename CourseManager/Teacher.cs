using System;

namespace CourseManager
{
    public class Teacher
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string FullName { get; }

        public Teacher(string fullName)
        {
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            Teacher other = obj as Teacher;
            if (other == null) return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
