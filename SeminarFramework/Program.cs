using System;
using System.Collections.Generic;

namespace SeminarFramework
{
    internal class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;

#else
            Console.Title = Properties.Settings.Default.ApplicationName;

#endif
            if(string.IsNullOrEmpty(Properties.Settings.Default.FIO) || Properties.Settings.Default.Age <= 0)
            {
                Console.WriteLine("Введите ваше имя: ");
                Properties.Settings.Default.FIO = Console.ReadLine();

                Console.WriteLine("Ваш возраст: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Properties.Settings.Default.Age = age;
                }
                else
                {
                    Properties.Settings.Default.Age = 0;
                }

                Properties.Settings.Default.Save();

            }

            Console.WriteLine(Properties.Settings.Default.FIO);
            Console.WriteLine(Properties.Settings.Default.Age);

            //ConnectingString connectingString = new ConnectingString
            //{
            //    DatabaseName = "DbShop",
            //    Host = "alcaro.host",
            //    Password = "pa55w0rd",
            //    UserName = "AlcaroUser"

            //}; 
            
            //ConnectingString connectingString2 = new ConnectingString
            //{
            //    DatabaseName = "DbShop2",
            //    Host = "alcaro.host2",
            //    Password = "pa55w0rd2",
            //    UserName = "AlcaroUser2"

            //};

            //List<ConnectingString> list = new List<ConnectingString>();

            //list.Add(connectingString);
            //list.Add(connectingString2);

            //CacheProvider cacheProvider = new CacheProvider();
            //cacheProvider.CacheConnections(list);

            CacheProvider cacheProvider = new CacheProvider();
            List<ConnectingString> connections = cacheProvider.GetConnectingsStringsFromCache();

            foreach (var con in connections)
            {
                Console.WriteLine($"{con.DatabaseName}  {con.UserName}");
            }          


            Console.ReadKey();

        }
    }
}