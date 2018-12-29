using Nancy.Hosting.Self;
using NancyHostMySql.DAL.Seed;
using NancyHostMySql.Log;
using System;

namespace NancyHostMySql
{
    class Program
    {
        private static string Host = $"http://localhost:";
        private static int Port = 1234;
        static void Main(string[] args)
        {
            InitializeDataBase();

            Console.WriteLine(LogMsgs.ServerStart + Host + Port);

            try
            {
                var config = new HostConfiguration
                {
                    UrlReservations = new UrlReservations
                    {
                        CreateAutomatically = true
                    }
                };
                using (var webService = new NancyHost(config, new Uri(Host + Port)))
                {
                    webService.Start();
                    Console.WriteLine(LogMsgs.ServerRunning);
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void InitializeDataBase()
        {
            try
            {
                new CreateTables();
                new InitializeTables();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
