using System;

namespace UniversityApp
{
    public static class CompanyProject
    {
        public static double GetSquare(double x) => x * x;
        public static void Log(string msg) => Console.WriteLine($"[Project Log]: {msg}");
        public static int GetId() => new Random().Next(100, 999);
    }
}
