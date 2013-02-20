using System;
using Microsoft.Owin.Hosting;



namespace Shell.MVC2.Services.Chat
{
    class Program
    {
        internal IDisposable Host; 


        static void Main(string[] args)
        {
            using (WebApplication.Start<Startup>("http://localhost:8080/"))
            {
                Console.WriteLine("Server running at http://localhost:8080/");
                Console.ReadLine();
            }
           
        }

       

    }
}
