namespace StudentManagementSystem.Models
{
    // Представляет учебный предмет с оценкой
    public class AcademicSubject
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        
        public AcademicSubject(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"{Name}: {Grade}";
        }
    }
}