using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Models
{
    // Представляет студента с академической информацией
    public class Student
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

        // Проверка, является ли студент отличником
        public bool IsExcellentStudent()
        {
            return Subjects.All(subject => subject.Grade == 5);
        }

        // Вычисление среднего балла
        public double CalculateAverageGrade()
        {
            if (Subjects.Count == 0) return 0.0;
            return Subjects.Average(subject => subject.Grade);
        }

        // Добавление предмета
        public void AddSubject(AcademicSubject subject)
        {
            Subjects.Add(subject);
        }

        public override string ToString()
        {
            return $"{Surname} | {InstituteName} | {Group} | {Year} курс";
        }
    }
}