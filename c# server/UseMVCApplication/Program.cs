using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Collections;
using  WinFormMVC.Model;
using  WinFormMVC.View;
using  WinFormMVC.Controller;
using System.Net;
using System.Net.Sockets;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;



namespace UseMVCApplication
{
static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UsersView view = new UsersView();
            view.Visible = false;
            // Add some dummy data
            IList users = new ArrayList();
                    // Get Host IP Address that is used to establish a connection
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1
        // If a host has multiple addresses, you will get a list of addresses
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            try
            {

                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;


                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);




                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                users.Add(new User("", "", "0", "unknown", true,handler,new ArrayList(),"unknown","unknown","unknown"));


                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            UsersController controller = new UsersController(view, users);
            controller.LoadView();
            view.ShowDialog();
        }
    }
}
