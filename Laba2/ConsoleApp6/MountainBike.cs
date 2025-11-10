namespace Lab2;

// 3. Производный класс от Велосипеда: Горный велосипед
public class MountainBike : Bicycle//, IFoldable
{
    // Специфичное свойство для горного велосипеда(Наличие амортизационной вилки)
    public bool HasSuspension { get; set; }

    // свойство тип подвески
    public string SuspensionType { get; set; }

    // Реализация свойства из интерфейса IFoldable
    public bool IsFolded { get; set; }
    public MountainBike(string name, decimal price, string manufacturer, int gearsCount, 
        string brakeType, int maxSpeed, bool hasSuspension, string suspensionType)
        : base(name, price, manufacturer, gearsCount, brakeType, maxSpeed)
    {
        HasSuspension = hasSuspension;
        SuspensionType = suspensionType;
        IsFolded = false;
    }

    // Переопределение абстрактного метода
    public override void DisplayInfo()
    {
        Console.WriteLine($"=== ГОРНЫЙ ВЕЛОСИПЕД ===");
        base.GetCommonInfo();
        Console.WriteLine($"Амортизационная вилка: {(HasSuspension ? "есть" : "нет")}");
        if (HasSuspension)
        {
            Console.WriteLine($"Тип подвески: {SuspensionType}");
        }
        Console.WriteLine($"Сложен: {(IsFolded ? "да" : "нет")}");
    }

    // Реализация методов интерфейса IFoldable
    public void Fold()
    {
        if (!IsFolded)
        {
            IsFolded = true;
            Console.WriteLine($"{Name}: Горный велосипед сложен для транспортировки");
        }
        else
        {
            Console.WriteLine($"{Name}: Велосипед уже сложен");
        }
    }

    public void Unfold()
    {
        if (IsFolded)
        {
            IsFolded = false;
            Console.WriteLine($"{Name}: Горный велосипед готов к использованию");
        }
        else
        {
            Console.WriteLine($"{Name}: Велосипед уже готов к использованию");
        }
    }

    // Новый метод с перегрузкой (полиморфизм)
    public void Ride()
    {
        Console.WriteLine($"{Name}: Едем по горной тропе!");
    }

    public void Ride(string terrain)
    {
        Console.WriteLine($"{Name}: Едем по {terrain} с максимальным комфортом!");
    }
}