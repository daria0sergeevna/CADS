namespace Lab1;

// 4. Другой производный класс от Товара: Самокат
public class Scooter : Product
{
    // Специфичное свойство для самоката(складной или нет)
    public bool IsFolding { get; set; }

    // Специфичное свойство для самоката(материал деки)
    public string DeckMaterial { get; set; }

    public Scooter(string name, decimal price, string manufacturer, bool isFolding, string deckMaterial) : base(name, price, manufacturer)
    {
        IsFolding = isFolding;
        DeckMaterial = deckMaterial;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        string foldingStatus = IsFolding ? "складной" : "нескладной";
        Console.WriteLine($"Тип: Самокат, Конструкция: {foldingStatus}, Материал: {DeckMaterial}");
    }
}