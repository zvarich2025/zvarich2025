//Похідний клас для агрегату зі своїми специфічними характеристиками.
using System;

namespace Lab5
{
    public class Subsystem : Product
    {
        private string _factorySerialNumber;
        private double _powerConsumption;

        public string FactorySerialNumber { get => _factorySerialNumber; set => _factorySerialNumber = value; }
        public double PowerConsumption { get => _powerConsumption; set => _powerConsumption = value; }

        // Конструктори
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
}
