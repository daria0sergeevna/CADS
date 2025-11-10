using System;

namespace StudentManagementSystem.Models.Exceptions
{
    // Базовый класс для пользовательских исключений системы управления студентами
    public class StudentManagementException : Exception
    {
        public string ErrorCode { get; }
        public DateTime Timestamp { get; }

        public StudentManagementException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {ErrorCode}: {Message}";
        }
    }

    // Исключение для ошибок переполнения стека
    public class StudentStackOverflowException : StudentManagementException
    {
        public StudentStackOverflowException(string operation) 
            : base($"Переполнение стека при выполнении операции: {operation}", "STACK_OVERFLOW")
        {
        }
    }

    // Исключение для несоответствия типов массива
    public class StudentArrayTypeMismatchException : StudentManagementException
    {
        public StudentArrayTypeMismatchException(string expectedType, string actualType) 
            : base($"Несоответствие типа массива. Ожидался: {expectedType}, получен: {actualType}", "ARRAY_TYPE_MISMATCH")
        {
        }
    }

    // Исключение для деления на ноль
    public class StudentDivideByZeroException : StudentManagementException
    {
        public StudentDivideByZeroException(string operation) 
            : base($"Попытка деления на ноль в операции: {operation}", "DIVIDE_BY_ZERO")
        {
        }
    }

    // Исключение для выхода за границы массива
    public class StudentIndexOutOfRangeException : StudentManagementException
    {
        public StudentIndexOutOfRangeException(int index, int maxIndex) 
            : base($"Индекс {index} выходит за границы массива [0, {maxIndex - 1}]", "INDEX_OUT_OF_RANGE")
        {
        }
    }

    // Исключение для недопустимого приведения типов
    public class StudentInvalidCastException : StudentManagementException
    {
        public StudentInvalidCastException(string fromType, string toType) 
            : base($"Недопустимое приведение типа: {fromType} -> {toType}", "INVALID_CAST")
        {
        }
    }

    // Исключение для нехватки памяти
    public class StudentOutOfMemoryException : StudentManagementException
    {
        public long RequestedMemory { get; }

        public StudentOutOfMemoryException(long requestedMemory) 
            : base($"Недостаточно памяти для выделения {FormatMemory(requestedMemory)}", "OUT_OF_MEMORY")
        {
            RequestedMemory = requestedMemory;
        }

        private static string FormatMemory(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double len = bytes;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }

    // Исключение для арифметического переполнения
    public class StudentOverflowException : StudentManagementException
    {
        public StudentOverflowException(string operation, string value) 
            : base($"Арифметическое переполнение в операции: {operation} со значением: {value}", "OVERFLOW")
        {
        }
    }
}