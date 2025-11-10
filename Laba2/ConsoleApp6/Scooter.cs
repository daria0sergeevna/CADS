namespace Lab2 ;

// 4. Другой производный класс от Товара: Самокат
public class Scooter : Product//, ITransport, IFoldable
{
    // Специфичное свойство для самоката(складной или нет)
    public bool IsFolding { get; set; }

    // Свойство материал деки
    public string DeckMaterial { get; set; }

    // Реализация свойств из интерфейсов
    public int MaxSpeed { get; set; }
    public bool IsFolded { get; set; }

    public Scooter(string name, decimal price, string manufacturer, bool isFolding,
        string deckMaterial, int maxSpeed)
        : base(name, price, manufacturer)
    {
        IsFolding = isFolding;
        DeckMaterial = deckMaterial;
        MaxSpeed = maxSpeed;
        IsFolded = false;
    }

    // Реализация абстрактного метода
    public override void DisplayInfo()
    {
        Console.WriteLine($"=== САМОКАТ ===");
        GetCommonInfo();
        Console.WriteLine($"Конструкция: {(IsFolding ? "складной" : "нескладной")}");
        Console.WriteLine($"Материал деки: {DeckMaterial}");
        Console.WriteLine($"Максимальная скорость: {MaxSpeed} км/ч");
        Console.WriteLine($"Текущее состояние: {(IsFolded ? "сложен" : "разложен")}");
    }

    // Реализация методов интерфейса ITransport
    public void Start()
    {
        Console.WriteLine($"{Name}: Отталкиваемся ногой и поехали!");
    }

    public void Stop()
    {
        Console.WriteLine($"{Name}: Тормозим ногой об землю");
    }

    // Реализация методов интерфейса IFoldable
    public void Fold()
    {
        if (IsFolding && !IsFolded)
        {
            IsFolded = true;
            Console.WriteLine($"{Name}: Самокат сложен для хранения");
        }
        else if (!IsFolding)
        {
            Console.WriteLine($"{Name}: Этот самокат нельзя сложить");
        }
        else
        {
            Console.WriteLine($"{Name}: Самокат уже сложен");
        }
    }

    public void Unfold()
    {
        if (IsFolded)
        {
            IsFolded = false;
            Console.WriteLine($"{Name}: Самокат готов к использованию");
        }
        else
        {
            Console.WriteLine($"{Name}: Самокат уже готов к использованию");
        }
    }

    // Сокрытие метода
    public new void GetCommonInfo()
    {
        Console.WriteLine($"САМОКАТ: {Name} - {Price} руб. ({Manufacturer})");
    }
}