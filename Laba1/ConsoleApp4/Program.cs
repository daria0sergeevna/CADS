namespace Lab1
//создание пространства имен для организации кода
{

    class Program
    {
        static void Main(string[] args)
        {
            // Демонстрация работы классов

            // Создаем объекты разных классов
            Product simpleProduct = new Product("Масло", 250, "Lukoil");
            Bicycle cityBike = new Bicycle("Городской велосипед", 15000, "Forward", 7, "Ободные");
            MountainBike adventureBike = new MountainBike("Горный велосипед", 45000, "Stels", 21, "Дисковые", true, "Амортизационная");
            Scooter kickScooter = new Scooter("Самокат", 8000, "Xiaomi", true, "Алюминий");

            Console.WriteLine("--- Информация о товарах ---\n");

            // Выводим информацию о каждом объекте
            simpleProduct.GetInfo();
            Console.WriteLine();

            cityBike.GetInfo();
            Console.WriteLine();

            adventureBike.GetInfo();
            Console.WriteLine();

            kickScooter.GetInfo();
            Console.WriteLine();

            // Демонстрация инкапсуляции: попытка установить отрицательную цену
            Console.WriteLine("--- Проверка инкапсуляции---");
            simpleProduct.Price = -100; // Сработает проверка в сеттере свойства Price

            Console.ReadLine();
        }
    }
}