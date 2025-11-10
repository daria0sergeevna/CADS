using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System;
using System.Linq;

namespace StudentManagementSystem
{
    class Program
    {
        private static StudentDatabase _database;
        private static FileDataManager _fileManager;
        private static QueryProcessor _queryProcessor;

        static void Main(string[] args)
        {
            Initialize();
            RunApplication();
        }

        // Инициализация компонентов приложения
        static void Initialize()
        {
            _fileManager = new FileDataManager();
            _database = _fileManager.LoadStudentsData();
            _queryProcessor = new QueryProcessor(_database);
            
            // Создание тестовых данных при первом запуске
            if (_database.Students.Count == 0)
            {
                CreateSampleData();
            }
        }

        // Создание демонстрационных данных
        static void CreateSampleData()
        {
            var student1 = new Student("Токарева", "КубГУ", "ФКТиПМ", "21/1", 2);
            student1.AddSubject(new AcademicSubject("Математический анализ", 5));
            student1.AddSubject(new AcademicSubject("Программирование", 5));
            student1.AddSubject(new AcademicSubject("Дискретная математика", 5));
            student1.AddSubject(new AcademicSubject("Дифференциальные уравнения", 5));
            _database.AddStudent(student1);
            
            var student2 = new Student("Прохоров", "КубГУ", "ФКТиПМ", "21/1", 2);
            student2.AddSubject(new AcademicSubject("Математический анализ", 5));
            student2.AddSubject(new AcademicSubject("Программирование", 5));
            student2.AddSubject(new AcademicSubject("Дискретная математика", 5));
            student2.AddSubject(new AcademicSubject("Дифференциальные уравнения", 5));
            _database.AddStudent(student2);

            var student3 = new Student("Литвиненко", "КубГУ", "ФКТиПМ", "21/1", 2);
            student3.AddSubject(new AcademicSubject("Математический анализ", 5));
            student3.AddSubject(new AcademicSubject("Программирование", 5));
            student3.AddSubject(new AcademicSubject("Дискретная математика", 5));
            student3.AddSubject(new AcademicSubject("Дифференциальные уравнения", 5));
            _database.AddStudent(student3);
            
            var student4 = new Student("Степуленко", "КубГУ", "ФКТиПМ", "21/1", 2);
            student4.AddSubject(new AcademicSubject("Математический анализ", 5));
            student4.AddSubject(new AcademicSubject("Программирование", 5));
            student4.AddSubject(new AcademicSubject("Дискретная математика", 5));
            student4.AddSubject(new AcademicSubject("Дифференциальные уравнения", 5));
            _database.AddStudent(student4);

            _fileManager.SaveStudentsData(_database);
            Console.WriteLine("Созданы тестовые данные");
        }

        // Главный цикл приложения
        static void RunApplication()
        {
            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ExecuteQuery15();
                        break;
                    case "2":
                        ShowStatistics();
                        break;
                    case "3":
                        AddNewStudent();
                        break;
                    case "4":
                        ShowAllStudents();
                        break;
                    case "5":
                        ExpelStudent(); // функция отчисления
                        break;
                    case "6":
                        SaveAndExit();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        // Отображение главного меню
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== СИСТЕМА УЧЕТА СТУДЕНТОВ ===");
            Console.WriteLine("1. Выполнить запрос 15");
            Console.WriteLine("2. Показать статистику");
            Console.WriteLine("3. Добавить студента");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("5. Отчислить студента");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите: ");
        }

        // Выполнение запроса варианта 15
        static void ExecuteQuery15()
        {
            var results = _queryProcessor.ExecuteQuery15();
            Console.WriteLine("\n" + results);
            _fileManager.SaveQueryResults(results);
            Console.WriteLine("\nРезультаты сохранены в файл results.txt");
        }

        // Отображение статистики базы данных
        static void ShowStatistics()
        {
            var stats = _queryProcessor.GetDatabaseStats();
            Console.WriteLine("\n" + stats);
        }

        // Добавление нового студента с предметами
        static void AddNewStudent()
        {
            Console.WriteLine("\nДОБАВЛЕНИЕ СТУДЕНТА:");
            Console.Write("Фамилия: ");
            var surname = Console.ReadLine();

            Console.Write("Институт: ");
            var institute = Console.ReadLine();

            Console.Write("Факультет: ");
            var faculty = Console.ReadLine();

            Console.Write("Группа: ");
            var group = Console.ReadLine();

            Console.Write("Курс: ");
            var year = int.Parse(Console.ReadLine());

            var student = new Student(surname, institute, faculty, group, year);

            // Добавление предметов студенту
            Console.Write("Сколько предметов добавить? ");
            int subjectCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < subjectCount; i++)
            {
                Console.Write($"Название предмета {i + 1}: ");
                string subjectName = Console.ReadLine();

                Console.Write($"Оценка по {subjectName}: ");
                int grade = int.Parse(Console.ReadLine());

                student.AddSubject(new AcademicSubject(subjectName, grade));
            }

            _database.AddStudent(student);
            Console.WriteLine($"Студент {surname} успешно добавлен!");
        }

        // Отображение всех студентов
        static void ShowAllStudents()
        {
            Console.WriteLine("\nВСЕ СТУДЕНТЫ:");
            foreach (var student in _database.Students)
            {
                Console.WriteLine(student);
                Console.WriteLine($"  Средний балл: {student.CalculateAverageGrade():F2}");
                Console.WriteLine("  Предметы: " + string.Join(", ", student.Subjects));
            }
        }

        // Отчисление студента по фамилии
        static void ExpelStudent()
        {
            Console.WriteLine("\nОТЧИСЛЕНИЕ СТУДЕНТА:");
            Console.Write("Введите фамилию студента для отчисления: ");
            string surname = Console.ReadLine();

            if (_database.RemoveStudent(surname))
            {
                Console.WriteLine($"Студент {surname} отчислен.");
            }
            else
            {
                Console.WriteLine($"Студент {surname} не найден.");
            }
        }

        // Сохранение данных и выход из приложения
        static void SaveAndExit()
        {
            _fileManager.SaveStudentsData(_database);
            Console.WriteLine("Данные сохранены в файл students.txt. Выход...");
        }
    }
}