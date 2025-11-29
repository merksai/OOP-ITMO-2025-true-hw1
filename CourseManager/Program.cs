using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManager
{
    internal class Program
    {
        static CourseManagementSystem system = new CourseManagementSystem();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("=== Система управления курсами ===");
                Console.WriteLine("1. Добавить курс");
                Console.WriteLine("2. Добавить преподавателя");
                Console.WriteLine("3. Добавить студента");
                Console.WriteLine("4. Назначить преподавателя на курс");
                Console.WriteLine("5. Записать студента на курс");
                Console.WriteLine("6. Показать курсы преподавателя");
                Console.WriteLine("7. Показать студентов курса");
                Console.WriteLine("8. Показать все курсы");
                Console.WriteLine("0. Выход");
                Console.Write("Команда: ");

                string command = Console.ReadLine();

                if (command == "0")
                    break;

                switch (command)
                {
                    case "1": AddCourse(); break;
                    case "2": AddTeacher(); break;
                    case "3": AddStudent(); break;
                    case "4": AssignTeacherToCourse(); break;
                    case "5": EnrollStudentToCourse(); break;
                    case "6": ShowTeacherCourses(); break;
                    case "7": ShowCourseStudents(); break;
                    case "8": ListCourses(); break;
                    default: Console.WriteLine("Неизвестная команда"); break;
                }

                Console.WriteLine();
            }
        }

        static void AddCourse()
        {
            Console.Write("Название курса: ");
            string title = Console.ReadLine();

            Console.WriteLine("Тип курса: 1 - онлайн, 2 - офлайн");
            string type = Console.ReadLine();

            if (type == "1")
            {
                Console.Write("Платформа: ");
                string platform = Console.ReadLine();
                Console.Write("URL: ");
                string url = Console.ReadLine();

                var course = new OnlineCourse(title, platform, url);
                system.AddCourse(course);
            }
            else if (type == "2")
            {
                Console.Write("Аудитория: ");
                string classroom = Console.ReadLine();
                Console.Write("Корпус: ");
                string building = Console.ReadLine();

                var course = new OfflineCourse(title, classroom, building);
                system.AddCourse(course);
            }
            else
            {
                Console.WriteLine("Неверный тип курса");
                return;
            }

            Console.WriteLine("Курс добавлен");
        }

        static void AddTeacher()
        {
            Console.Write("ФИО преподавателя: ");
            string name = Console.ReadLine();

            var teacher = new Teacher(name);
            teachers.Add(teacher);

            Console.WriteLine("Преподаватель добавлен");
        }

        static void AddStudent()
        {
            Console.Write("ФИО студента: ");
            string name = Console.ReadLine();

            var student = new Student(name);
            students.Add(student);

            Console.WriteLine("Студент добавлен");
        }

        static void AssignTeacherToCourse()
        {
            var course = ChooseCourse();
            if (course == null) return;

            var teacher = ChooseTeacher();
            if (teacher == null) return;

            system.AssignTeacherToCourse(course.Id, teacher);
            Console.WriteLine("Преподаватель назначен");
        }

        static void EnrollStudentToCourse()
        {
            var course = ChooseCourse();
            if (course == null) return;

            var student = ChooseStudent();
            if (student == null) return;

            system.EnrollStudentToCourse(course.Id, student);
            Console.WriteLine("Студент записан на курс");
        }

        static void ShowTeacherCourses()
        {
            var teacher = ChooseTeacher();
            if (teacher == null) return;

            var courses = system.GetCoursesByTeacher(teacher.Id);

            if (courses.Count == 0)
            {
                Console.WriteLine("У преподавателя нет курсов");
                return;
            }

            Console.WriteLine("Курсы преподавателя " + teacher.FullName + ":");
            foreach (var c in courses)
                Console.WriteLine("- " + c.Title);
        }

        static void ShowCourseStudents()
        {
            var course = ChooseCourse();
            if (course == null) return;

            var courseStudents = system.GetStudentsOfCourse(course.Id);

            if (courseStudents.Count == 0)
            {
                Console.WriteLine("На курс никто не записан");
                return;
            }

            Console.WriteLine("Студенты курса " + course.Title + ":");
            foreach (var s in courseStudents)
                Console.WriteLine("- " + s.FullName);
        }

        static void ListCourses()
        {
            var courses = system.Courses.ToList();
            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет");
                return;
            }

            Console.WriteLine("Список курсов:");
            for (int i = 0; i < courses.Count; i++)
            {
                var c = courses[i];
                string teacherName = c.Teacher != null ? c.Teacher.FullName : "не назначен";
                Console.WriteLine($"{i + 1}. {c.Title} | Преподаватель: {teacherName}");
            }
        }

        static Course ChooseCourse()
        {
            var courses = system.Courses.ToList();
            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет");
                return null;
            }

            Console.WriteLine("Выберите курс:");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i + 1}. {courses[i].Title}");

            Console.Write("Номер: ");
            if (!int.TryParse(Console.ReadLine(), out int index) ||
                index < 1 || index > courses.Count)
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            return courses[index - 1];
        }

        static Teacher ChooseTeacher()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("Преподавателей нет");
                return null;
            }

            Console.WriteLine("Выберите преподавателя:");
            for (int i = 0; i < teachers.Count; i++)
                Console.WriteLine($"{i + 1}. {teachers[i].FullName}");

            Console.Write("Номер: ");
            if (!int.TryParse(Console.ReadLine(), out int index) ||
                index < 1 || index > teachers.Count)
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            return teachers[index - 1];
        }

        static Student ChooseStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Студентов нет");
                return null;
            }

            Console.WriteLine("Выберите студента:");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}. {students[i].FullName}");

            Console.Write("Номер: ");
            if (!int.TryParse(Console.ReadLine(), out int index) ||
                index < 1 || index > students.Count)
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            return students[index - 1];
        }
    }
}