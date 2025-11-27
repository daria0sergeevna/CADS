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

        // Создает предмет из массива данных
        public AcademicSubject(string[] inputData)
        {
            Name = inputData[0];
            Grade = int.Parse(inputData[1]);
        }

        public override string ToString()
        {
            return $"{Name}: {Grade}";
        }
    }
}