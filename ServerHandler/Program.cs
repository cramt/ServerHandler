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
                e.Response(ServerFunctions.Run(e.Data));
            };
            handler.Init(PORT);
            Thread.Sleep(-1);
        }
    }
}
