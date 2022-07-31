using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
                );
           var response = await _httpClient.PostAsync($"{_config["CommandService"]}", httpContent) ;
           if(response .IsSuccessStatusCode)
           {
            System.Console.WriteLine("--> Sync Post to Command Line was successful");
           }else
           {
            System.Console.WriteLine("--> Sync Post to Command Line was not successful");
           }
        }
    }
}