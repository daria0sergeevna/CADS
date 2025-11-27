namespace Lab2;

// Абстрактный класс для демонстрации полиморфизма
public abstract class Product
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

    // Свойство производитель
    public string Manufacturer { get; set; }

    // Конструктор по умолчанию
    public Product()
    {
        Name = "Неизвестный товар";
        Price = 0;
        Manufacturer = "Неизвестно";
    }

    // Конструктор с параметрами
    public Product(string name, decimal price, string manufacturer)
    {
        Name = name;
        Price = price;
        Manufacturer = manufacturer;
    }

    // Абстрактный метод - должен быть реализован в производных классах
    public abstract void DisplayInfo();

    // Виртуальный метод - может быть переопределен в производных классах
    public virtual void GetCommonInfo()
    {
        Console.WriteLine($"Товар: {Name}, Цена: {Price} руб., Производитель: {Manufacturer}");
    }
}

// // Интерфейс для транспортных средств
// public interface ITransport
// {
//     int MaxSpeed { get; set; } // Максимальная скорость
//     void Start(); // Метод запуска транспорта
//     void Stop();  // Метод остановки транспорта
// }
//
// // Интерфейс для складных предметов
// public interface IFoldable
// {
//     bool IsFolded { get; set; } // Сложен или нет
//     void Fold();   // Метод сложения
//     void Unfold(); // Метод раскладывания
//}