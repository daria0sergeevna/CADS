using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;
using System;
using System.Linq;

namespace StudentManagementSystem
{
    // Главный класс приложения
    class Program
    {
        private static StudentDatabase _database;
        private static FileDataManager _fileManager;
        private static QueryProcessor _queryProcessor;
        private static StudentService _studentService;

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
            _studentService = new StudentService(_database.Students);
            
            // Создание тестовых данных при первом запуске
            if (_database.Students.Count == 0)
            {
                CreateSampleData();
            }

            // Демонстрация новых возможностей ЛР4
            DemonstrateInterfacesAndDelegates();
        }

        // Создание демонстрационных данных
        static void CreateSampleData()
        {
            var student1 = new Student("Токарева", "КубГУ", "ФКТиПМ", "21/1", 2);
            student1.AddSubject("Математический анализ", 5);
            student1.AddSubject("Программирование", 5);
            student1.AddSubject("Дискретная математика", 5);
            _database.AddStudent(student1);
            
            var student2 = new Student("Прохоров", "КубГУ", "ФКТиПМ", "21/1", 2);
            student2.AddSubject("Математический анализ", 5);
            student2.AddSubject("Программирование", 5);
            student2.AddSubject("Физика", 5);
            _database.AddStudent(student2);

            var student3 = new Student("Литвиненко", "КубГУ", "ФКТиПМ", "21/1", 2);
            student3.AddSubject("Математический анализ", 5);
            student3.AddSubject("Программирование", 5);
            student3.AddSubject("Дискретная математика", 5);
            _database.AddStudent(student3);

            _fileManager.SaveStudentsData(_database);
            Console.WriteLine("Созданы тестовые данные");
        }

        // Демонстрация работы интерфейсов и делегатов (новый метод для ЛР4)
        static void DemonstrateInterfacesAndDelegates()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("ДЕМОНСТРАЦИЯ ИНТЕРФЕЙСОВ И ДЕЛЕГАТОВ");
            Console.WriteLine(new string('=', 50));

            if (_database.Students.Count > 0)
            {
                var firstStudent = _database.Students[0];
                
                // Демонстрация работы с интерфейсами
                Console.WriteLine("\n1. РАБОТА С ИНТЕРФЕЙСАМИ:");
                
                Interfaces.IStudentActions studentActions = firstStudent;
                Interfaces.IAcademicOperations academicOps = firstStudent;
                Interfaces.IReportable reportable = firstStudent;

                Console.WriteLine($"IStudentActions: Средний балл = {studentActions.CalculateAverageGrade()}");
                Console.WriteLine($"IAcademicOperations: Количество предметов = {academicOps.GetSubjectCount()}");
                Console.WriteLine($"IReportable: Может выпускаться = {reportable.CanGraduate()}");

                // Демонстрация многоадресного делегата
                Console.WriteLine("\n2. МНОГОАДРЕСНЫЙ ДЕЛЕГАТ:");
                _studentService.DemonstrateMulticastDelegate(firstStudent);
            }
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
                        ExpelStudent();
                        break;
                    case "6":
                        TestInterfaces(); // Новая функция для тестирования интер фейсов
                        break;
                    case "7":
                        TestDelegates(); // Новая функция для тестирования делегатов
                        break;
                    case "8":
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
            Console.WriteLine("=== СИСТЕМА УЧЕТА СТУДЕНТОВ (ЛР4) ===");
            Console.WriteLine("1. Выполнить запрос 15");
            Console.WriteLine("2. Показать статистику");
            Console.WriteLine("3. Добавить студента");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("5. Отчислить студента");
            Console.WriteLine("6. Тестирование интерфейсов"); // Новый пункт меню
            Console.WriteLine("7. Тестирование делегатов");   // Новый пункт меню
            Console.WriteLine("8. Выход");
            Console.Write("Выберите: ");
        }

        // Новая функция для тестирования интерфейсов
        static void TestInterfaces()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ИНТЕРФЕЙСОВ ===");
            
            foreach (var student in _database.Students.Take(2))
            {
                Console.WriteLine($"\n--- Тест для {student.Surname} ---");
                
                // Тестируем все три интерфейса
                TestIStudentActions(student);
                TestIAcademicOperations(student);
                TestIReportable(student);
            }
        }

        static void TestIStudentActions(Student student)
        {
            Interfaces.IStudentActions actions = student;
            Console.WriteLine($"IStudentActions: Средний балл = {actions.CalculateAverageGrade()}, Отличник = {actions.IsExcellentStudent()}");
        }

        static void TestIAcademicOperations(Student student)
        {
            Interfaces.IAcademicOperations operations = student;
            Console.WriteLine($"IAcademicOperations: Предметов = {operations.GetSubjectCount()}");
            
            // Добавляем новый предмет через интерфейс
            operations.AddSubject("Тестовая дисциплина", 4);
            Console.WriteLine($"После добавления предмета: {operations.GetSubjectCount()}");
        }

        static void TestIReportable(Student student)
        {
            Interfaces.IReportable reportable = student;
            Console.WriteLine($"IReportable: Может выпускаться = {reportable.CanGraduate()}");
            Console.WriteLine($"Отчет:\n{reportable.GenerateReport()}");
        }

        // Новая функция для тестирования делегатов
        static void TestDelegates()
        {
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ДЕЛЕГАТОВ ===");
            
            // Создаем многоадресный делегат с тремя лямбда-выражениями
            Delegates.StudentOperationDelegate multiDelegate = student =>
            {
                Console.WriteLine($"\n🔹 Обработка студента: {student.Surname}");
            };
            
            multiDelegate += student =>
            {
                Console.WriteLine($"   Средний балл: {student.CalculateAverageGrade()}");
            };
            
            multiDelegate += student =>
            {
                Console.WriteLine($"   Статус: {(student.IsExcellentStudent() ? "Отличник" : "Обычный")}");
            };

            // Применяем делегат ко всем студентам
            Console.WriteLine("Применение многоадресного делегата ко всем студентам:");
            _studentService.ExecuteStudentOperations(multiDelegate);
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

                student.AddSubject(subjectName, grade);
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
            Console.WriteLine("Данные сохранены в файл students1.txt. Выход...");
        }
    }
}