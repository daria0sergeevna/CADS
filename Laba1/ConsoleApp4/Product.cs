namespace Lab1;

public class Product
{
    private decimal price;

    // Свойство для доступа к цене с проверкой
    public decimal Price
    {
        get { return price; }
        set
        {
            if (value >= 0)
                price = value;
            else
                Console.WriteLine("Цена не может быть отрицательной");
        }
    }

    // Автоматическое свойство
    public string Name { get; set; }

    // Свойство - Производитель
    public string Manufacturer { get; set; }

    // Конструктор по умолчанию
    // public Product()
    // {
    //     Name = "Неизвестный товар";
    //     Price = 0;
    //     Manufacturer = "Неизвестно";
    // }

    // Конструктор с параметрами
    public Product(string name = "Неизвестный товар", decimal price = 0, string manufacturer= "Неизвестно")
    {
        Name = name;
        Price = price;
        Manufacturer = manufacturer;
    }

    // Виртуальный метод (может быть переопределен в наследниках)
    public virtual void GetInfo()
    {
        Console.WriteLine($"Товар: {Name}, Цена: {Price} руб., Производитель: {Manufacturer}");
        //интерполяция строк, подставляет значения переменных в строку
    }
}