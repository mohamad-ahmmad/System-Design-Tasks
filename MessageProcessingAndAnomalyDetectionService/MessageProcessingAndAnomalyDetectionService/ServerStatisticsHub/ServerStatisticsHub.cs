using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.ServerStatisticsHub.Interface;
using Microsoft.AspNetCore.SignalR.Client;

namespace MessageProcessingAndAnomalyDetectionService.ServerStatisticsHub
{
    public class ServerStatisticsHub : IServerStatisticsHub
    {
        private readonly string _hubHost;
        public ServerStatisticsHub(string hubHost)
        {
            _hubHost = hubHost;
        }
        
        public async Task SendAlertsToClientsAsync(IEnumerable<Alert> alerts)
        {
            if(!alerts.Any()) 
            {
                return;
            }

            var connection = new HubConnectionBuilder()
                .WithUrl(_hubHost)
                .Build();

            await connection.StartAsync();
            foreach (var a in alerts)
            {
            await connection.InvokeCoreAsync("SendAlert", new[] {a.AlertType.ToString(), a.Message});
            }
            await connection.DisposeAsync();
        }
    }
}
