namespace Lab1;

// 2. Производный класс: Велосипед
public class Bicycle : Product
{
    // Специфичное свойство для велосипеда(количество скоростей)
    public int GearsCount { get; set; }

    // Специфичное свойство для велосипеда(тип тормозов)
    public string BrakeType { get; set; }

    // Конструктор, вызывающий конструктор базового класса
    public Bicycle(string name, decimal price, string manufacturer, int gearsCount, string brakeType) : base(name, price, manufacturer)
    {
        GearsCount = gearsCount;
        BrakeType = brakeType;
    }

    // Переопределение метода базового класса
    public override void GetInfo()
    {
        // Вызываем метод базового класса
        base.GetInfo();
        // Добавляем свою информацию
        Console.WriteLine($"Тип: Велосипед, Количество скоростей: {GearsCount}, Тормоза: {BrakeType}");
    }
}