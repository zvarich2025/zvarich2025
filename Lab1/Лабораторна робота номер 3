using System;
using System.IO;
using System.Text;

namespace UniversityApp
{
    // ==========================================
    // 1. КЛАС ФАКУЛЬТЕТ
    // ==========================================
    public class Faculty
    {
        private string name;
        private int departmentsCount;
        private int specialtiesCount;
        private int studentsCount;

        // Конструктор без параметрів
        public Faculty()
        {
            name = "";
            departmentsCount = 0;
            specialtiesCount = 0;
            studentsCount = 0;
        }

        // Конструктор з параметрами
        public Faculty(string name, int departments, int specialties, int students)
        {
            this.name = name;
            this.departmentsCount = departments;
            this.specialtiesCount = specialties;
            this.studentsCount = students;
        }

        // Властивості (get/set)
        public string Name { get => name; set => name = value; }
        public int DepartmentsCount { get => departmentsCount; set => departmentsCount = value; }
        public int SpecialtiesCount { get => specialtiesCount; set => specialtiesCount = value; }
        public int StudentsCount { get => studentsCount; set => studentsCount = value; }

        public void Input()
        {
            Console.WriteLine("--- Введення даних факультету ---");
            Console.Write("Назва факультету: ");
            name = Console.ReadLine();
            Console.Write("Кількість спеціальностей: ");
            specialtiesCount = int.Parse(Console.ReadLine());
            Console.Write("Загальна кількість студентів: ");
            studentsCount = int.Parse(Console.ReadLine());

            // Викликаю логіку коригування кількості кафедр
            AdjustDepartments();
        }

        public void Show()
        {
            Console.WriteLine("\n--- Дані факультету ---");
            Console.WriteLine($"Назва: {name}");
            Console.WriteLine($"Спеціальностей: {specialtiesCount}");
            Console.WriteLine($"Кафедр (розраховано): {departmentsCount}");
            Console.WriteLine($"Студентів: {studentsCount}");
        }

        public void WriteToFile()
        {
            string path = "faculty.txt";
            string info = $"Факультет: {name}, Кафедр: {departmentsCount}, Спеціальностей: {specialtiesCount}";
            File.WriteAllText(path, info);
            Console.WriteLine($"\n[OK] Дані факультету збережено у файл {path}");
        }

        private void AdjustDepartments()
        {
            // Логіка: одна спеціальність = одна кафедра, 
            // якщо на спеціальність припадає не менше 200 студентів.
            if (specialtiesCount > 0 && (studentsCount / specialtiesCount) >= 200)
            {
                departmentsCount = specialtiesCount;
            }
            else
            {
                // Якщо студентів мало, кількість кафедр розраховується просто по 200 осіб на одну
                departmentsCount = studentsCount / 200;
                if (departmentsCount == 0 && studentsCount > 0) departmentsCount = 1;
            }
        }
    }

    // ==========================================
    // 2. КЛАС КАФЕДРА (ЧАСТИНА 1)
    // ==========================================
    public partial class Department
    {
        private string depName;
        private int teachersCount;
        private int studentsCount;
        private int subjectsCount;
        private double totalHours;

        public Department()
        {
            depName = "";
            teachersCount = 0;
            studentsCount = 0;
            subjectsCount = 0;
            totalHours = 0;
        }

        public void Input()
        {
            Console.WriteLine("\n--- Введення даних кафедри ---");
            Console.Write("Назва кафедри: ");
            depName = Console.ReadLine();
            Console.Write("Кількість викладачів: ");
            teachersCount = int.Parse(Console.ReadLine());
            Console.Write("Кількість студентів: ");
            studentsCount = int.Parse(Console.ReadLine());
            Console.Write("Кількість дисциплін: ");
            subjectsCount = int.Parse(Console.ReadLine());
            Console.Write("Загальна кількість годин: ");
            totalHours = double.Parse(Console.ReadLine());
        }

