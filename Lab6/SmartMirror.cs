using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SmartMirrorLab6
{
    // БАЗОВІЙ КЛАС: РОЗУМНЕ ДЗЕРКАЛО
    public class SmartMirror
    {
        // Елементи композиції (Дзеркало монолітно володіє цими модулями)
        protected CameraModule camera;
        protected DisplayInterface display;
        protected HardwareBiometricScanner scanner;
        protected WirelessNetworkModule network;

        // Елемент агрегації (Масив людей приходить і створюється окремо ззовні програми)
        protected List<User> registeredUsers;
        protected int currentBatteryCharge;

        // Оголошення події виявлення критичної ситуації здоров'я або збою
        public event EmergencySituationHandler CriticalSituationDetected;

        public SmartMirror(List<User> externalUsers, int initialBattery)
        {
            // Створення об'єктів композиції всередині конструктора власника
            camera = new CameraModule();
            display = new DisplayInterface();
            scanner = new HardwareBiometricScanner();
            network = new WirelessNetworkModule();

            // Прив'язка агрегованого масиву
            registeredUsers = externalUsers;
            currentBatteryCharge = initialBattery;
        }

        public void ActivateSystem()
        {
            Console.WriteLine("[Дзеркало] Ініціалізація операційної системи...");
            network.InitializeProtocols();
            VerifyPowerStatus();
        }

        public void VerifyPowerStatus()
        {
            if (currentBatteryCharge <= 0)
            {
                throw new BatteryDepletedException("Аварійне завершення: Рівень енергії акумулятора становить 0%!");
            }
            Console.WriteLine($"[Дзеркало] Поточний стан батареї: {currentBatteryCharge}%");
        }

        public void AnalyzeUserReflection(int userIndex)
        {
            if (userIndex < 0 || userIndex >= registeredUsers.Count)
            {
                throw new IndexOutOfRangeException("Помилка ідентифікації: Користувача під таким індексом немає в базі пристрою!");
            }

            User user = registeredUsers[userIndex];
            display.RenderScreen($"Користувач: {user.Name}\nВідображення емоцій: {user.CurrentEmotion}");

            if (user.Pulse == 0 || user.Pulse > 160 || user.Temperature >= 40.0)
            {
                throw new CriticalMedicalEmergencyException(
                    $"Критична充ситуація! Зафіксовано загрозу життю користувача {user.Name}!",
                    user.Temperature,
                    user.Pulse
                );
            }

            PredictUserHealth(user);

            double bmi = scanner.ComputeBMI(user);
            string composition = scanner.EvaluateBodyComposition(user);
            Console.WriteLine($"[Дзеркало] Розрахований індекс маси тіла (ІМТ): {bmi:F1}");
            Console.WriteLine($"[Дзеркало] Аналіз тканин: {composition}");

            currentBatteryCharge -= 20;
        }

        private void PredictUserHealth(User user)
        {
            Console.WriteLine("[Алгоритм] Автоматичне прогнозування стану здоров'я за відображенням...");
            if (user.Temperature > 37.3 && (user.CurrentEmotion == "Сум" || user.CurrentEmotion == "Поганий настрій"))
            {
                Console.WriteLine("[Прогноз] Результат аналізу: Зафіксовано ознаки початку вірусного захворювання та пригнічений стан. Рекомендується ізоляція та вимірювання тиску.");
            }
            else if (user.CurrentEmotion == "Радість")
            {
                Console.WriteLine("[Прогноз] Результат аналізу: Показники ендорфінів високі. Fizичний тонус організму в нормі.");
            }
            else
            {
                Console.WriteLine("[Прогноз] Результат аналізу: Стабільний біометричний баланс.");
            }
        }

        public void ExecuteHairstyleSimulation()
        {
            camera.TakePhoto();
            Console.WriteLine("[Візуалізація] Моделювання зачіски: Примірка стилю 'Андеркат' на поточну форму обличчя...");
            Thread.Sleep(800);
            Console.WriteLine("[Візуалізація] Результат успішно виведено поверх дзеркального відображення.");
        }

        public void FireEmergencyAlert(string message, User user)
        {
            CriticalSituationDetected?.Invoke(message, user);
        }

        public void SyncConfig(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"Помилка доступу до даних: Накопичувач не містить файлу конфігурації: '{filepath}'");
            }
            Console.WriteLine("[Файли] Дані профілів успішно синхронізовано.");
        }
    }
}
