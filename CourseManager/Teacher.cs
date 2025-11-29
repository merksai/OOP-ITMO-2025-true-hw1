using System;

namespace CourseManager
{
    public class Teacher
    {
        public Guid Id { get; }
        public string FullName { get; }

        public Teacher(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Имя преподавателя не может быть пустым", nameof(fullName));

            Id = Guid.NewGuid();
            FullName = fullName;
        }

        public override bool Equals(object obj)
        {
            return obj is Teacher other && Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}