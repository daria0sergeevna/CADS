using StudentManagementSystem.Models;

namespace StudentManagementSystem.Delegates
{
    // Делегат для операций со студентами
    public delegate void StudentOperationDelegate(Student student);
    
    // Делегат для проверки условий студентов
    public delegate bool StudentConditionDelegate(Student student);
    
    // Делегат для генерации отчетов
    public delegate string ReportDelegate(Student student);
}