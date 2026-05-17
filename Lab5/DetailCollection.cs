//Клас-контейнер, який реалізує логіку індексатора для деталей.
using System;

namespace Lab5
{
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
}
