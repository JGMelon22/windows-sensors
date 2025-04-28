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

        //HardwareType[] supportedHardwareTypes =
        //[
        //    HardwareType.Cpu,
        //    HardwareType.GpuNvidia,
        //    HardwareType.GpuAmd,
        //    HardwareType.GpuIntel
        //];



        //foreach (IHardware hardware in computer.Hardware)
        //{
        //    // Include CPU and all GPU types (discrete and integrated)
        //    if (supportedHardwareTypes.Contains(hardware.HardwareType))
        //    {
        //        Console.WriteLine("Hardware: {0}", hardware.Name);

        //        foreach (ISensor sensor in hardware.Sensors)
        //        {
        //            // Filter for temperature sensors
        //            if (sensor.SensorType == SensorType.Temperature)
        //            {
        //                Console.WriteLine("\tSensor: {0}, Temperature: {1:F2}°C", sensor.Name, sensor.Value);
        //            }
        //        }
        //    }
        //}

        foreach (IHardware hardware in computer.Hardware)
        {
            Console.WriteLine("Hardware: {0}", hardware.Name);

            foreach (IHardware subhardware in hardware.SubHardware)
            {
                Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                foreach (ISensor sensor in subhardware.Sensors)
                {
                    Console.WriteLine("\t\tSensor: {0}, value: {1:F2}°C", sensor.Name, sensor.Value);
                }
            }

            foreach (ISensor sensor in hardware.Sensors)
            {
                Console.WriteLine("\tSensor: {0}, value: {1:F2}°C", sensor.Name, sensor.Value);
            }
        }

        computer.Close();
    }
}