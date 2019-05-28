using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace ServerHandler {
    public static class Utilities {
        public static List<Process> GetChildProcesses(this Process process) {
            List<Process> children = new List<Process>();
            ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format("Select * From Win32_Process Where ParentProcessID={0}", process.Id));

            foreach (ManagementObject mo in mos.Get()) {
                children.Add(Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
            }

            return children;
        }
    }
}
