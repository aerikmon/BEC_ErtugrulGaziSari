using System;

namespace ErtugrulGaziSari_BEChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var operation = new Requests();
            
            operation.ReadData();
            operation.TakeRequest();
            operation.WriteData();
            
            Console.WriteLine("\nClosing program");
        }
    }
}