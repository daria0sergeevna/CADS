namespace StudentManagementSystem.Interfaces
{
    // Интерфейс для действий студента
    public interface IStudentActions
    {
        string Surname { get; }
        double CalculateAverageGrade();
        bool IsExcellentStudent();
        void DisplayInfo();
    }
}