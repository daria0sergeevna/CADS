namespace Lab2;

// 2. Производный класс: Велосипед
public class Bicycle : Product//, ITransport
{
    // Специфичное свойство для велосипеда(количество скоростей)
    public int GearsCount { get; set; }

    // свойство тип тормозов
    public string BrakeType { get; set; }

    // Реализация свойства из интерфейса ITransport
    public int MaxSpeed { get; set; }

    // Конструктор, вызывающий конструктор базового класса
    public Bicycle(string name, decimal price, string manufacturer, int gearsCount, string brakeType, int maxSpeed) 
        : base(name, price, manufacturer)
    {
        GearsCount = gearsCount;
        BrakeType = brakeType;
        MaxSpeed = maxSpeed;
    }

    // Реализация абстрактного метода
    public override void DisplayInfo()
    {
        Console.WriteLine($"=== ВЕЛОСИПЕД ===");
        GetCommonInfo();
        Console.WriteLine($"Количество скоростей: {GearsCount}");
        Console.WriteLine($"Тип тормозов: {BrakeType}");
        Console.WriteLine($"Максимальная скорость: {MaxSpeed} км/ч");
    }

    // // Реализация методов интерфейса ITransport
    // public void Start()
    // {
    //     Console.WriteLine($"{Name}: Начинаем крутить педали!");
    // }
    //
    // public void Stop()
    // {
    //     Console.WriteLine($"{Name}: Останавливаемся с помощью {BrakeType} тормозов");
    // }

    // Переопределение виртуального метода
    public override void GetCommonInfo()
    {
        base.GetCommonInfo();
        Console.WriteLine($"Тип: Велосипед, Скоростей: {GearsCount}");
    }
}