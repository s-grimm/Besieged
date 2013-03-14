﻿using BesiegedLogic;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace BesiegedServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                svcHost = new ServiceHost(typeof(BesiegedServer), new Uri("http://localhost:31337/BesiegedServer/"));
                svcHost.AddServiceEndpoint(typeof(Framework.ServiceContracts.IBesiegedServer), new WSDualHttpBinding(), "BesiegedMessage");
                svcHost.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
                svcHost.Open();
                Console.Write("Service Started.\n> ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                while (true)
                {
                    var serverMessage = Console.ReadLine().Trim().ToString().ToLower();
                    if (serverMessage == "exit") break;
                    else if (serverMessage == "?" || serverMessage == "help" || serverMessage == "\\help")
                    {
                        Console.WriteLine("Besieged Server Commands\nexit: Stops the server.");
                        Console.Write("> ");
                    }
                    else if (serverMessage == "list")
                    {
                        var clients = GameObject.GetClients();
                        if (clients.Count <= 0)
                        {
                            Console.Write("> ");
                            continue;
                        }
                        Console.WriteLine("Connected Clients");
                        foreach (BesiegedLogic.Objects.Client client in clients)
                        {
                            Console.WriteLine(client.Alias);
                        }
                    }
                }
                if (svcHost != null)
                {
                    svcHost.Close();
                }
            }
        }
    }
}