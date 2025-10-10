using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using KLib;
using KLib.Net;

namespace VPTInterface
{
    public class VPTNetwork
    {
        public delegate string RemoteMessageHandlerDelegate(string message);
        public RemoteMessageHandlerDelegate RemoteMessageHandler { set; get; } = null;

        public IPEndPoint PTBEndPoint { get; private set; }

        private CancellationTokenSource _serverCancellationToken = null;

        public VPTNetwork() 
        {
#if false && DEBUG
            PTBEndPoint = new IPEndPoint(IPAddress.Parse("169.254.245.221"), 4926);
#else
            PTBEndPoint = Discovery.FindNextAvailableEndPoint();
#endif
        }

        public void Disconnect()
        {
            if (_serverCancellationToken != null)
            {
                _serverCancellationToken.Cancel();
            }
            if (PTBEndPoint != null)
            {
                KTcpClient.SendMessage(PTBEndPoint, "Quit");
            }
        }

        public void StartDiscoveryServer()
        {
            _serverCancellationToken = new CancellationTokenSource();
            //Task.Run(() =>
            //{
            //    Listener(_ipEndPoint, _serverCancellationToken.Token);
            //}, _serverCancellationToken.Token);

            Task.Run(() =>
            {
                MulticastReceiver("VPT.INTERFACE", PTBEndPoint, _serverCancellationToken.Token);
            }, _serverCancellationToken.Token);
        }

        //private void Listener(IPEndPoint endpoint, CancellationToken ct)
        //{
        //    var server = new KTcpListener();
        //    server.StartListener(endpoint);

        //    Debug.WriteLine($"TCP server started on {server.ListeningOn}");

        //    while (!ct.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            if (server.Pending())
        //            {
        //                ProcessTCPMessage(server);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.Message);
        //        }
        //    }

        //    server.CloseListener();
        //    Debug.WriteLine("TCP server stopped");
        //}

        //private void ProcessTCPMessage(KTcpListener server)
        //{
        //    server.AcceptTcpClient();

        //    string input = server.ReadString();

        //    if (input.Equals("GetProjectionSettings"))
        //    {
        //        var response = RemoteMessageHandler?.Invoke(input);
        //        if (!string.IsNullOrEmpty(response))
        //        {
        //            server.WriteStringAsByteArray(response);
        //            server.CloseTcpClient();
        //        }
        //        else
        //        {
        //            server.SendAcknowledgement();
        //            server.CloseTcpClient();
        //        }
        //    }
        //    else
        //    {
        //        server.SendAcknowledgement();
        //        server.CloseTcpClient();
        //        RemoteMessageHandler?.Invoke(input);
        //    }
        //}

        private void MulticastReceiver(string name, IPEndPoint endpoint, CancellationToken ct)
        {
            var ipLocal = new IPEndPoint(endpoint.Address, 10000);
            Debug.WriteLine(ipLocal);

            var address = IPAddress.Parse("234.5.6.7");
            var ipEndPoint = new IPEndPoint(address, 10000);

            var udp = new UdpClient();
            udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp.Client.Bind(ipLocal);
            udp.Client.ReceiveTimeout = 1000;

            udp.JoinMulticastGroup(address, endpoint.Address);
            Debug.WriteLine(endpoint.Address);

            var anyIP = new IPEndPoint(IPAddress.Any, 0);

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    // receive bytes
                    var bytes = udp.Receive(ref anyIP);
                    var response = Encoding.Default.GetString(bytes);
                    
                    if (response.Equals(name))
                    {
                        bytes = Encoding.UTF8.GetBytes(endpoint.ToString());
                        udp.Send(bytes, bytes.Length, anyIP);
                    }
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex.Message);
                }
            }
        }

        public void SendMessageToPTB(string message, string data = null)
        {
            if (data != null)
            {
                message += $":{data}";
            }
            Debug.WriteLine($"sending {message}");
            var result = KTcpClient.SendMessage(PTBEndPoint, message);
            Debug.WriteLine($"result = {result}");
        }

    }
}
