//Похідний клас, який через наслідування додає розпізнавання голосових команд та звукове сповіщення.
using System;
using System.Collections.Generic;

namespace SmartMirrorLab6
{
    // ПОХІДНИЙ КЛАС: РОЗУМНИЙ ГОЛОСОВИЙ ПОМІЧНИК (ЗАВДАННЯ 3 - НАСЛІДУВАННЯ)
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
            Console.WriteLine($"\n[{_assistantName} - МОВНИЙ СИНТЕЗАТОР ШОКОВОЇ СИТУАЦІЇ]: Увага!");
            Console.WriteLine($"[{_assistantName}]: Користувач {user.Name} знепритомнів або має критичний пульс ({user.Pulse} уд/хв). Запускаю протокол порятунку!");
            Console.ResetColor();
        }
    }
}
