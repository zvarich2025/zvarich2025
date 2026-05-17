//Похідний клас для вузла, який складається з певної кількості деталей.
using System;

namespace Lab5
{
    public class AssemblyUnit : Product
    {
        private int _componentsCount;

        public int ComponentsCount { get => _componentsCount; set => _componentsCount = value; }

        // Конструктори
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
}
