using StudentManagementSystem.Delegates;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Services
{
    // Сервис для работы со студентами с использованием делегатов
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students)
        {
            _students = students;
        }

        // Многоадресный делегат для выполнения операций над студентами
        public void ExecuteStudentOperations(StudentOperationDelegate operations, StudentConditionDelegate condition = null)
        {
            var targetStudents = condition == null ? _students : _students.Where(s => condition(s)).ToList();
            
            foreach (var student in targetStudents)
            {
                operations(student);
            }
        }

        // Метод демонстрации многоадресности делегатов
        public void DemonstrateMulticastDelegate(Student student)
        {
            // Создаем многоадресный делегат с тремя методами
            StudentOperationDelegate multiOperation = DisplayBasicInfo;
            multiOperation += DisplayAcademicInfo;
            multiOperation += DisplayReport;
            
            Console.WriteLine("=== МНОГОАДРЕСНЫЙ ДЕЛЕГАТ В ДЕЙСТВИИ ===");
            multiOperation(student); // Вызов всех трех методов одним вызовом делегата
            Console.WriteLine("=======================================");
        }

        // Методы для делегатов
        private void DisplayBasicInfo(Student student)
        {
            Console.WriteLine($"📋 БАЗОВАЯ ИНФОРМАЦИЯ:");
            Console.WriteLine($"   Студент: {student.Surname}");
            Console.WriteLine($"   Группа: {student.Group}, Курс: {student.Year}");
        }

        private void DisplayAcademicInfo(Student student)
        {
            var iStudent = (IStudentActions)student;
            Console.WriteLine($"📊 АКАДЕМИЧЕСКАЯ ИНФОРМАЦИЯ:");
            Console.WriteLine($"   Средний балл: {iStudent.CalculateAverageGrade()}");
            Console.WriteLine($"   Отличник: {iStudent.IsExcellentStudent()}");
            Console.WriteLine($"   Предметов: {((IAcademicOperations)student).GetSubjectCount()}");
        }

        private void DisplayReport(Student student)
        {
            var reportable = (IReportable)student;
            Console.WriteLine($"📄 ОТЧЕТ:");
            Console.WriteLine($"   Может выпускаться: {reportable.CanGraduate()}");
            
            var stats = reportable.GetAcademicStats();
            foreach (var stat in stats)
            {
                Console.WriteLine($"   {stat.Key}: {stat.Value}");
            }
        }

        // Статические методы для использования с делегатами
        public static void PromoteStudent(Student student)
        {
            if (student.Year < 5)
            {
                student.Year++;
                Console.WriteLine($"🎓 Студент {student.Surname} переведен на {student.Year} курс");
            }
        }

        public static void DisplayFullInfo(Student student)
        {
            student.DisplayInfo();
        }

        public static bool IsExcellentStudent(Student student)
        {
            return student.IsExcellentStudent();
        }

        public static bool NeedsAcademicHelp(Student student)
        {
            return student.CalculateAverageGrade() < 3.5;
        }
    }
}