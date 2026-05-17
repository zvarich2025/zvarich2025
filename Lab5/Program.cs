using System;

namespace Lab5
{
    // === ЗАВДАННЯ 1: ІЄРАРХІЯ КЛАСІВ ===

    // Базовий клас Виріб
    public class Product
    {
        private string _name;
        private double _materialWeight;
        private double _materialPrice;

        // Відкриті властивості (Properties)
        public string Name { get => _name; set => _name = value; }
        public double MaterialWeight { get => _materialWeight; set => _materialWeight = value; }
        public double MaterialPrice { get => _materialPrice; set => _materialPrice = value; }

        // ЗАВДАННЯ 2: Три конструктори для базового класу
        // 1. Конструктор за замовчуванням
        public Product()
        {
            _name = "Невідомий виріб";
            _materialWeight = 0.0;
            _materialPrice = 0.0;
        }

        // 2. Конструктор з параметрами
        public Product(string name, double weight, double price)
        {
            _name = name;
            _materialWeight = weight;
            _materialPrice = price;
        }

        // 3. Конструктор копіювання
        public Product(Product other)
        {
            _name = other._name;
            _materialWeight = other._materialWeight;
            _materialPrice = other._materialPrice;
        }

        // ЗАВДАННЯ 3: Віртуальний метод для вартості
        public virtual double CalculateCost()
        {
            return _materialWeight * _materialPrice;
        }

        // ЗАВДАННЯ 4: Віртуальний метод для кількості об'єктів із сировини
        public virtual double CalculateOutputQuantity(double totalRawMaterial)
        {
            if (_materialWeight <= 0) return 0;
            return totalRawMaterial / _materialWeight;
        }

        public virtual void DisplayInfo()
        {
            Console.Write($"Виріб: {Name} | Вага матеріалу: {MaterialWeight} кг | Ціна матеріалу: {MaterialPrice} грн");
        }
    }

    // Похідний клас Деталь
    public class Detail : Product
    {
        private double _laborCost;

        public double LaborCost { get => _laborCost; set => _laborCost = value; }

        // ЗАВДАННЯ 2: Три конструктори для Деталі
        public Detail() : base()
        {
            _laborCost = 0.0;
        }

        public Detail(string name, double weight, double price, double laborCost)
            : base(name, weight, price)
        {
            _laborCost = laborCost;
        }

        public Detail(Detail other) : base(other)
        {
            _laborCost = other._laborCost;
        }

        // ЗАВДАННЯ 3: Перевизначення вартості (Матеріал * Ціна + Робота)
        public override double CalculateCost()
        {
            return (MaterialWeight * MaterialPrice) + _laborCost;
        }

        // ЗАВДАННЯ 4: Перевизначення кількості виробів
        public override double CalculateOutputQuantity(double totalRawMaterial)
        {
            return base.CalculateOutputQuantity(totalRawMaterial);
        }

        // ЗАВДАННЯ 5: Перевантаження бінарних операторів порівняння
        public static bool operator >(Detail a, Detail b) => a.CalculateCost() > b.CalculateCost();
        public static bool operator <(Detail a, Detail b) => a.CalculateCost() < b.CalculateCost();
        public static bool operator ==(Detail a, Detail b) => a.CalculateCost() == b.CalculateCost();
        public static bool operator !=(Detail a, Detail b) => a.CalculateCost() != b.CalculateCost();

        // Додавання числа збільшує оплату праці
        public static Detail operator +(Detail d, double value)
        {
            d.LaborCost += value;
            return d;
        }

        // Віднімання числа зменшує оплату праці
        public static Detail operator -(Detail d, double value)
        {
            d.LaborCost -= value;
            return d;
        }

        // ЗАВДАННЯ 6: Перевантаження унарних операторів
        // Інкремент (++) збільшує вагу матеріалу
        public static Detail operator ++(Detail d)
        {
            d.MaterialWeight += 0.5;
            return d;
        }

        // Декремент (--) зменшує вагу матеріалу
        public static Detail operator --(Detail d)
        {
            d.MaterialWeight -= 0.5;
            return d;
        }

        // Унарний мінус (-) позначає брак (вартість роботи йде в збиток)
        public static Detail operator -(Detail d)
        {
            d.LaborCost = -d.LaborCost;
            return d;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($" | Робота: {LaborCost} грн | Загальна вартість: {CalculateCost()} грн");
        }

        public override bool Equals(object obj) => obj is Detail detail && this == detail;
        public override int GetHashCode() => CalculateCost().GetHashCode();
    }

    // Похідний клас Вузол
    public class AssemblyUnit : Product
    {
        private int _componentsCount;

        public int ComponentsCount { get => _componentsCount; set => _componentsCount = value; }

        // ЗАВДАННЯ 2: Три конструктори для Вузла
        public AssemblyUnit() : base()
        {
            _componentsCount = 0;
        }

        public AssemblyUnit(string name, double weight, double price, int componentsCount)
            : base(name, weight, price)
        {
            _componentsCount = componentsCount;
        }

        public AssemblyUnit(AssemblyUnit other) : base(other)
        {
            _componentsCount = other._componentsCount;
        }

        public override double CalculateCost()
        {
            // Націнка за збірку вузла залежно від кількості його компонентів
            return (MaterialWeight * MaterialPrice) + (_componentsCount * 50);
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($" | К-сть деталей: {ComponentsCount} шт | Загальна вартість: {CalculateCost()} грн");
        }
    }

    // Похідний клас Агрегат (Новий клас за ЗАВДАННЯМ 1)
    public class Subsystem : Product
    {
        private string _factorySerialNumber;
        private double _powerConsumption;

        public string FactorySerialNumber { get => _factorySerialNumber; set => _factorySerialNumber = value; }
        public double PowerConsumption { get => _powerConsumption; set => _powerConsumption = value; }

        // ЗАВДАННЯ 2: Три航空структори для Агрегату
        public Subsystem() : base()
        {
            _factorySerialNumber = "000-XYZ";
            _powerConsumption = 0.0;
        }

        public Subsystem(string name, double weight, double price, string serialNumber, double power)
            : base(name, weight, price)
        {
            _factorySerialNumber = serialNumber;
            _powerConsumption = power;
        }

        public Subsystem(Subsystem other) : base(other)
        {
            _factorySerialNumber = other._factorySerialNumber;
            _powerConsumption = other._powerConsumption;
        }

        public override double CalculateCost()
        {
            // Додаткова амортизаційна вартість залежно від потужності агрегату
            return (MaterialWeight * MaterialPrice) + (_powerConsumption * 12);
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($" | Серійний №: {FactorySerialNumber} | Потужність: {PowerConsumption} кВт | Загальна вартість: {CalculateCost()} грн");
        }
    }

    // === ЗАВДАННЯ 7: КЛАС-КОНТЕЙНЕР З ІНДЕКСАТОРОМ ===
    public class DetailCollection
    {
        private Detail[] _items;

        public DetailCollection(int size)
        {
            _items = new Detail[size];
        }

        // Власний індексатор
        public Detail this[int index]
        {
            get
            {
                if (index >= 0 && index < _items.Length)
                    return _items[index];

                Console.WriteLine("Помилка: Вихід за межі масиву!");
                return null;
            }
            set
            {
                if (index >= 0 && index < _items.Length)
                    _items[index] = value;
                else
                    Console.WriteLine("Помилка: Неможливо записати об'єкт, невірний індекс!");
            }
        }

        public int Length => _items.Length;
    }

    // === ГОЛОВНА ПРОГРАМА ===
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
