using System.Collections.Generic;

namespace StudentManagementSystem.Interfaces
{
    // Интерфейс для генерации отчетов
    public interface IReportable
    {
        string GenerateReport();
        Dictionary<string, string> GetAcademicStats();
        bool CanGraduate();
    }
}