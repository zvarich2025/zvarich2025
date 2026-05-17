//Базовий клас для всієї ієрархії виробів.
using System;

namespace Lab5
{
    public class Product
    {
        private string _name;
        private double _materialWeight;
        private double _materialPrice;

        public string Name { get => _name; set => _name = value; }
        public double MaterialWeight { get => _materialWeight; set => _materialWeight = value; }
        public double MaterialPrice { get => _materialPrice; set => _materialPrice = value; }

        // Конструктор за замовчуванням
        public Product()
        {
            _name = "Невідомий виріб";
            _materialWeight = 0.0;
            _materialPrice = 0.0;
        }

        // Конструктор з параметрами
        public Product(string name, double weight, double price)
        {
            _name = name;
            _materialWeight = weight;
            _materialPrice = price;
        }

        // Конструктор копіювання
        public Product(Product other)
        {
            _name = other._name;
            _materialWeight = other._materialWeight;
            _materialPrice = other._materialPrice;
        }

        // Віртуальний метод для вартості
        public virtual double CalculateCost()
        {
            return _materialWeight * _materialPrice;
        }

        // Віртуальний метод для кількості об'єктів із сировини
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
}
