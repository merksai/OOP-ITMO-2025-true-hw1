using System;
using System.Collections.Generic;

namespace CourseManager
{
    public class CourseManagementSystem
    {
        public List<Course> Courses { get; } = new List<Course>();

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public bool RemoveCourse(Guid courseId)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].Id == courseId)
                {
                    Courses.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        private Course GetCourse(Guid courseId)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].Id == courseId)
                    return Courses[i];
            }
            return null;
        }

        public void AssignTeacherToCourse(Guid courseId, Teacher teacher)
        {
            var course = GetCourse(courseId);
            if (course != null)
                course.AssignTeacher(teacher);
        }

        public void EnrollStudentToCourse(Guid courseId, Student student)
        {
            var course = GetCourse(courseId);
            if (course != null)
                course.EnrollStudent(student);
        }

        public List<Student> GetStudentsOfCourse(Guid courseId)
        {
            var course = GetCourse(courseId);
            if (course != null)
                return course.Students;

            return new List<Student>();
        }

        public List<Course> GetCoursesByTeacher(Guid teacherId)
        {
            var result = new List<Course>();

            for (int i = 0; i < Courses.Count; i++)
            {
                var course = Courses[i];
                if (course.Teacher != null && course.Teacher.Id == teacherId)
                    result.Add(course);
            }

            return result;
        }
    }
}
