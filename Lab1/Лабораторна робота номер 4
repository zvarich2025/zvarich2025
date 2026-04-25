using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4_Varych
{
    // === ТРЕТЯ ТА ДРУГА ВЕРСІЯ: ІНТЕРФЕЙС ТА АБСТРАКТНИЙ КЛАС ===

    // Інтерфейс для виробу
    public interface IProduct
    {
        string Name { get; set; }
        double GetWeight();
        double GetPrice();
        void DisplayInfo();
    }

    // Абстрактний базовий клас
    public abstract class ProductBase : IProduct, IComparable<ProductBase>
    {
        private string name;
        private double width, height, length;
        private string purpose;

        public string Name { get => name; set => name = value; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public double Length { get => length; set => length = value; }
        public string Purpose { get => purpose; set => purpose = value; }

        public ProductBase(string name, double w, double h, double l, string purp)
        {
            this.name = name;
            this.width = w;
            this.height = h;
            this.length = l;
            this.purpose = purp;
        }

        // Абстрактні методи, які будуть реалізовані в похідних класах
        public abstract double GetWeight();
        public abstract double GetPrice();
        public abstract int QualityScore { get; }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Вирiб: {Name} | Габарити: {Width}x{Height}x{Length} | Призначення: {Purpose}");
        }

        // Реалізація IComparable (Завдання 4: Сортування за вагою)
        public int CompareTo(ProductBase other)
        {
            if (other == null) return 1;
            return this.GetWeight().CompareTo(other.GetWeight());
        }
    }

    // === ПОХІДНІ КЛАСИ ===

    // Клас Деталь
    public class Detail : ProductBase
    {
        private double weight;
        private double price;
        private string material;

        public Detail(string name, double w, double h, double l, string purp, double weight, double price, string material)
            : base(name, w, h, l, purp)
        {
            this.weight = weight;
            this.price = price;
            this.material = material;
        }

        public override double GetWeight() => weight;
        public override double GetPrice() => price;
        public override int QualityScore => 85; // Умовна шкала якості

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  [Деталь] Матерiал: {material}, Вага: {weight}кг, Цiна: {price}грн");
        }
    }

    // Клас Вузол (складається з деталей)
    public class Assembly : ProductBase
    {
        private List<Detail> details = new List<Detail>();
        public string Certificate { get; set; }

        public Assembly(string name, double w, double h, double l, string purp, string cert)
            : base(name, w, h, l, purp)
        {
            Certificate = cert;
        }

        public void AddDetail(Detail d) => details.Add(d);

        public override double GetWeight()
        {
            double sum = 0;
            foreach (var d in details) sum += d.GetWeight();
            return sum;
        }

        public override double GetPrice()
        {
            double sum = 0;
            foreach (var d in details) sum += d.GetPrice();
            return sum;
        }

        public override int QualityScore => 95;

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  [Вузол] Сертифiкат: {Certificate}, К-сть деталей: {details.Count}, Загальна вага: {GetWeight()}кг");
        }
    }

    // === ДОПОМІЖНІ КЛАСИ (IComparer, IEnumerable) ===

    // Порівняння за ціною
    public class PriceComparer : IComparer<ProductBase>
    {
        public int Compare(ProductBase x, ProductBase y) => x.GetPrice().CompareTo(y.GetPrice());
    }

    // Порівняння за якістю
    public class QualityComparer : IComparer<ProductBase>
    {
        public int Compare(ProductBase x, ProductBase y) => x.QualityScore.CompareTo(y.QualityScore);
    }

    // Клас для ітерації (IEnumerable)
    public class ProductList : IEnumerable
    {
        private ProductBase[] _products;
        public ProductList(ProductBase[] pArray) { _products = pArray; }

        // Повертаємо список, впорядкований за ціною
        public IEnumerator GetEnumerator()
        {
            Array.Sort(_products, new PriceComparer());
            return _products.GetEnumerator();
        }
    }

    // === ГОЛОВНА ПРОГРАМА ===
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Створення об'єктів
            Detail bolt = new Detail("Болт M10", 1, 1, 5, "Крiплення", 0.05, 15, "Сталь");
            Detail nut = new Detail("Гайка M10", 2, 2, 1, "Крiплення", 0.02, 10, "Сталь");

            Assembly engineBlock = new Assembly("Блок цилiндрiв", 50, 40, 60, "Двигун", "ISO-9001");
            engineBlock.AddDetail(bolt);
            engineBlock.AddDetail(nut);

            // 2. Масив виробів для демонстрації інтерфейсів
            ProductBase[] inventory = { bolt, nut, engineBlock };

            Console.WriteLine("--- Список виробiв (IComparable - Сортування за вагою) ---");
            Array.Sort(inventory);
            foreach (var item in inventory)
                Console.WriteLine($"{item.Name}: {item.GetWeight()} кг");

            Console.WriteLine("\n--- Використання IComparer (Сортування за якiстю) ---");
            Array.Sort(inventory, new QualityComparer());
            foreach (var item in inventory)
                Console.WriteLine($"{item.Name}: Якiсть {item.QualityScore}");

            Console.WriteLine("\n--- Використання IEnumerable (Сортування за цiною) ---");
            ProductList pList = new ProductList(inventory);
            foreach (ProductBase p in pList)
            {
                Console.WriteLine($"{p.Name}: {p.GetPrice()} грн");
            }

            Console.WriteLine("\n--- Доступ через посилання на iнтерфейс IProduct ---");
            IProduct interfacedProduct = engineBlock;
            interfacedProduct.DisplayInfo();

            Console.ReadKey();
        }
    }
}
