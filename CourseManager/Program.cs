using System;
using System.Collections.Generic;

namespace CourseManager
{
    class Program
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

                if (command == "1") AddCourse();
                else if (command == "2") AddTeacher();
                else if (command == "3") AddStudent();
                else if (command == "4") AssignTeacherToCourse();
                else if (command == "5") EnrollStudentToCourse();
                else if (command == "6") ShowTeacherCourses();
                else if (command == "7") ShowCourseStudents();
                else if (command == "8") ListCourses();
                else Console.WriteLine("Неизвестная команда");

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

                system.AddCourse(new OnlineCourse(title, platform, url));
                Console.WriteLine("Курс добавлен");
            }
            else if (type == "2")
            {
                Console.Write("Аудитория: ");
                string classroom = Console.ReadLine();
                Console.Write("Корпус: ");
                string building = Console.ReadLine();

                system.AddCourse(new OfflineCourse(title, classroom, building));
                Console.WriteLine("Курс добавлен");
            }
            else
            {
                Console.WriteLine("Неверный тип курса");
            }
        }

        static void AddTeacher()
        {
            Console.Write("ФИО преподавателя: ");
            string name = Console.ReadLine();

            teachers.Add(new Teacher(name));
            Console.WriteLine("Преподаватель добавлен");
        }

        static void AddStudent()
        {
            Console.Write("ФИО студента: ");
            string name = Console.ReadLine();

            students.Add(new Student(name));
            Console.WriteLine("Студент добавлен");
        }

        static void AssignTeacherToCourse()
        {
            Course course = ChooseCourse();
            if (course == null) return;

            Teacher teacher = ChooseTeacher();
            if (teacher == null) return;

            system.AssignTeacherToCourse(course.Id, teacher);
            Console.WriteLine("Преподаватель назначен");
        }

        static void EnrollStudentToCourse()
        {
            Course course = ChooseCourse();
            if (course == null) return;

            Student student = ChooseStudent();
            if (student == null) return;

            system.EnrollStudentToCourse(course.Id, student);
            Console.WriteLine("Студент записан на курс");
        }

        static void ShowTeacherCourses()
        {
            Teacher teacher = ChooseTeacher();
            if (teacher == null) return;

            List<Course> courses = system.GetCoursesByTeacher(teacher.Id);

            if (courses.Count == 0)
            {
                Console.WriteLine("У преподавателя нет курсов");
                return;
            }

            Console.WriteLine("Курсы преподавателя " + teacher.FullName + ":");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine("- " + courses[i].Title);
        }

        static void ShowCourseStudents()
        {
            Course course = ChooseCourse();
            if (course == null) return;

            List<Student> courseStudents = system.GetStudentsOfCourse(course.Id);

            if (courseStudents.Count == 0)
            {
                Console.WriteLine("На курс никто не записан");
                return;
            }

            Console.WriteLine("Студенты курса " + course.Title + ":");
            for (int i = 0; i < courseStudents.Count; i++)
                Console.WriteLine("- " + courseStudents[i].FullName);
        }

        static void ListCourses()
        {
            List<Course> courses = system.Courses;

            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет");
                return;
            }

            Console.WriteLine("Список курсов:");
            for (int i = 0; i < courses.Count; i++)
            {
                Course c = courses[i];
                string teacherName = (c.Teacher != null) ? c.Teacher.FullName : "не назначен";
                Console.WriteLine((i + 1) + ". " + c.Title + " | Преподаватель: " + teacherName);
            }
        }

        static Course ChooseCourse()
        {
            List<Course> courses = system.Courses;

            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет");
                return null;
            }

            Console.WriteLine("Выберите курс:");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine((i + 1) + ". " + courses[i].Title);

            Console.Write("Номер: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            if (index < 1 || index > courses.Count)
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
                Console.WriteLine((i + 1) + ". " + teachers[i].FullName);

            Console.Write("Номер: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            if (index < 1 || index > teachers.Count)
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
                Console.WriteLine((i + 1) + ". " + students[i].FullName);

            Console.Write("Номер: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index))
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            if (index < 1 || index > students.Count)
            {
                Console.WriteLine("Неверный номер");
                return null;
            }

            return students[index - 1];
        }
    }
}
