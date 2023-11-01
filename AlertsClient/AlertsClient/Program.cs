// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.Build();   

var host = configuration["SignalRConfig:Host"] ?? throw new ArgumentNullException("SignalRConfig:Host");
Console.WriteLine(host);
var conn = new HubConnectionBuilder()
    .WithUrl(host)
    .Build();
await conn.StartAsync();
Console.WriteLine("Connected");
conn.On("AlertRecieved", (string alertType, string message) =>
{
    Console.WriteLine(alertType+" "+ message);
});
Console.WriteLine("Press any key to exit.");
Console.ReadLine();