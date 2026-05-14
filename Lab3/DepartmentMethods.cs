using System;

namespace UniversityApp
{
    public partial class Department
    {
        public void CalculateTeacherLoad()
        {
            if (teachersCount == 0) return;
            double loadPerOne = totalHours / teachersCount;
            Console.WriteLine($"Навантаження: {loadPerOne:F1} год/рік");
            
            if (loadPerOne > 600) Console.WriteLine("!!! Перевищення норми годин");
            Console.WriteLine($"Співвідношення: {(double)studentsCount / teachersCount:F1}:1");
        }
    }
}
