using System;

namespace CourseManager
{
    public class Student
    {
        public Guid Id { get; }
        public string FullName { get; }

        public Student(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Имя студента не может быть пустым", nameof(fullName));

            Id = Guid.NewGuid();
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            return obj is Student other && Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}