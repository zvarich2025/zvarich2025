using System;

namespace Lab4_Varych
{
    // Клас для окремих деталей
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
        public override int QualityScore => 85; 

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"  [Деталь] Матерiал: {material}, Вага: {weight}кг, Цiна: {price}грн");
        }
    }
}
