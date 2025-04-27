using LibreHardwareMonitor.Hardware;

namespace Sensors;
public class HardwareMonitorService
{
    public void Monitor()
    {
        Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true
        };

        computer.Open();
        computer.Accept(new UpdateVisitor());

        HardwareType[] supportedHardwareTypes =
        [
            HardwareType.Cpu,
            HardwareType.GpuNvidia,
            HardwareType.GpuAmd,
            HardwareType.GpuIntel
        ];

        foreach (IHardware hardware in computer.Hardware)
        {
            // Include CPU and all GPU types (discrete and integrated)
            if (supportedHardwareTypes.Contains(hardware.HardwareType))
            {
                Console.WriteLine("Hardware: {0}", hardware.Name);

                foreach (ISensor sensor in hardware.Sensors)
                {
                    // Filter for temperature sensors
                    if (sensor.SensorType == SensorType.Temperature)
                    {
                        Console.WriteLine("\tSensor: {0}, Temperature: {1:F2}°C", sensor.Name, sensor.Value);
                    }
                }
            }
        }

        computer.Close();
    }
}