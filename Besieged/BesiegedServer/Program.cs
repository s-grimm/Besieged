using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BesiegedServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MessageService service = new MessageService();
            Thread sendThread = new Thread(service.SendMessage);
            Thread recieveThread = new Thread(service.RecieveMessage);

            //start the threads
            sendThread.Start();
            recieveThread.Start();
            while(!sendThread.IsAlive);
            while(!recieveThread.IsAlive);

            while (Console.ReadLine().Trim().ToString() != "exit")
            {
                Thread.Sleep(1);
            }
            Console.WriteLine("Stopping Message Threads");
            sendThread.Abort();
            recieveThread.Abort();
            sendThread.Join();
            recieveThread.Join();
            Console.WriteLine("Message Threads Stopped");
        }
    }
}