using StudentManagementSystem.Events;
using StudentManagementSystem.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.Services
{
    // Сервис для обработки исключений с использованием событий
    public class ExceptionHandlingService : ExceptionEventManager
    {
        private readonly List<ExceptionEventArgs> _exceptionLog;

        public ExceptionHandlingService()
        {
            _exceptionLog = new List<ExceptionEventArgs>();
            
            // Подписываемся на собственное событие для логирования
            ExceptionOccurred += LogException;
            ExceptionOccurred += DisplayException;
        }

        // Метод для логирования исключений
        private void LogException(object sender, ExceptionEventArgs e)
        {
            _exceptionLog.Add(e);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[LOG] {e.GetFormattedMessage()}");
            Console.ResetColor();
        }

        // Метод для отображения исключений
        private void DisplayException(object sender, ExceptionEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {e.Exception.ErrorCode}: {e.Exception.Message}");
            Console.ResetColor();
        }

        // Методы для генерации различных типов исключений

        public void TestStackOverflowException()
        {
            ExecuteSafely(() =>
            {
                // Имитация переполнения стека через рекурсию
                RecursiveMethod(0);
            }, "Рекурсивный расчет");
        }

        private void RecursiveMethod(int depth)
        {
            if (depth > 1000) // Искусственное ограничение для демонстрации
            {
                throw new StudentStackOverflowException("Рекурсивный расчет глубины");
            }
            RecursiveMethod(depth + 1);
        }

        public void TestArrayTypeMismatchException()
        {
            ExecuteSafely(() =>
            {
                object[] array = new string[5];
                array[0] = 123; // Попытка записи int в string[]
            }, "Работа с массивом");
        }

        public void TestDivideByZeroException()
        {
            ExecuteSafely(() =>
            {
                int a = 10, b = 0;
                int result = a / b;
            }, "Деление чисел");
        }

        public void TestIndexOutOfRangeException()
        {
            ExecuteSafely(() =>
            {
                int[] array = new int[5];
                int value = array[10]; // Выход за границы массива
            }, "Обращение к элементу массива");
        }

        public void TestInvalidCastException()
        {
            ExecuteSafely(() =>
            {
                object obj = "строка";
                int number = (int)obj; // Недопустимое приведение типа
            }, "Приведение типов");
        }

        public void TestOutOfMemoryException()
        {
            ExecuteSafely(() =>
            {
                // Создаем студента и пытаемся добавить слишком много предметов
                var student = new Models.Student("Тестовый", "Институт", "Факультет", "Группа", 1);
                for (int i = 0; i < 20; i++)
                {
                    student.AddSubject($"Предмет {i}", 5);
                }
            }, "Добавление предметов студенту");
        }

        public void TestOverflowException()
        {
            ExecuteSafely(() =>
            {
                int max = int.MaxValue;
                int result = checked(max + 1); // Арифметическое переполнение
            }, "Арифметическая операция");
        }

        // Метод для получения статистики исключений
        public void DisplayExceptionStats()
        {
            Console.WriteLine("\n=== СТАТИСТИКА ИСКЛЮЧЕНИЙ ===");
            Console.WriteLine($"Всего исключений: {_exceptionLog.Count}");
            
            var groupedExceptions = _exceptionLog
                .GroupBy(e => e.Exception.ErrorCode)
                .OrderByDescending(g => g.Count());
            
            foreach (var group in groupedExceptions)
            {
                Console.WriteLine($"{group.Key}: {group.Count()} раз");
            }

            if (_exceptionLog.Count > 0)
            {
                Console.WriteLine("\nПоследние 3 исключения:");
                var lastExceptions = _exceptionLog
                    .Skip(Math.Max(0, _exceptionLog.Count - 3))
                    .Take(3);
                    
                foreach (var exception in lastExceptions)
                {
                    Console.WriteLine($"  {exception.GetFormattedMessage()}");
                }
            }
        }

        // Метод для очистки лога исключений
        public void ClearLog()
        {
            _exceptionLog.Clear();
            Console.WriteLine("Лог исключений очищен.");
        }
    }
}