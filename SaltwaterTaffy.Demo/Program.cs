﻿// This file is part of SaltwaterTaffy, an nmap wrapper library for .NET
// Copyright (C) 2013 Thom Dixon <thom@thomdixon.org>
// Released under the GNU GPLv2 or any later version
using System;
using System.Linq;
using SaltwaterTaffy.Container;

namespace SaltwaterTaffy.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Console.Write("Enter an IP or subnet: ");
            var target = new Target("192.168.0.0/24");
            Console.WriteLine("Initializing scan of {0}", target);

            //var result = new Scanner(target).HostDiscovery();

            //var result = new Scanner(target).GetAllHostNetworkInterfaces();

            ScanResult result = new Scanner(target).PortScan();
            Console.WriteLine("Detected {0} host(s), {1} up and {2} down.", result.Total, result.Up, result.Down);
            foreach (Host i in result.Hosts)
            {
                Console.WriteLine("<-----BEGIN HOST----->");
                Console.WriteLine("Host: {0}", i.Address);
                foreach (Port j in i.Ports)
                {
                    Console.WriteLine("\tport {0}", j.PortNumber);
                    if (!string.IsNullOrEmpty(j.Service.Name))
                    {
                        Console.Write(" is running {0}", j.Service.Name);
                    }

                    if (j.Filtered)
                    {
                        Console.Write(" is filtered");
                    }

                    Console.WriteLine();
                }

                if (i.OsMatches.Any())
                {
                    Console.WriteLine("and is probably running {0}", i.OsMatches.First().Name);
                }

                Console.WriteLine("MAC: {0}", i.Mac);
                Console.WriteLine("Vendor: {0}", i.Vendor);
                Console.WriteLine("<-----END HOST----->");
                Console.WriteLine("");
            }


            Console.Read();
        }
    }
}