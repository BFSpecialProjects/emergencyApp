using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Threading;
using System.Net;

/* WDT
 * 12.1.18
 * Asynchronous client proof-of-concept for Black Falcon Emergency Alert App.
 */

//TODO: All it does as of now is accept multiple connections.
//	Figure out how to send a single message from every
//	client that connects. Good work so far!

namespace AsyncClient
{
	/* State object for receiving date from remote device. */
	public class StateObject
	{
		//init client socket and receiver buffer
		public Socket workSocket = null;
		public const int BUFFERSIZE = 256;

		//init receiver buffer and string
		public byte[] buffer = new byte[BUFFERSIZE];
		public StringBuilder sb = new StringBuilder();
	}//end class

	public class AsynchronousClient
	{
		//port number for the remote device
		private const int PORT = 11000;

		//ManualResetEvent instances signal completion
		private static ManualResetEvent connectDone = new ManualResetEvent(false);
		private static ManualResetEvent sendDone = new ManualResetEvent(false);
		private static ManualResetEvent receiveDone = new ManualResetEvent(false);

		//string response from remove device
		private static String response = String.Empty;

		private static void startClient()
		{
			//connect to remote device
			try
			{
				//establish remove endpoint for socket
				IPHostEntry ipHostInfo = Dns.GetHostEntry("wintermute");
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);

				//create a TCP/IP socket
				Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				//connect to the remote endpoint
				client.BeginConnect(remoteEP, new AsyncCallback(connectCallback), client);
				connectDone.WaitOne();

				//send test data to remote device
				send(client, "This is a test<EOF>");
				sendDone.WaitOne();

				//receive response from remote device and write to console
				receive(client);
				receiveDone.WaitOne();
				Console.WriteLine("Response received: {0}", response);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		private static void connectCallback(IAsyncResult ar)
		{
			try
			{
				//receive the socket from the state
				Socket client = (Socket)ar.AsyncState;
				
				//complete connection
				client.EndConnect(ar);
				Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

				//signal that connection has been made
				connectDone.Set();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		private static void receive(Socket client)
		{
			try
			{
				//create the state object
				StateObject state = new StateObject();
				state.workSocket = client;

				//begin receiving data from the remote device
				client.BeginReceive(state.buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(receiveCallback), state);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		private static void receiveCallback(IAsyncResult ar)
		{
			try
			{
				//retrieve state object and client socket from asynchronous state object
				StateObject state = (StateObject)ar.AsyncState;
				Socket client = state.workSocket;

				//read data from remote device
				int bytesRead = client.EndReceive(ar);

				if (bytesRead > 0)
				{
					//store data received so far
					state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

					//get the rest of the data
					client.BeginReceive(state.buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(receiveCallback), state);
				}
				else
				{
					//all data has arrived, put in the response
					if (state.sb.Length > 1)
					{
						response = state.sb.ToString();
					}//end if

					//signal that all bytes have been received
					receiveDone.Set();
				}//end if
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		private static void send(Socket client, String data)
		{
			while(true)
			{
				Console.ReadLine();
				
				//convert the string data to byte data
				byte[] byteData = Encoding.ASCII.GetBytes(data);

				//begin sending data to the remote device
				client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(sendCallback), client);
			}//end while
		}//end method

		private static void sendCallback(IAsyncResult ar)
		{
			try
			{
				//retreive the socket from the state object
				Socket client = (Socket)ar.AsyncState;

				//complete sending data to remote device and signal that all bytes have been sent
				int bytesSent = client.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to the server.", bytesSent);
				sendDone.Set();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		public static int Main(String[] args)
		{
			startClient();
			Console.Read();
			return 0;
		}//end main
	}//end class
}//end namespace
