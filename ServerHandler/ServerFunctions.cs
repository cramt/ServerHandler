using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServerHandler {
    public static class ServerFunctions {
        private static Process minecraft = null;
        private static Dictionary<string, Func<string, string>> funcs = new Dictionary<string, Func<string, string>> {
                {
                    "minecraft_start",
                    (x) => {
                        if(minecraft != null) {
                            return "the minecraft server is already running";
                        }
                        minecraft = new Process();
                        minecraft.StartInfo.FileName = @"E:\Libraries\Desktop\minecraft_test\Start.bat";
                        minecraft.StartInfo.CreateNoWindow = false;
                        minecraft.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        minecraft.StartInfo.WorkingDirectory = @"E:\Libraries\Desktop\minecraft_test";
                        minecraft.Start();
                        return "the minecraft server is starting";
                    }
                },
                {
                    "minecraft_stop",
                    (x) => {
                        if(minecraft == null) {
                            return "minecraft is not running";
                        }
                        minecraft.GetChildProcesses().ForEach(c => {
                            c.Kill();
                        });
                        minecraft.Kill();
                        return "the minecraft server has stopped";
                    }
                }
            };
        public static string Run(string s) {

            return funcs[s](s);
        }
    }
}
