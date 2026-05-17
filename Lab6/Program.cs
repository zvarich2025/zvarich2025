//Головна точка входу, яка налаштовує UTF-8, створює об'єкти, підписує модулі на події та запускає 5 тест-сценаріїв.
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartMirrorLab6
{
    // ТОЧКА ВХОДУ В ПРОГРАМУ (ТЕСТУВАННЯ ТА ОБРОБКА ВСІХ ВИКЛЮЧЕНЬ)
    class Program
    {
        static void Main(string[] args)
        {
            // Встановлення UTF-8 кодування для консолі з метою уникнення знаків питання в тексті
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("======= ЛАБОРАТОРНА РОБОТА №6. ВИКОНАВ СТУДЕНТ КНУ =======");

            // Створення списку користувачів (Агрегація)
            List<User> userGroup = new List<User>
            {
                new User("Олексій", 78.2, 1.82, 36.6, 75, "Радість"),      
                new User("Марія", 54.0, 1.68, 38.5, 98, "Сум"),            
                new User("Дмитро", 85.0, 1.76, 39.9, 175, "Раптовий інфаркт") 
            };

            // Створення екземпляру похідного класу (Наслідування)
            SmartMirrorWithVoiceAssistant smartMirror = new SmartMirrorWithVoiceAssistant(userGroup, 100, "Аліса");

            // ПІДПИСКА НА ПОДІЮ ЧЕРЕЗ ДЕЛЕГАТИ (ЗАВДАННЯ 5)
            smartMirror.CriticalSituationDetected += smartMirror.SpeakEmergencyWarning;
            smartMirror.CriticalSituationDetected += (msg, usr) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Служба Екстреної Допомоги 103] Wi-Fi ТРИВОГА: Отримано SOS-пакет від смарт-дзеркала.");
                Console.WriteLine($"[Служба Екстреної Допомоги 103] Направлено бригаду реанімації за GPS-координатами пристрою.");
                Console.ResetColor();
            };

            // Активація дзеркала
            smartMirror.ActivateSystem();

            // Сценарій 1: Тест нормальної роботи
            Console.WriteLine("\n--- Сценарій 1: Тест нормальної роботи ---");
            try
            {
                smartMirror.InterpretVoiceInput("Аліса, запусти біометричну діагностику", 0); 
                smartMirror.InterpretVoiceInput("Аліса, увімкни моделювання зачіски", 1);    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            // Сценарій 2: Тест виходу за межі масиву людей
            Console.WriteLine("\n--- Сценарій 2: Тест виходу за межі масиву людей ---");
            try
            {
                smartMirror.AnalyzeUserReflection(12); 
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ВИКЛЮЧЕННЯ МАСИВУ]: {ex.Message}");
                Console.ResetColor();
            }

            // Сценарій 3: Тест помилки читання файлів
            Console.WriteLine("\n--- Сценарій 3: Тест помилки читання файлів ---");
            try
            {
                smartMirror.SyncConfig(@"C:\SmartMirror\NonExistingConfig.dat");
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ФАЙЛОВЕ ВИКЛЮЧЕННЯ]: {ex.Message}");
                Console.ResetColor();
            }

            // Сценарій 4: Робота з критичною ситуацією здоров'я
            Console.WriteLine("\n--- Сценарій 4: Тест медичного форс-мажору ---");
            try
            {
                smartMirror.AnalyzeUserReflection(2); 
            }
            catch (CriticalMedicalEmergencyException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ГЕНЕРАЦІЯ ВИКЛЮЧЕННЯ ПРЕДМЕТНОЇ ОБЛАСТІ]: {ex.Message}");
                Console.WriteLine($"Біометрія збою: Пульс {ex.Pulse} уд/хв, Температура {ex.Temperature}°C.");
                Console.ResetColor();

                smartMirror.FireEmergencyAlert(ex.Message, userGroup[2]);
            }

            // Сценарій 5: Критичний розряд акумулятора
            Console.WriteLine("\n--- Сценарій 5: Тест критичного розряду акумулятора ---");
            SmartMirror brokenMirror = new SmartMirror(userGroup, 0);
            try
            {
                brokenMirror.ActivateSystem();
            }
            catch (BatteryDepletedException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО КРИТИЧНИЙ ЗБОЙ ЖИВЛЕННЯ]: {ex.Message}");
                Console.WriteLine("[Система] Дзеркало автоматично вимкнулося задля збереження мікросхем.");
                Console.ResetColor();
            }

            Console.WriteLine("\n====================== ЛАБОРАТОРНА ЗАВЕРШЕНА УСПІШНО ======================");
            Console.ReadLine();
        }
    }
}
