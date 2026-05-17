//
using System;

namespace SmartMirrorLab6
{
    // Системне виключення: повна розрядка батареї дзеркала
    public class BatteryDepletedException : Exception
    {
        public BatteryDepletedException(string message) : base(message) { }
    }
}
