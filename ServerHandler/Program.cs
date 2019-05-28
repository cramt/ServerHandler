using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerHandler {
    class Program {
        const int PORT = 666;
        static void Main(string[] args) {
            TcpHandler handler = new TcpHandler();
            handler.OnData += (object sender, TcpEventArgs e) => {
                Console.WriteLine(e.Data);
                e.Response("done");
            };
            handler.Init(PORT);

            Thread.Sleep(-1);

            /*
            Process minecraft = new Process();
            minecraft.StartInfo.FileName = @"E:\Libraries\Desktop\minecraft_test\Start.bat";
            minecraft.StartInfo.CreateNoWindow = false;
            minecraft.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            minecraft.StartInfo.WorkingDirectory = @"E:\Libraries\Desktop\minecraft_test";
            minecraft.Start();
            while (true) {
                string line = Console.ReadLine();
                if (line == "stop") {
                    minecraft.GetChildProcesses().ForEach(x => x.Kill());
                    minecraft.Kill();
                    
                }
            }
            */
        }
    }
}
