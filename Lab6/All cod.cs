using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SmartMirrorLab6
{
    // =========================================================================
    // 1. КЛАСИ ВИКЛЮЧНИХ СИТУАЦІЙ (СЕМАНТИКА ПРЕДМЕТНОЇ ОБЛАСТІ - ЗАВДАННЯ 4)
    // =========================================================================

    // Системне виключення: повна розрядка батареї дзеркала
    public class BatteryDepletedException : Exception
    {
        public BatteryDepletedException(string message) : base(message) { }
    }

    // Медичне виключення: користувач потрапив у критичну ситуацію (інфаркт, інсульт, непритомність)
    public class CriticalMedicalEmergencyException : Exception
    {
        public double Temperature { get; }
        public int Pulse { get; }

        public CriticalMedicalEmergencyException(string message, double temperature, int pulse) : base(message)
        {
            Temperature = temperature;
            Pulse = pulse;
        }
    }

    // =========================================================================
    // 2. КЛАС КОРИСТУВАЧА (СУТНІСТЬ ДЛЯ ДЕМОНСТРАЦІЇ АГРЕГАЦІЇ)
    // =========================================================================
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

    // =========================================================================
    // 3. СКЛАДОВІ ЕЛЕМЕНТИ «РОЗУМНОГО ДЗЕРКАЛА» (ДЛЯ РЕАЛІЗАЦІЇ КОМПОЗИЦІЇ)
    // =========================================================================

    public class CameraModule
    {
        public void TakePhoto() => Console.WriteLine("[Камера] Фото успішно зафіксовано. Збереження в пам'ять...");
        public void StartVideoCall(string contactName) => Console.WriteLine($"[Камера] Встановлення стабільного відеозв'язку з: {contactName}...");
    }

    public class DisplayInterface
    {
        public void RenderScreen(string screenText)
        {
            Console.WriteLine("\n[--- SMART MIRROR DISPLAY ---]");
            Console.WriteLine(screenText);
            Console.WriteLine("[----------------------------]");
        }
        public void ShowCriticalAlert(string alertMessage) => Console.WriteLine($"\n!!! [СПОВІЩЕННЯ СИСТЕМИ БЕЗПЕКИ] !!! {alertMessage}");
    }

    public class HardwareBiometricScanner
    {
        // Метод обчислення індексу маси тіла (ІМТ)
        public double ComputeBMI(User user) => user.Weight / Math.Pow(user.Height, 2);

        // Визначення співвідношення тканин
        public string EvaluateBodyComposition(User user)
        {
            double bmi = ComputeBMI(user);
            if (bmi < 18.5) return "Превалює м'язова маса, дефіцит жирової тканини.";
            if (bmi <= 24.9) return "Ідеальний баланс жирової та м'язової тканин тіла.";
            return "Зафіксовано надлишок жирової тканини.";
        }
    }

    public class WirelessNetworkModule
    {
        public void InitializeProtocols() => Console.WriteLine("[Мережа] Модулі Wi-Fi та Bluetooth активовано. Сигнал стабільний.");
        public void BroadcastCloudData(string payload, string target) => Console.WriteLine($"[Мережа] Пакет даних '{payload}' успішно передано до {target} через Wi-Fi.");
    }

    // =========================================================================
    // 4. ДЕЛЕГАТ ДЛЯ РЕАЛІЗАЦІЇ СИСТЕМИ ПОДІЙ (ЗАВДАННЯ 5)
    // =========================================================================
    public delegate void EmergencySituationHandler(string logMessage, User user);

    // =========================================================================
    // 5. БАЗОВІЙ КЛАС: РОЗУМНЕ ДЗЕРКАЛО
    // =========================================================================
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
            // Обробка виходу за межі масиву (Завдання 4)
            if (userIndex < 0 || userIndex >= registeredUsers.Count)
            {
                throw new IndexOutOfRangeException("Помилка ідентифікації: Користувача під таким індексом немає в базі пристрою!");
            }

            User user = registeredUsers[userIndex];
            display.RenderScreen($"Користувач: {user.Name}\nВідображення емоцій: {user.CurrentEmotion}");

            // Моделювання потрапляння людини у критичну ситуацію (Інфаркт / раптовий збій життєдіяльності)
            if (user.Pulse == 0 || user.Pulse > 160 || user.Temperature >= 40.0)
            {
                throw new CriticalMedicalEmergencyException(
                    $"Критична ситуація! Зафіксовано загрозу життю користувача {user.Name}!",
                    user.Temperature,
                    user.Pulse
                );
            }

            // Інтелектуальне прогнозування самопочуття (Мобілізація фантазії)
            PredictUserHealth(user);

            // Визначення біометричних індексів
            double bmi = scanner.ComputeBMI(user);
            string composition = scanner.EvaluateBodyComposition(user);
            Console.WriteLine($"[Дзеркало] Розрахований індекс маси тіла (ІМТ): {bmi:F1}");
            Console.WriteLine($"[Дзеркало] Аналіз тканин: {composition}");

            // Витрата заряду пристрою
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
                Console.WriteLine("[Прогноз] Результат аналізу: Показники ендорфінів високі. Фізичний тонус організму в нормі.");
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
            // Виклик події через делегат
            CriticalSituationDetected?.Invoke(message, user);
        }

        // Обробка виключних ситуацій читання файлів налаштувань (Завдання 4)
        public void SyncConfig(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"Помилка доступу до даних: Накопичувач не містить файлу конфігурації: '{filepath}'");
            }
            Console.WriteLine("[Файли] Дані профілів успішно синхронізовано.");
        }
    }

    // =========================================================================
    // 6. ПОХІДНИЙ КЛАС: РОЗУМНИЙ ГОЛОСОВИЙ ПОМІЧНИК (ЗАВДАННЯ 3)
    // =========================================================================
    public class SmartMirrorWithVoiceAssistant : SmartMirror
    {
        private string _assistantName;

        public SmartMirrorWithVoiceAssistant(List<User> externalUsers, int initialBattery, string name)
            : base(externalUsers, initialBattery)
        {
            _assistantName = name;
        }

        public void InterpretVoiceInput(string voiceCommand, int userIndex)
        {
            Console.WriteLine($"\n[Голосовий модуль] Почуто команду: \"{voiceCommand}\"");
            Console.WriteLine($"[{_assistantName}] Виконую обробку мовного запиту...");

            if (voiceCommand.Contains("сканування") || voiceCommand.Contains("діагностика"))
            {
                AnalyzeUserReflection(userIndex);
            }
            else if (voiceCommand.Contains("зачіска"))
            {
                ExecuteHairstyleSimulation();
            }
            else if (voiceCommand.Contains("фото"))
            {
                camera.TakePhoto();
                network.BroadcastCloudData("Image_RAW_092.png", "Хмарне Сховище Друзів");
            }
            else
            {
                Console.WriteLine($"[{_assistantName}] Команда \"{voiceCommand}\" не підтримується поточною прошивкою.");
            }
        }

        // Реалізація обробника події критичної ситуації всередині помічника
        public void SpeakEmergencyWarning(string alertText, User user)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[{_assistantName} - МОВНИЙ СИНТЕЗАТОР ШОКОВОЇ СИТУАЦІЇ]: Внимание!");
            Console.WriteLine($"[{_assistantName}]: Користувач {user.Name} знепритомнів або має критичний пульс ({user.Pulse} уд/хв). Запускаю протокол порятунку!");
            Console.ResetColor();
        }
    }

    // =========================================================================
    // ТОЧКА ВХОДУ В ПРОГРАМУ (ТЕСТУВАННЯ ТА ОБРОБКА ВСІХ ВИКЛЮЧЕНЬ)
    // =========================================================================
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
                new User("Олексій", 78.2, 1.82, 36.6, 75, "Радість"),      // Здоровий статус
                new User("Марія", 54.0, 1.68, 38.5, 98, "Сум"),            // Застуда
                new User("Дмитро", 85.0, 1.76, 39.9, 175, "Раптовий інфаркт") // Критичний статусสุขภาพ
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

            // -----------------------------------------------------------------
            // СЦЕНАРІЙ 1: Демонстрація штатної роботи та голосових команд
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Сценарій 1: Тест нормальної роботи ---");
            try
            {
                smartMirror.InterpretVoiceInput("Аліса, запусти біометричну діагностику", 0); // Олексій
                smartMirror.InterpretVoiceInput("Аліса, увімкни моделювання зачіски", 1);    // Марія
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            // -----------------------------------------------------------------
            // СЦЕНАРІЙ 2: Обробка виходу за межі масиву (Завдання 4)
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Сценарій 2: Тест виходу за межі масиву людей ---");
            try
            {
                smartMirror.AnalyzeUserReflection(12); // Неіснуючий користувач
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ВИКЛЮЧЕННЯ МАСИВУ]: {ex.Message}");
                Console.ResetColor();
            }

            // -----------------------------------------------------------------
            // СЦЕНАРІЙ 3: Обробка помилок файлової системи (Завдання 4)
            // -----------------------------------------------------------------
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

            // -----------------------------------------------------------------
            // СЦЕНАРІЙ 4: Робота з критичною ситуацією здоров'я (Завдання 4 та 5)
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Сценарій 4: Тест медичного форс-мажору ---");
            try
            {
                smartMirror.AnalyzeUserReflection(2); // Дмитро з інфарктними показниками
            }
            catch (CriticalMedicalEmergencyException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ГЕНЕРАЦІЯ ВИКЛЮЧЕННЯ ПРЕДМЕТНОЇ ОБЛАСТІ]: {ex.Message}");
                Console.WriteLine($"Біометрія збою: Пульс {ex.Pulse} уд/хв, Температура {ex.Temperature}°C.");
                Console.ResetColor();

                // Активація події розсилки сигналів тривоги стороннім системам через делегат
                smartMirror.FireEmergencyAlert(ex.Message, userGroup[2]);
            }

            // -----------------------------------------------------------------
            // СЦЕНАРІЙ 5: Моделювання повної розрядки батареї (Завдання 4)
            // -----------------------------------------------------------------
            Console.WriteLine("\n--- Сценарій 5: Тест критичного розряду акумулятора ---");
            // Створюємо дзеркало з розрядженою до нуля батареєю
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
