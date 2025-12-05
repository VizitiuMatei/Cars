using System;
using CarsApi.Observer;

namespace Cars.Observers
{
    public class ConsoleLoggingCarCreatedListener : ICarCreatedListener
    {
        public void OnCarCreated()
        {
            Console.WriteLine("S a adaugat o masina noua prin POST endpoint");
        }
    }
}
