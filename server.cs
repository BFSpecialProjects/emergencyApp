using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;

/* WDT
 * 12.1.18
 * Asynchronous server proof-of-concept for Black Falcon Emergency Alert App.
 */

namespace AsyncServer
{
	/* State object for reading client data asynchronously. */
	public class StateObject
	{
		//client socket
		public Socket workSocket = null;

		//size of receiver buffer and buffer itself
		public const int BUFFERSIZE = 256;
		public byte[] buffer = new byte[BUFFERSIZE];

		//string to hold message received
		public StringBuilder sb = new StringBuilder();
	}//end class

	/* Listener for the server */
	public class AsynchronousSocketListener
	{
		//thread signal, set to incomplete
		public static ManualResetEvent allDone = new ManualResetEvent(false);

		//constructor
		public AsynchronousSocketListener() { }
		
		//begin listening
		public static void startListening()
		{
			//establish the local endpoint for the socket
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddr = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11000);

			//create a TCP/IP socket
			Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			//bind the socket to the local endpoint and listen for incoming connections
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while(true)
				{
					//set the event to nonsignaled state
					allDone.Reset();

					//start an asynchronous socket to listen for connections
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(new AsyncCallback(acceptCallback), listener);

					//wait until a connection is made before continuing
					allDone.WaitOne();
				}//end while
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
		}//end method

		//accept a callback
		public static void acceptCallback(IAsyncResult ar)
		{
			//signal the main thread to continue
			allDone.Set();

			//get the socket that handles the client request
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			//create the state object
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(readCallback), state);
		}//end method

		//read callback data
		public static void readCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			//retreive the state object and the handler socket form the async state object
			StateObject state = (StateObject)ar.AsyncState;
			Socket handler = state.workSocket;

			//read data from the client socket
			int bytesRead = handler.EndReceive(ar);

			while(true)
			{
				if (bytesRead > 0)
				{
					//store data received so far
					state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

					//check for end-of-file, if not there continue to read
					content = state.sb.ToString();
					if (content.IndexOf("<EOF>") > -1)
					{
						state.sb.Clear();

						//all data has been read, display on console
						Console.WriteLine("Read {0} bytes from the socket.", content.Length);
						Console.WriteLine("Data: {0}", content);

						//echo the data back to the client
						send(handler, content);
					}
					else
					{
						//continue to read data
						handler.BeginReceive(state.buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(readCallback), state);
					}//end nested if
				}//end if
			}//end while
		}//end method

		public static void send(Socket handler, String data)
		{
			//convert the string data to byte data using ASCII
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			//begin sending data to the remote device
			handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(sendCallback), handler);
		}//end method

		public static void sendCallback(IAsyncResult ar)
		{
			try
			{
				//retrieve the socket from the state object
				Socket handler = (Socket)ar.AsyncState;

				//finish sending data to the remote device
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}//end try-catch
		}//end method

		public static int Main(String[] args)
		{
			startListening();
			return 0;
		}//end method
	}//end class
}//end namespace
