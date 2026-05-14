using System;

namespace Lab4_Varych
{
    // Абстрактний клас, що реалізує спільні поля та базову логіку порівняння
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

        public abstract double GetWeight();
        public abstract double GetPrice();
        public abstract int QualityScore { get; }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Вирiб: {Name} | Габарити: {Width}x{Height}x{Length} | Призначення: {Purpose}");
        }

        // Реалізація IComparable для автоматичного сортування за вагою
        public int CompareTo(ProductBase other)
        {
            if (other == null) return 1;
            return this.GetWeight().CompareTo(other.GetWeight());
        }
    }
}
