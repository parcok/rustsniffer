using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Management;
using System.Net;
using System.IO;

namespace RustSniffer {
    public static class Global {

        public static bool apiSettings = false;
        public static string apiServer = "";
        public static string apiKey = "";
        public static int apiTime = 0;
        public static int apiThreads = 0;
        public static bool autoPurgeEnabled = true;
        public static ArrayList ipAddresses = new ArrayList();
        public static ArrayList players = new ArrayList();
        public static ArrayList whitelist = new ArrayList();
        public static bool authenticated = false;

        public static string getUniqueID() {
            string uID = "";
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string hddID = dsk["VolumeSerialNumber"].ToString();

            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string cpuID = "";
            foreach (ManagementObject mo in mbsList) {
                cpuID = mo["ProcessorID"].ToString();
            }
            uID = hddID + cpuID;
            return uID;
        }
    }
}