        public void Show()
        {
            Console.WriteLine("\n--- Дані кафедри ---");
            Console.WriteLine($"Назва: {depName}");
            Console.WriteLine($"Викладачів: {teachersCount}");
            Console.WriteLine($"Студентів: {studentsCount}");
            Console.WriteLine($"Дисциплін: {subjectsCount}");
            Console.WriteLine($"Загальне навантаження: {totalHours} год.");
        }

        public void SaveToFile()
        {
            string path = "department.txt";
            string data = $"{depName}| Викладачів:{teachersCount}| Студентів:{studentsCount}| Годин:{totalHours}";
            File.WriteAllText(path, data);
            Console.WriteLine($"[OK] Дані кафедри збережено у файл {path}");
        }

        // 7. ВКЛАДЕНИЙ КЛАС Філія_кафедри
        public class BranchChair
        {
            private string companyName;
            private int rating;
            private double finance;

            public BranchChair(string cName, int cRating, double cFinance)
            {
                companyName = cName;
                rating = cRating;
                finance = cFinance;
            }

            public void CalculateKPI(double fact, double baseVal, double norm)
            {
                if (fact < baseVal)
                {
                    Console.WriteLine("Результат нижче базового - KPI = 0%");
                }
                else
                {
                    double kpi = ((fact - baseVal) / (norm - baseVal)) * 100;
                    Console.WriteLine($"Індекс KPI студента в {companyName}: {kpi:F2}%");
                }
            }

            public void OptimizeCosts()
            {
                Random rnd = new Random();
                double saved = rnd.Next(5000, 25000); // Генеримо випадкову економію
                Console.WriteLine($"Оптимізація витрат {companyName}: зекономлено {saved} грн на перенавчанні.");
            }
        }
    }

    // ==========================================
    // 3. КЛАС КАФЕДРА (ЧАСТИНА 2 - PARTIAL)
    // ==========================================
    public partial class Department
    {
        public void CalculateTeacherLoad()
        {
            if (teachersCount == 0) return;

            double loadPerOne = totalHours / teachersCount;
            Console.WriteLine($"\nНавантаження на одного викладача: {loadPerOne:F1} год/рік");

            // Перевірка нормативів
            if (loadPerOne > 600) Console.WriteLine("!!! Перевищення норми (макс 600 год)");
            if ((totalHours / subjectsCount) < 90) Console.WriteLine("!!! Малий обсяг однієї дисципліни (< 90 год)");

            double ratio = (double)studentsCount / teachersCount;
            Console.WriteLine($"Співвідношення студент/викладач: {ratio:F1}:1 (Норма 10:1)");
        }
    }

    // ==========================================
    // 4. СТАТИЧНИЙ КЛАС (ПРОЕКТ КОМПАНІЇ)
    // ==========================================
    public static class CompanyProject
    {
        public static double GetSquare(double x) => x * x;

        public static void PrintMessage(string msg) => Console.WriteLine($"[Project Log]: {msg}");

        public static int GetRandomId() => new Random().Next(100, 999);
    }

    // ==========================================
    // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення символів
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Працюємо з Факультетом
            Faculty myFaculty = new Faculty();
            myFaculty.Input();
            myFaculty.Show();
            myFaculty.WriteToFile();

            Console.WriteLine("\n-------------------------------------");

            // 2. Працюємо з Кафедрою
            Department myDep = new Department();
            myDep.Input();
            myDep.Show();
            myDep.CalculateTeacherLoad();
            myDep.SaveToFile();

            Console.WriteLine("\n-------------------------------------");

            // 3. Працюємо з Філією (Вкладений клас)
            Department.BranchChair myBranch = new Department.BranchChair("IT-Universe", 1, 500000);
            myBranch.CalculateKPI(85, 50, 90); // Факт: 85, База: 50, Норма: 90
            myBranch.OptimizeCosts();

            Console.WriteLine("\n-------------------------------------");

            // 4. Тест статики
            CompanyProject.PrintMessage("Система працює стабільно.");
            Console.WriteLine($"ID проекту: {CompanyProject.GetRandomId()}");

            Console.WriteLine("\nРоботу завершено. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
