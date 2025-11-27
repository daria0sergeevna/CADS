using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Models
{
    // Класс Student реализует три интерфейса
    public class Student : IStudentActions, IAcademicOperations, IReportable
    {
        public string Surname { get; set; }
        public string InstituteName { get; set; }
        public string Faculty { get; set; }
        public string Group { get; set; }
        public int Year { get; set; }
        public List<AcademicSubject> Subjects { get; set; }

        // Событие для уведомления об изменениях
        public event EventHandler<string> StudentModified;

        public Student(string surname, string institute, string faculty, string group, int year)
        {
            Surname = surname;
            InstituteName = institute;
            Faculty = faculty;
            Group = group;
            Year = year;
            Subjects = new List<AcademicSubject>();
        }

        // Метод для вызова события
        protected virtual void OnStudentModified(string action)
        {
            StudentModified?.Invoke(this, action);
        }

        // Реализация IStudentActions
        public double CalculateAverageGrade()
        {
            if (Subjects.Count == 0) 
            {
                // Генерация пользовательского исключения
                throw new StudentDivideByZeroException("Расчет среднего балла (нет предметов)");
            }
            
            return Math.Round(Subjects.Average(subject => subject.Grade), 2);
        }

        public bool IsExcellentStudent()
        {
            return Subjects.Count > 0 && Subjects.All(subject => subject.Grade == 5);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Студент: {Surname}");
            Console.WriteLine($"Институт: {InstituteName}, Факультет: {Faculty}");
            Console.WriteLine($"Группа: {Group}, Курс: {Year}");
            
            //Обработка исключения при расчете среднего балла
            try
            {
                Console.WriteLine($"Средний балл: {CalculateAverageGrade()}");
            }
            catch (StudentDivideByZeroException)
            {
                Console.WriteLine($"Средний балл: нет предметов");
            }
            
            Console.WriteLine($"Отличник: {(IsExcellentStudent() ? "Да" : "Нет")}");
        }

        // Реализация IAcademicOperations
        public void AddSubject(string name, int grade)
        {
            if (grade < 2 || grade > 5)
                // НОВОЕ ДЛЯ ЛР5: Генерация пользовательского исключения
                throw new StudentIndexOutOfRangeException(grade, 2);
            
            if (Subjects.Count >= 10) // Ограничение на количество предметов
                // ИСПРАВЛЕНИЕ: Используем более реалистичное значение для демонстрации OutOfMemory
                throw new StudentOutOfMemoryException(1024 * 1024 * 1024); // 1 GB
            
            Subjects.Add(new AcademicSubject(name, grade));
            OnStudentModified($"Добавлен предмет: {name}");
        }

        public void RemoveSubject(string subjectName)
        {
            var subject = Subjects.FirstOrDefault(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
            if (subject != null)
            {
                Subjects.Remove(subject);
                OnStudentModified($"Удален предмет: {subjectName}");
            }
            else
            {
                // НОВОЕ ДЛЯ ЛР5: Генерация пользовательского исключения
                throw new StudentInvalidCastException("string", "AcademicSubject");
            }
        }

        public bool HasSubject(string subjectName)
        {
            return Subjects.Any(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
        }

        public int GetSubjectCount()
        {
            return Subjects.Count;
        }

        // Реализация IReportable
        public string GenerateReport()
        {
            double averageGrade;
            //Обработка исключения при расчете среднего балла
            try
            {
                averageGrade = CalculateAverageGrade();
            }
            catch (StudentDivideByZeroException)
            {
                averageGrade = 0.0;
            }

            return $"""
                АКАДЕМИЧЕСКИЙ ОТЧЕТ
                Студент: {Surname}
                Группа: {Group}, Курс: {Year}
                Средний балл: {averageGrade:F2}
                Количество предметов: {Subjects.Count}
                Статус: {(IsExcellentStudent() ? "Отличник" : "Успевающий")}
                """;
        }

        public Dictionary<string, string> GetAcademicStats()
        {
            double averageGrade;
            // НОВОЕ ДЛЯ ЛР5: Обработка исключения при расчете среднего балла
            try
            {
                averageGrade = CalculateAverageGrade();
            }
            catch (StudentDivideByZeroException)
            {
                averageGrade = 0.0;
            }

            return new Dictionary<string, string>
            {
                ["ФИО"] = Surname,
                ["Группа"] = Group,
                ["Курс"] = Year.ToString(),
                ["Средний балл"] = averageGrade.ToString("F2"),
                ["Количество предметов"] = Subjects.Count.ToString(),
                ["Лучшая оценка"] = Subjects.Count > 0 ? Subjects.Max(s => s.Grade).ToString() : "Нет",
                ["Худшая оценка"] = Subjects.Count > 0 ? Subjects.Min(s => s.Grade).ToString() : "Нет"
            };
        }

        // Проверяет, может ли студент выпуститься
        public bool CanGraduate()
        {
            return Subjects.Count >= 3 && CalculateAverageGrade() >= 3.0 && !Subjects.Any(s => s.Grade == 2);
        }

        // Метод для тестирования исключений с массивом
        public void TestArrayOperations()
        {
            try
            {
                // Имитация работы с массивом
                AcademicSubject[] subjectArray = new AcademicSubject[3];
                subjectArray[0] = new AcademicSubject("Математика", 5);
                
                // Попытка несоответствия типов
                object[] objArray = subjectArray;
                objArray[1] = "Неправильный тип"; // Это вызовет исключение
            }
            catch (ArrayTypeMismatchException ex)
            {
                throw new StudentArrayTypeMismatchException("AcademicSubject", "string");
            }
        }

        // Метод для тестирования арифметического переполнения
        public void TestArithmeticOperations()
        {
            try
            {
                int maxGrade = 5;
                int total = 0;
                
                foreach (var subject in Subjects)
                {
                    total = checked(total + subject.Grade * 1000000); // Может вызвать переполнение
                }
            }
            catch (OverflowException ex)
            {
                throw new StudentOverflowException("Суммирование оценок", "large number");
            }
        }

        public override string ToString()
        {
            return $"{Surname} | {InstituteName} | {Group} | {Year} курс";
        }
    }
}