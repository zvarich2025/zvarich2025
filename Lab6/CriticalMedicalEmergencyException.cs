//Виняток, який фіксує небезпечні для життя показники (пульс, температура) та передає їх у блок обробки.
using System;

namespace SmartMirrorLab6
{
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
}
