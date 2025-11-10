using StudentManagementSystem.Models;
using System;
using System.IO;

namespace StudentManagementSystem.Data
{
    // Управление файлами для загрузки и сохранения данных
    public class FileDataManager
    {
        private readonly string _dataFilePath;
        private readonly string _outputFilePath;

        public FileDataManager(string dataFile = "students.txt", string outputFile = "results.txt")
        {
            _dataFilePath = dataFile;
            _outputFilePath = outputFile;
        }

        // Загрузка данных студентов из файла
        public StudentDatabase LoadStudentsData()
        {
            var database = new StudentDatabase();
            
            if (!File.Exists(_dataFilePath))
            {
                Console.WriteLine($"Файл {_dataFilePath} не найден. Будет создан новый.");
                return database;
            }

            try
            {
                using var reader = new StreamReader(_dataFilePath);
                string line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    
                    var studentData = line.Split('|');
                    if (studentData.Length >= 5)
                    {
                        var student = new Student(
                            studentData[0].Trim(),
                            studentData[1].Trim(),
                            studentData[2].Trim(),
                            studentData[3].Trim(),
                            int.Parse(studentData[4].Trim())
                        );

                        // Чтение предметов студента
                        while (!string.IsNullOrEmpty(line = reader.ReadLine()) && line != "---")
                        {
                            var subjectData = line.Split(':');
                            if (subjectData.Length == 2)
                            {
                                student.AddSubject(new AcademicSubject(
                                    subjectData[0].Trim(),
                                    int.Parse(subjectData[1].Trim())
                                ));
                            }
                        }
                        
                        database.AddStudent(student);
                    }
                }
                Console.WriteLine($"Данные загружены из {_dataFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            }
            
            return database;
        }

        // Сохранение данных студентов в файл
        public void SaveStudentsData(StudentDatabase database)
        {
            try
            {
                using var writer = new StreamWriter(_dataFilePath, false);
                
                foreach (var student in database.Students)
                {
                    writer.WriteLine($"{student.Surname}|{student.InstituteName}|{student.Faculty}|{student.Group}|{student.Year}");
                    
                    foreach (var subject in student.Subjects)
                    {
                        writer.WriteLine($"{subject.Name}:{subject.Grade}");
                    }
                    
                    writer.WriteLine("---"); // Разделитель между студентами
                }
                Console.WriteLine($"Данные сохранены в {_dataFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        // Сохранение результатов запроса в файл
        public void SaveQueryResults(string results)
        {
            try
            {
                using var writer = new StreamWriter(_outputFilePath, false);
                writer.WriteLine("=== РЕЗУЛЬТАТЫ ЗАПРОСА №15 ===");
                writer.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
                writer.WriteLine();
                writer.Write(results);
                Console.WriteLine($"Результаты сохранены в {_outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения результатов: {ex.Message}");
            }
        }
    }
}