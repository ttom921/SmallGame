using System;

namespace ConsoleAppTestGuess
{
    class Program
    {
        static void Main(string[] args)
        {
            GuessNumber Guestapp = new GuessNumber();
            Guestapp.guess();
            Console.ReadKey();
        }
    }
}
