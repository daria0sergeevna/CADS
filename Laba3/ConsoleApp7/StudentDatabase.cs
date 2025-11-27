using StudentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Data
{
    // Управляет коллекцией студентов и операциями с данными
    public class StudentDatabase
    {
        public List<Student> Students { get; private set; }

        public StudentDatabase()
        {
            Students = new List<Student>();
        }

        // Добавляет студента в базу
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        // Удаляет студента по фамилии
        public bool RemoveStudent(string surname)
        {
            var student = Students.FirstOrDefault(s => s.Surname == surname);
            if (student != null)
            {
                Students.Remove(student);
                return true;
            }
            return false;
        }

        // Возвращает отличников указанных курсов
        public List<Student> GetExcellentStudentsByYears(params int[] years)
        {
            return Students
                .Where(s => years.Contains(s.Year) && s.IsExcellentStudent())
                .ToList();
        }

        // Возвращает группы, отсортированные по среднему баллу
        public Dictionary<string, double> GetSortedGroupAverages()
        {
            var groupAverages = Students
                .GroupBy(s => s.Group)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(s => s.CalculateAverageGrade())
                );

            return groupAverages
                .OrderByDescending(kv => kv.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}