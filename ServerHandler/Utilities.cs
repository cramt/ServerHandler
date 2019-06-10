using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

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



        [StructLayout(LayoutKind.Sequential)]
        struct STARTUPINFO {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        const int STARTF_USESHOWWINDOW = 1;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOWMINNOACTIVE = 7;


        public static Process StartProcessNoActivate(string cmdLine, string currentDirectory = null) {
            STARTUPINFO si = new STARTUPINFO();
            si.cb = Marshal.SizeOf(si);
            si.dwFlags = STARTF_USESHOWWINDOW;
            si.wShowWindow = SW_SHOWMINNOACTIVE;

            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();

            CreateProcess(null, cmdLine, IntPtr.Zero, IntPtr.Zero, true,
                0, IntPtr.Zero, currentDirectory, ref si, out pi);


            return Process.GetProcessById(pi.dwProcessId);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("User32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
        [DllImportAttribute("User32.DLL")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
