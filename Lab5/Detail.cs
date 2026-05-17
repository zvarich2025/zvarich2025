//Похідний клас, що представляє деталь, містить перевантажені оператори.
using System;

namespace Lab5
{
    public class Detail : Product
    {
        private double _laborCost;

        public double LaborCost { get => _laborCost; set => _laborCost = value; }

        // Конструктори
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

        // Перевизначення вартості (Матеріал * Ціна + Робота)
        public override double CalculateCost()
        {
            return (MaterialWeight * MaterialPrice) + _laborCost;
        }

        public override double CalculateOutputQuantity(double totalRawMaterial)
        {
            return base.CalculateOutputQuantity(totalRawMaterial);
        }

        // Бінарні оператори порівняння
        public static bool operator >(Detail a, Detail b) => a.CalculateCost() > b.CalculateCost();
        public static bool operator <(Detail a, Detail b) => a.CalculateCost() < b.CalculateCost();
        public static bool operator ==(Detail a, Detail b) => a.CalculateCost() == b.CalculateCost();
        public static bool operator !=(Detail a, Detail b) => a.CalculateCost() != b.CalculateCost();

        // Математичні бінарні оператори
        public static Detail operator +(Detail d, double value)
        {
            d.LaborCost += value;
            return d;
        }

        public static Detail operator -(Detail d, double value)
        {
            d.LaborCost -= value;
            return d;
        }

        // Унарні оператори
        public static Detail operator ++(Detail d)
        {
            d.MaterialWeight += 0.5;
            return d;
        }

        public static Detail operator --(Detail d)
        {
            d.MaterialWeight -= 0.5;
            return d;
        }

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
}
