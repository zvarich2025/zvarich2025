using System;
using System.Collections.Generic;

namespace Lab4_Varych
{
    // Клас для вузлів, що містять у собі список деталей
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
}
