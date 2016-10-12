using System;

namespace ReflowController
{
    public class ThermoCoupleStatus
    {
        public ThermoCoupleStatus(double temperature, bool thermoCoupleOpen)
        {
            Temperature = temperature;
            ThermoCoupleOpen = thermoCoupleOpen;
        }
        public double Temperature { get; private set; }
        public bool ThermoCoupleOpen { get; private set; }
    }
}