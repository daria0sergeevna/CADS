using StudentManagementSystem.Models.Exceptions;
using System;

namespace StudentManagementSystem.Events
{
    // Делегат для обработки событий исключений
    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

    // Аргументы события для исключений
    public class ExceptionEventArgs : EventArgs
    {
        public StudentManagementException Exception { get; }
        public string Operation { get; }
        public DateTime Timestamp { get; }

        public ExceptionEventArgs(StudentManagementException exception, string operation)
        {
            Exception = exception;
            Operation = operation;
            Timestamp = DateTime.Now;
        }

        public string GetFormattedMessage()
        {
            return $"[{Timestamp:HH:mm:ss}] Ошибка в операции '{Operation}': {Exception.Message}";
        }
    }

    // Менеджер событий исключений
    public class ExceptionEventManager
    {
        // Событие для обработки исключений
        public event ExceptionEventHandler ExceptionOccurred;

        // Метод для вызова события
        protected virtual void OnExceptionOccurred(ExceptionEventArgs e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }

        // Метод для обработки и распространения исключений
        public void HandleException(StudentManagementException exception, string operation)
        {
            var eventArgs = new ExceptionEventArgs(exception, operation);
            OnExceptionOccurred(eventArgs);
        }

        // Метод для безопасного выполнения операций с обработкой исключений
        public void ExecuteSafely(Action action, string operationName)
        {
            try
            {
                action();
            }
            catch (StudentManagementException ex)
            {
                HandleException(ex, operationName);
            }
        }

        public T ExecuteSafely<T>(Func<T> function, string operationName, T defaultValue = default(T))
        {
            try
            {
                return function();
            }
            catch (StudentManagementException ex)
            {
                HandleException(ex, operationName);
                return defaultValue;
            }
        }
    }
}