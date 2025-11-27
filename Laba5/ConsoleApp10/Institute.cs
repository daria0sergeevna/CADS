namespace StudentManagementSystem.Models
{
    // Представляет учебное заведение
    public class Institute
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        
        public Institute(string name, string faculty)
        {
            Name = name;
            Faculty = faculty;
        }

        public override string ToString()
        {
            return $"{Name} ({Faculty})";
        }
    }
}