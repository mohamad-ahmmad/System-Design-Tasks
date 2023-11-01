using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapHub<AlertsHub>("/alerts");
app.Run();


class AlertsHub : Hub
{

    public async Task SendAlert(string alertType, string message)
    {
        Console.WriteLine(alertType+" "+message);
        await Clients.All.SendAsync("AlertRecieved",alertType, message);
    }
}