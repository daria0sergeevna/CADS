using StudentManagementSystem.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentManagementSystem.Services
{
    // Обрабатывает запросы согласно варианту 15
    public class QueryProcessor
    {
        private readonly StudentDatabase _database;

        public QueryProcessor(StudentDatabase database)
        {
            _database = database;
        }

        // Выполняет основной запрос варианта 15
        public string ExecuteQuery15()
        {
            var result = new StringBuilder();

            // 1. Отличники 1-2 курсов
            var excellentStudents = _database.GetExcellentStudentsByYears(1, 2);
            
            result.AppendLine("СТУДЕНТЫ-ОТЛИЧНИКИ 1-2 КУРСОВ:");
            result.AppendLine("==============================");
            
            if (excellentStudents.Any())
            {
                foreach (var student in excellentStudents)
                {
                    result.AppendLine($"• {student.Surname} - {student.InstituteName}, гр. {student.Group}");
                }
            }
            else
            {
                result.AppendLine("Отличники не найдены");
            }

            result.AppendLine();
            
            // 2. Средний балл по группам
            var groupAverages = _database.GetSortedGroupAverages();
            
            result.AppendLine("СРЕДНИЙ БАЛЛ ПО ГРУППАМ:");
            result.AppendLine("========================");
            
            foreach (var group in groupAverages)
            {
                result.AppendLine($"Группа {group.Key}: {group.Value:F2}");
            }

            return result.ToString();
        }

        // Возвращает статистику по базе данных
        public string GetDatabaseStats()
        {
            var stats = new StringBuilder();
            
            stats.AppendLine("СТАТИСТИКА БАЗЫ ДАННЫХ:");
            stats.AppendLine("=======================");
            stats.AppendLine($"Всего студентов: {_database.Students.Count}");
            stats.AppendLine($"Отличников: {_database.Students.Count(s => s.IsExcellentStudent())}");
            
            var studentsByYear = _database.Students.GroupBy(s => s.Year)
                .OrderBy(g => g.Key);
                
            foreach (var yearGroup in studentsByYear)
            {
                stats.AppendLine($"Курс {yearGroup.Key}: {yearGroup.Count()} студентов");
            }

            return stats.ToString();
        }
    }
}