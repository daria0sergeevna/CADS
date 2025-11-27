namespace Lab1;



// 3. Производный класс от Велосипеда: Горный велосипед
public class MountainBike : Bicycle
{
    // Специфичное свойство для горного велосипеда(Наличие амортизационной вилки)
    public bool HasSuspension { get; set; }

    // Специфичное свойство для горного велосипеда(Наличие амортизационной вилки(тип подвески)
    public string SuspensionType { get; set; }

    public MountainBike(string name, decimal price, string manufacturer, int gearsCount, string brakeType, bool hasSuspension, string suspensionType)
        : base(name, price, manufacturer, gearsCount, brakeType) // Вызов конструктора Bicycle
    {
        HasSuspension = hasSuspension;
        SuspensionType = suspensionType;
    }

    public override void GetInfo()
    {
        base.GetInfo(); // Вызов метода из Bicycle, который уже вызвал метод из Product
        string suspensionStatus = HasSuspension ? "есть" : "нет";
        Console.WriteLine($"амортизационная вилка: {suspensionStatus}, Тип подвески: {SuspensionType}");
    }
}