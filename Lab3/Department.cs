using System;
using System.IO;

namespace UniversityApp
{
    public partial class Department
    {
        private string depName;
        private int teachersCount;
        private int studentsCount;
        private int subjectsCount;
        private double totalHours;

        public Department() { depName = ""; }

        public void Input()
        {
            Console.WriteLine("\n--- Введення даних кафедри ---");
            Console.Write("Назва кафедри: "); depName = Console.ReadLine();
            Console.Write("Викладачів: "); teachersCount = int.Parse(Console.ReadLine());
            Console.Write("Студентів: "); studentsCount = int.Parse(Console.ReadLine());
            Console.Write("Дисциплін: "); subjectsCount = int.Parse(Console.ReadLine());
            Console.Write("Годин: "); totalHours = double.Parse(Console.ReadLine());
        }

        public void Show() => Console.WriteLine($"\nКафедра: {depName}, Викладачів: {teachersCount}, Студентів: {studentsCount}");

        public void SaveToFile()
        {
            File.WriteAllText("department.txt", $"{depName}| Викладачів:{teachersCount}| Студентів:{studentsCount}");
            Console.WriteLine("[OK] Дані кафедри збережено.");
        }

        // Вкладений клас - Філія
        public class BranchChair
        {
            private string companyName;
            public BranchChair(string name) => companyName = name;

            public void CalculateKPI(double fact, double baseVal, double norm)
            {
                double kpi = fact < baseVal ? 0 : ((fact - baseVal) / (norm - baseVal)) * 100;
                Console.WriteLine($"Індекс KPI в {companyName}: {kpi:F2}%");
            }

            public void OptimizeCosts()
            {
                int saved = new Random().Next(5000, 25000);
                Console.WriteLine($"Оптимізація {companyName}: зекономлено {saved} грн.");
            }
        }
    }
}
