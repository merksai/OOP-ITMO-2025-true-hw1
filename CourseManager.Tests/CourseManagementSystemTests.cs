using System;
using CourseManager;
using Xunit;

namespace CourseManager.Tests
{
    public class CourseManagementSystemTests
    {
        [Fact]
        public void AddCourse_ShouldAddCourseToSystem()
        {
            var system = new CourseManagementSystem();
            var course = new OnlineCourse("C# ООП", "Moodle", "https://example.com/csharp");

            system.AddCourse(course);

            Assert.Contains(course, system.Courses);
        }

        [Fact]
        public void RemoveCourse_ShouldRemoveCourseFromSystem()
        {
            var system = new CourseManagementSystem();
            var course = new OfflineCourse("Алгоритмы", "101", "Главный корпус");
            system.AddCourse(course);

            var removed = system.RemoveCourse(course.Id);

            Assert.True(removed);
            Assert.DoesNotContain(course, system.Courses);
        }

        [Fact]
        public void RemoveCourse_ShouldReturnFalse_WhenCourseNotFound()
        {
            var system = new CourseManagementSystem();

            var removed = system.RemoveCourse(Guid.NewGuid());

            Assert.False(removed);
        }

        [Fact]
        public void AssignTeacherToCourse_ShouldSetTeacher()
        {
            var system = new CourseManagementSystem();
            var course = new OnlineCourse("C# ООП", "Moodle", "https://example.com/csharp");
            var teacher = new Teacher("Иванов Иван");
            system.AddCourse(course);

            system.AssignTeacherToCourse(course.Id, teacher);

            Assert.Equal(teacher, course.Teacher);
        }

        [Fact]
        public void EnrollStudentToCourse_ShouldAddStudent()
        {
            var system = new CourseManagementSystem();
            var course = new OfflineCourse("Алгоритмы", "101", "Главный корпус");
            var student = new Student("Петров Пётр");
            system.AddCourse(course);

            system.EnrollStudentToCourse(course.Id, student);
            var students = system.GetStudentsOfCourse(course.Id);

            Assert.Contains(student, students);
        }

        [Fact]
        public void GetStudentsOfCourse_ShouldReturnEmptyList_WhenCourseNotFound()
        {
            var system = new CourseManagementSystem();

            var students = system.GetStudentsOfCourse(Guid.NewGuid());

            Assert.Empty(students);
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnOnlyCoursesOfThatTeacher()
        {
            var system = new CourseManagementSystem();

            var teacher1 = new Teacher("Иванов Иван");
            var teacher2 = new Teacher("Сидоров Сидор");

            var course1 = new OnlineCourse("C# ООП", "Moodle", "https://example.com/csharp");
            var course2 = new OfflineCourse("Алгоритмы", "101", "Главный корпус");
            var course3 = new OnlineCourse("Java ООП", "Moodle", "https://example.com/java");

            system.AddCourse(course1);
            system.AddCourse(course2);
            system.AddCourse(course3);

            system.AssignTeacherToCourse(course1.Id, teacher1);
            system.AssignTeacherToCourse(course2.Id, teacher1);
            system.AssignTeacherToCourse(course3.Id, teacher2);

            var teacher1Courses = system.GetCoursesByTeacher(teacher1.Id);

            Assert.Equal(2, teacher1Courses.Count);
            Assert.Contains(course1, teacher1Courses);
            Assert.Contains(course2, teacher1Courses);
            Assert.DoesNotContain(course3, teacher1Courses);
        }
    }
}
