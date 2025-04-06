using System;
using System.IO.Ports;
using HiveGuardian.Data;
using HiveGuardian.Models;
using Microsoft.Extensions.Hosting;

namespace HiveGuardian.Services;

public class SerialReaderService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private SerialPort _serialPort;

    public SerialReaderService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _serialPort = new SerialPort("COM3", 9600);
        _serialPort.Open();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_serialPort.BytesToRead > 0)
                {
                    string line = _serialPort.ReadLine();
                    var data = ParseSensorData(line);

                    if (data != null)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<HiveDbContext>();
                        db.SensorData.Add(data);
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            await Task.Delay(500);
        }
    }

    private SensorData? ParseSensorData(string input)
    {
        try
        {
            // Example: "Temp: 24.5°C  Hum: 56%"
            var tempPart = input.Split("Temp:")[1].Split("°")[0].Trim();
            var humPart = input.Split("Hum:")[1].Replace("%", "").Trim();

            return new SensorData
            {
                Timestamp = DateTime.UtcNow,
                Temperature = float.Parse(tempPart),
                Humidity = float.Parse(humPart)
            };
        }
        catch
        {
            return null;
        }
    }
}
