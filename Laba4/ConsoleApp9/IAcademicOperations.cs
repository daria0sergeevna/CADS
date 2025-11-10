namespace StudentManagementSystem.Interfaces
{
    // Интерфейс для академических операций
    public interface IAcademicOperations
    {
        void AddSubject(string name, int grade);
        void RemoveSubject(string subjectName);
        bool HasSubject(string subjectName);
        int GetSubjectCount();
    }
}