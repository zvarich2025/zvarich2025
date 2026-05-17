//Файл із компонентами дзеркала (камера, екран, сканер, Wi-Fi), які реалізують зв'язок композиції.
using System;

namespace SmartMirrorLab6
{
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
        public double ComputeBMI(User user) => user.Weight / Math.Pow(user.Height, 2);

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
}
