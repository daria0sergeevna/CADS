namespace Lab2
{

    class Program
    {
        static void Main()
        {

            // Создаем объекты разных классов
            Bicycle cityBike = new Bicycle("Городской велосипед", 15000, "Forward", 7, "Ободные", 25);
            MountainBike adventureBike = new MountainBike("Горный велосипед", 45000, "Stels", 21, "Дисковые", 45, true,
                "Амортизационная");
            Scooter kickScooter = new Scooter("Самокат", 8000, "Xiaomi", true, "Алюминий", 20);

            // Демонстрация полиморфизма через абстрактный метод
            Console.WriteLine("1. ДЕМОНСТРАЦИЯ АБСТРАКТНОГО МЕТОДА:");

            Product[] products = { cityBike, adventureBike, kickScooter };
            foreach (Product product in products)
            {
                product.DisplayInfo(); // Полиморфный вызов - для каждого объекта свой метод
                Console.WriteLine();
            }

            // // Демонстрация полиморфизма через интерфейс ITransport
            // Console.WriteLine("2. ДЕМОНСТРАЦИЯ ИНТЕРФЕЙСА ITRANSPORT:");
            //
            // ITransport[] transports = { cityBike, adventureBike, kickScooter };
            // foreach (ITransport transport in transports)
            // {
            //     transport.Start();
            //     transport.Stop();
            //     Console.WriteLine();
            // }
            //
            // // Демонстрация полиморфизма через интерфейс IFoldable
            // Console.WriteLine("3. ДЕМОНСТРАЦИЯ ИНТЕРФЕЙСА IFOLDABLE:");
            //
            // IFoldable[] foldables = { adventureBike, kickScooter };
            // foreach (IFoldable foldable in foldables)
            // {
            //     foldable.Fold();
            //     foldable.Unfold();
            //     Console.WriteLine();
            // }

            // Демонстрация перегрузки методов
            Console.WriteLine("4. ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ МЕТОДОВ:");

            adventureBike.Ride(); // Вызов метода без параметров
            adventureBike.Ride("горной местности"); // Вызов перегруженного метода

            Console.WriteLine();

            // Демонстрация сокрытия метода (ключевое слово new)
            Console.WriteLine("5. ДЕМОНСТРАЦИЯ СОКРЫТИЯ МЕТОДА:");

            kickScooter.GetCommonInfo(); // Вызов скрытого метода
            ((Product)kickScooter).GetCommonInfo(); // Вызов метода базового класса

            Console.WriteLine();

            // Демонстрация инкапсуляции
            Console.WriteLine("6. ПРОВЕРКА ИНКАПСУЛЯЦИИ:");

            try
            {
                cityBike.Price = -100; // Сработает проверка в сеттере свойства Price
            }
            catch
            {
                // Ошибка уже обработана в сеттере
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadLine();
        }
    }
}
