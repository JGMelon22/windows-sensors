using Sensors.Services;

HardwareMonitorService hardwareMonitorService = new();
hardwareMonitorService.Monitor();

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();
