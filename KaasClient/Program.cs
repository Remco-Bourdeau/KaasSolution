using KaasService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KaasClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            Console.Write("Smaak: ");
            var smaak = Console.ReadLine();
            var response = await client.GetAsync($"http://localhost:21722/kazen/smaak?smaak={smaak}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var kazen = await response.Content.ReadAsAsync<List<Kaas>>();
                    foreach (var kaas in kazen)
                        Console.WriteLine($"{kaas.Naam}");
                    break;
                case HttpStatusCode.NotFound:
                    Console.WriteLine("Kaas niet gevonden");
                    break;
                default:
                    Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
                    break;
            }
        }
    }
}
