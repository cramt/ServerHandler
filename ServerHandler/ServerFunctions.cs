using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServerHandler {
    public static class ServerFunctions {
        private static Process minecraft = null;
        private static Process modMinecraft = null;
        private static Process ttt = null;
        private static Dictionary<string, Func<string, string>> funcs = new Dictionary<string, Func<string, string>> {
                 {
                    "ping",
                    (x) => {
                        return "pong";
                    }
                },
                {
                    "minecraft_start",
                    (x) => {
                        if(minecraft != null) {
                            return "the minecraft server is already running";
                        }

                        minecraft = new Process();
                        minecraft.StartInfo.FileName = @"H:\Minecraft Simones liv\Start.bat";
                        minecraft.StartInfo.CreateNoWindow = false;
                        minecraft.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        minecraft.StartInfo.WorkingDirectory = @"H:\Minecraft Simones liv";
                        minecraft.Start();
                        return "the minecraft server is starting";
                    }
                },
                {
                    "minecraft_stop",
                    (x) => {
                        if(minecraft == null) {
                            return "The modded minecraft server is not running";
                        }
                        try {
                            minecraft.GetChildProcesses().ForEach(c => {
                                c.Kill();
                            });
                            minecraft.Kill();
                            minecraft = null;
                            return "The minecraft server has stopped";
                        } catch {
                            minecraft = null;
                            return "The modded minecraft server process could not be found, but was nulled anyway";
                        }
                    }
                },
                {
                    "mc_mod_start",
                    (x) => {
                        if(modMinecraft != null) {
                            return "The modded minecraft server is already running";
                        }

                        modMinecraft = new Process();
                        modMinecraft.StartInfo.FileName = @"H:\Modded 1.12.2\Start.bat";
                        modMinecraft.StartInfo.CreateNoWindow = false;
                        modMinecraft.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        modMinecraft.StartInfo.WorkingDirectory = @"H:\Modded 1.12.2";
                        modMinecraft.Start();
                        return "The modded minecraft server is starting";
                    }
                },
                {
                    "mc_mod_stop",
                    (x) => {
                        if(modMinecraft == null) {
                            return "The minecraft server is not running";
                        }
                        try {
                            modMinecraft.GetChildProcesses().ForEach(c => {
                            c.Kill();
                            });
                            modMinecraft.Kill();
                            modMinecraft = null;
                            return "The modded minecraft server has stopped";
                        } catch {
                            modMinecraft = null;
                            return "The modded minecraft server process could not be found, but was nulled anyway";
                        }

                    }
                },
                {
                    "ttt_start",
                    (x) => {
                        const string filename = @"H:\gmod\srcds.exe";
                        const string arguments = "-console -game garrysmod +map ttt_67thway_v6 +maxplayers 16 +host_workshop_collection 924039295 +gamemode terrortown +ttt_spec_prop_rechargetime 0.1";
                        if(ttt != null) {
                            return "The TTT server is already running";
                        }
                        ttt = new Process();
                        ttt.StartInfo.FileName = filename;
                        ttt.StartInfo.CreateNoWindow = false;
                        ttt.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        ttt.StartInfo.Arguments = arguments;
                        ttt.Start();
                        return "The TTT server is starting";
                    }
                },
                {
                    "ttt_stop",
                    (x) => {
                        if(ttt == null) {
                            return "Minecraft is not running";
                        }
                        ttt.GetChildProcesses().ForEach(c => {
                            c.Kill();
                        });
                        ttt.Kill();
                        ttt = null;
                        return "The TTT server has stopped";
                    }
                },{
                    "shutdown_pc",
                    (x) => {
                        System.Diagnostics.Process.Start("shutdown", "/s /t 300");
                        return("Shutting down serverPC");
                    }
                }
            };
        public static string Run(string s) {

            return funcs[s](s);
        }
    }
}
