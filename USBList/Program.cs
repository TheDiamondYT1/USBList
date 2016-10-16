using System;
using System.Collections.Generic;
using System.Management;

namespace USBList
{
    class Program
    {
        static void Main(string[] args)
        {
            var usbDevices = GetUSBDevices();

            foreach (var usbDevice in usbDevices)
            {
                Console.WriteLine("Device: {0}", usbDevice.Name);
                Console.WriteLine("Status: {0}\n", usbDevice.Status);
            }

            Console.Read();
        }

        public static List<USBDeviceInfo> GetUSBDevices()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub");
            ManagementObjectCollection collection = searcher.Get();

            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();       
            foreach (var device in collection)
            {
                USBDeviceInfo deviceInfo = new USBDeviceInfo();          
                 
                deviceInfo.Name = (String) device.GetPropertyValue("Name");
                deviceInfo.Status = (String) device.GetPropertyValue("Status");
                devices.Add(deviceInfo);
            }

            collection.Dispose();
            searcher.Dispose();
            return devices;
        }
    }

    public class USBDeviceInfo
    {
        public String Name { get; set; }    
        public String Status { get; set; }
    }
}
