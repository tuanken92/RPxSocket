using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPxSocket
{
    public static class MyLib
    {


        public static void sendData(string data, string ipAddress)
        {
            try
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.NoDelay = true;

                IPAddress ip = IPAddress.Parse(ipAddress);
                IPEndPoint ipep = new IPEndPoint(ip, 9100);
                clientSocket.Connect(ipep);

                byte[] fileBytes = Encoding.ASCII.GetBytes(data);
                int num_bytes = clientSocket.Send(fileBytes);
                log(String.Format($"Sent data to printer: {num_bytes} bytes"));

                clientSocket.Close();
                clientSocket.Dispose();
                showDlgMessage("Done!");
            }
            catch (Exception exception)
            {
                showDlgError(exception.Message);
            }

        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException e)
            {
                // Discard PingExceptions and return false;
                showDlgError(e.Message);
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public static void showDlgError(string e)
        {
            MessageBox.Show(e, "Error");
        }

        public static void showDlgMessage(string e)
        {
            MessageBox.Show(e, "THH-Notify");
        }
        public static void log(string data)
        {
            Console.WriteLine(data);
        }

    }
}
