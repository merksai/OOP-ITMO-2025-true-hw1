using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManager
{
    public class CourseManagementSystem
    {
        private readonly List<Course> _courses = new List<Course>();

        public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

        public void AddCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (_courses.Any(c => c.Id == course.Id))
                throw new InvalidOperationException("Курс с таким Id уже существует");

            _courses.Add(course);
        }

        public bool RemoveCourse(Guid courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return false;

            _courses.Remove(course);
            return true;
        }

        private Course GetCourseOrThrow(Guid courseId)
        {
            var course = _courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                throw new InvalidOperationException("Курс не найден");
            return course;
        }

        public void AssignTeacherToCourse(Guid courseId, Teacher teacher)
        {
            var course = GetCourseOrThrow(courseId);
            course.AssignTeacher(teacher);
        }

        public void EnrollStudentToCourse(Guid courseId, Student student)
        {
            var course = GetCourseOrThrow(courseId);
            course.EnrollStudent(student);
        }

        public IReadOnlyCollection<Student> GetStudentsOfCourse(Guid courseId)
        {
            var course = GetCourseOrThrow(courseId);
            return course.Students;
        }

        public IReadOnlyCollection<Course> GetCoursesByTeacher(Guid teacherId)
        {
            var result = _courses
                .Where(c => c.Teacher != null && c.Teacher.Id == teacherId)
                .ToList();

            return result.AsReadOnly();
        }
    }
}