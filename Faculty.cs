using System;
using System.IO;

namespace UniversityApp
{
    public class Faculty
    {
        private string name;
        private int departmentsCount;
        private int specialtiesCount;
        private int studentsCount;

        public Faculty()
        {
            name = "";
            departmentsCount = 0;
            specialtiesCount = 0;
            studentsCount = 0;
        }

        public Faculty(string name, int departments, int specialties, int students)
        {
            this.name = name;
            this.departmentsCount = departments;
            this.specialtiesCount = specialties;
            this.studentsCount = students;
        }

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
            AdjustDepartments();
        }

        public void Show()
        {
            Console.WriteLine("\n--- Дані факультету ---");
            Console.WriteLine($"Назва: {name}\nСпеціальностей: {specialtiesCount}\nКафедр: {departmentsCount}\nСтудентів: {studentsCount}");
        }

        public void WriteToFile()
        {
            string info = $"Факультет: {name}, Кафедр: {departmentsCount}, Спеціальностей: {specialtiesCount}";
            File.WriteAllText("faculty.txt", info);
            Console.WriteLine("\n[OK] Дані факультету збережено.");
        }

        private void AdjustDepartments()
        {
            if (specialtiesCount > 0 && (studentsCount / specialtiesCount) >= 200)
                departmentsCount = specialtiesCount;
            else
                departmentsCount = Math.Max(1, studentsCount / 200);
        }
    }
}
