using LibreHardwareMonitor.Hardware;

namespace Sensors.Services;
public class HardwareMonitorService
{
    public void Monitor()
    {
        Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
            IsMotherboardEnabled = true,
            IsControllerEnabled = true,
            IsNetworkEnabled = true,
            IsStorageEnabled = true
        };

        computer.Open();
        computer.Accept(new UpdateVisitor());

        Console.WriteLine("=== Hardware Monitoring Information ===");

        foreach (IHardware hardware in computer.Hardware)
        {
            // Set color based on hardware type and manufacturer
            if (hardware.HardwareType == HardwareType.Cpu)
            {
                if (hardware.Name.Contains("AMD", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (hardware.Name.Contains("Intel", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
            else if (hardware.HardwareType == HardwareType.GpuNvidia)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (hardware.HardwareType == HardwareType.GpuAmd)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (hardware.HardwareType == HardwareType.GpuIntel)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ResetColor(); // Default color for other hardware types
            }

            // Print hardware information
            Console.WriteLine($"Hardware: {hardware.HardwareType} - {hardware.Name}");
            Console.ResetColor(); // Reset color after printing the title
            Console.WriteLine(new string('-', 50));

            foreach (IHardware subhardware in hardware.SubHardware)
            {
                Console.WriteLine($"\tSubhardware: {subhardware.Name}");
                foreach (ISensor sensor in subhardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature)
                    {
                        Console.WriteLine($"\t\tSensor: {sensor.Name,-30} Value: {sensor.Value?.ToString("0.00")} °C");
                    }
                    else
                    {
                        Console.WriteLine($"\t\tSensor: {sensor.Name,-30} Value: {sensor.Value?.ToString("0.00") ?? "N/A"} {sensor.SensorType}");
                    }
                }
            }

            foreach (ISensor sensor in hardware.Sensors)
            {
                if (sensor.SensorType == SensorType.Temperature)
                {
                    Console.WriteLine($"\tSensor: {sensor.Name,-30} Value: {sensor.Value?.ToString("0.00")} °C");
                }
                else
                {
                    Console.WriteLine($"\tSensor: {sensor.Name,-30} Value: {sensor.Value?.ToString("0.00") ?? "N/A"} {sensor.SensorType}");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("========================================");
        computer.Close();
    }
}