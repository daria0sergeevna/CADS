using StudentManagementSystem.Interfaces;
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

        public Student(string surname, string institute, string faculty, string group, int year)
        {
            Surname = surname;
            InstituteName = institute;
            Faculty = faculty;
            Group = group;
            Year = year;
            Subjects = new List<AcademicSubject>();
        }

        // Реализация IStudentActions
        public double CalculateAverageGrade()
        {
            if (Subjects.Count == 0) return 0.0;
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
            Console.WriteLine($"Средний балл: {CalculateAverageGrade()}");
            Console.WriteLine($"Отличник: {(IsExcellentStudent() ? "Да" : "Нет")}");
        }

        // Реализация IAcademicOperations
        public void AddSubject(string name, int grade)
        {
            if (grade < 2 || grade > 5)
                throw new ArgumentException("Оценка должна быть от 2 до 5");
            
            Subjects.Add(new AcademicSubject(name, grade));
        }

        public void RemoveSubject(string subjectName)
        {
            var subject = Subjects.FirstOrDefault(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase));
            if (subject != null)
            {
                Subjects.Remove(subject);
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
            return $"""
                АКАДЕМИЧЕСКИЙ ОТЧЕТ
                Студент: {Surname}
                Группа: {Group}, Курс: {Year}
                Средний балл: {CalculateAverageGrade()}
                Количество предметов: {Subjects.Count}
                Статус: {(IsExcellentStudent() ? "Отличник" : "Успевающий")}
                """;
        }

        public Dictionary<string, string> GetAcademicStats()
        {
            return new Dictionary<string, string>
            {
                ["ФИО"] = Surname,
                ["Группа"] = Group,
                ["Курс"] = Year.ToString(),
                ["Средний балл"] = CalculateAverageGrade().ToString("F2"),
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

        public override string ToString()
        {
            return $"{Surname} | {InstituteName} | {Group} | {Year} курс";
        }
    }
}