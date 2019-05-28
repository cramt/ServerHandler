using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerHandler {
    public class TcpEventArgs: EventArgs {
        public string Data { get; set; }
        public Action<string> Response { get; set; }
    }
    public class TcpHandler {
        public event EventHandler<TcpEventArgs> OnData;
        TcpListener Listener;
        public Task Init(int port) {
            return Task.Factory.StartNew(() => {
                Listener = new TcpListener(IPAddress.Any, port);
                Listener.Start();
                while (Listener != null) {
                    try {
                        TcpClient client = Listener.AcceptTcpClient();
                        NetworkStream nwStream = client.GetStream();
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        if (OnData == null) {
                            client.Close();
                            return;
                        }
                        else {
                            OnData(this, new TcpEventArgs() {
                                Data = dataReceived,
                                Response = str => {
                                    byte[] strData = Encoding.ASCII.GetBytes(str);
                                    nwStream.Write(strData, 0, strData.Length);
                                    client.Close();
                                }
                            });
                        }
                    }
                    catch (Exception) {

                    }
                }
            });
        }
        public void Stop() {
            Listener.Stop();
            Listener = null;
        }
    }
}
