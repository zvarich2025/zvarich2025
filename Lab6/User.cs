using System;

namespace SmartMirrorLab6
{
    // КЛАС КОРИСТУВАЧА (СУТНІСТЬ ДЛЯ ДЕМОНСТРАЦІЇ АГРЕГАЦІЇ)
    public class User
    {
        // Закриті поля (Принцип інкапсуляції)
        private string _name;
        private double _weight;
        private double _height;
        private double _temperature;
        private int _pulse;
        private string _currentEmotion;

        // Відкриті властивості (аксесори) з валідацією вхідних даних
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? "Анонімний користувач" : value;
        }

        public double Weight
        {
            get => _weight;
            set => _weight = value > 0 ? value : throw new ArgumentException("Вага не може бути меншою або рівною 0!");
        }

        public double Height
        {
            get => _height;
            set => _height = value > 0 ? value : throw new ArgumentException("Зріст не може бути меншим або рівним 0!");
        }

        public double Temperature
        {
            get => _temperature;
            set => _temperature = (value >= 34.0 && value <= 43.0) ? value : throw new ArgumentException("Недопустиме значення температури тіла!");
        }

        public int Pulse
        {
            get => _pulse;
            set => _pulse = (value >= 0 && value <= 250) ? value : throw new ArgumentException("Показник пульсу виходить за межі норми!");
        }

        public string CurrentEmotion
        {
            get => _currentEmotion;
            set => _currentEmotion = string.IsNullOrWhiteSpace(value) ? "Спокій" : value;
        }

        // Конструктор з параметрами для ініціалізації
        public User(string name, double weight, double height, double temperature, int pulse, string emotion)
        {
            Name = name;
            Weight = weight;
            Height = height;
            Temperature = temperature;
            Pulse = pulse;
            CurrentEmotion = emotion;
        }
    }
}
