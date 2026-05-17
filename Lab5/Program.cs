//Головна точка входу в програму, де демонструється взаємодія всіх створених вище класів.
using System;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("--- Демонстрація Завдання 2: Робота конструкторів ---");
            Detail detail1 = new Detail("Вал шестерні", 4.5, 120, 300);
            Detail detail2 = new Detail(detail1);
            detail2.Name = "Копія Валу (модифікована)";

            detail1.DisplayInfo();
            detail2.DisplayInfo();

            Console.WriteLine("\n--- Демонстрація Завдань 3 та 4: Віртуальні методи ---");
            AssemblyUnit assembly = new AssemblyUnit("Редуктор", 25.0, 80, 5);
            Subsystem subsystem = new Subsystem("Генератор струму", 150.0, 95, "ZVD-2026", 5.5);

            assembly.DisplayInfo();
            subsystem.DisplayInfo();

            double rawMaterialStock = 500.0;
            Console.WriteLine($"Із {rawMaterialStock} кг сировини можна виготовити:");
            Console.WriteLine($"- Деталей '{detail1.Name}': {(int)detail1.CalculateOutputQuantity(rawMaterialStock)} шт.");
            Console.WriteLine($"- Вузлів '{assembly.Name}': {(int)assembly.CalculateOutputQuantity(rawMaterialStock)} шт.");
            Console.WriteLine($"- Агрегатів '{subsystem.Name}': {(int)subsystem.CalculateOutputQuantity(rawMaterialStock)} шт.");

            Console.WriteLine("\n--- Демонстрація Завдання 5: Бінарні оператори ---");
            Detail bolt = new Detail("Болт великий", 1.0, 50, 40);
            Detail nut = new Detail("Гайка велика", 0.5, 40, 20);

            bolt.DisplayInfo();
            nut.DisplayInfo();

            Console.WriteLine($"Чи дорожчий Болт за Гайку? -> {bolt > nut}");

            Console.WriteLine("Збільшуємо оплату праці для Гайки на 60 грн за допомогою оператора '+'");
            nut = nut + 60;
            nut.DisplayInfo();
            Console.WriteLine($"Чи тепер вони рівні за вартістю? -> {bolt == nut}");

            Console.WriteLine("\n--- Демонстрація Завдання 6: Унарні оператори ---");
            Console.WriteLine("Початковий стан Болта:");
            bolt.DisplayInfo();

            Console.WriteLine("Застосовуємо інкремент '++' (збільшує вагу матеріалу на 0.5 кг):");
            bolt++;
            bolt.DisplayInfo();

            Console.WriteLine("Маркуємо деталь як БРАК за допомогою унарного мінуса '-':");
            bolt = -bolt;
            bolt.DisplayInfo();

            Console.WriteLine("\n--- Демонстрація Завдання 7: Клас-колекція та індексатори ---");
            DetailCollection factoryStorage = new DetailCollection(3);

            factoryStorage[0] = new Detail("Корпус", 12.0, 150, 500);
            factoryStorage[1] = nut;
            factoryStorage[2] = new Detail("Шпилька", 0.2, 30, 15);

            for (int i = 0; i < factoryStorage.Length; i++)
            {
                Console.Write($"Елемент за індексом [{i}] -> ");
                factoryStorage[i].DisplayInfo();
            }

            Console.ReadKey();
        }
    }
}
