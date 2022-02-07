using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using MiniJSON;
using extOSC;



public class Networking : MonoBehaviour
{
	// singleton instance
	public static Networking instance; 

	public  string REMOTE_IP 									= ""; 
	private const int NEUROCORE_SERVER_TCP_PORT					= 45456;
	private const int NEUROCORE_SERVER_OSC_PORT					= 45554;
	
	private const int NEUROCORE_MESSAGE_TYPE_CLIENT_CONFIG 		= 21403;
	private const int NEUROCORE_MESSAGE_TYPE_JSON_EVENT 		= 21404;
	
	
	// author : chicmic , purpose : upgrading to extOSC
	private OSCReceiver oscReceiver;
	private OSCTransmitter oscTransmitter;

	// TCP
	Thread			networkingThreadTcp;		// networking threads

	private 	 	TcpClient	  	tcpClient;			
	private 	 	NetworkStream 	tcpStream;
	private const 	int 	  		tcpServerPort		= NEUROCORE_SERVER_TCP_PORT;		
	private 		IPEndPoint	  	tcpServerEndPoint;
	private			float			tcpClientLoopTime	= 1f;
	public 			bool			tcpConnected = false;

	// path to config.json
	private			string			jsonConfig;			
	
	// initialize the networking system
	void Awake()
	{
		// Singleton instantiation
		if (instance != null)
			throw new UnityException ("Duplicate allocation of singleton class!");
		instance = this;
	}
	
	void Start()
	{
		Debug.Log("Initializing network system");

		// read the config.json to a string
		TextAsset jsonTextAsset = Resources.Load("Config") as TextAsset;
		StringReader streamReader = new StringReader(jsonTextAsset.text);
		jsonConfig = streamReader.ReadToEnd();
		streamReader.Close();

		// author : chicmic , purpose : upgrading to extOSC

		oscReceiver = GetComponent<OSCReceiver>();
		oscTransmitter = GetComponent<OSCTransmitter>();
		oscReceiver.AutoConnect = true;
		oscTransmitter.AutoConnect = false;
		oscTransmitter.RemoteHost = "255.255.255.255";

		// author : chicmic , purpose : upgrading to extOSC
		oscReceiver.LocalPort = NEUROCORE_SERVER_OSC_PORT;
		oscTransmitter.LocalPort = NEUROCORE_SERVER_OSC_PORT;

		// Datahandler which sends data to neuromore.cs
	}

	public void CreateTCPSocket(){
		try{
			tcpServerEndPoint = new IPEndPoint(IPAddress.Parse(REMOTE_IP), NEUROCORE_SERVER_TCP_PORT);
			networkingThreadTcp = new Thread( new ThreadStart(TcpThread) );
			networkingThreadTcp.IsBackground = true;
			networkingThreadTcp.Start();
			
			ConnectSocket(tcpServerEndPoint);
		}
		catch{
			Debug.LogWarning("No valid IP adress.");
		}
	}

	private void ConnectSocket(IPEndPoint address)
	{	
		try
		{
			tcpClient = new TcpClient();
			tcpClient.Connect(address);
			tcpStream = tcpClient.GetStream();
			
			if (tcpClient.Connected == true) 
			{
				tcpConnected = true;
				Debug.Log("Connected to server " +address.Address+":"+address.Port);
				return;
			}		
		}
		catch (Exception err)
		{
			tcpConnected = false;
			Debug.LogWarning("Network Error: " + err.ToString() );
			ResetConnection();
			StopSession();
		}
	}

	private bool isdisconnect = false;
	private float time = 0;
	void Update()
	{
		//add time for timing of json client events
		tcpClientLoopTime += Time.deltaTime;
	}


	// destruct the networking system
	void Destruct()
	{
		Debug.Log("Destructing network system");
		ResetConnection();

		if (networkingThreadTcp != null)
		{
			Debug.Log("Stopping TCP Thread ...");
			networkingThreadTcp.Abort(); 
			networkingThreadTcp.Join ();	// block until the thread ends; if this hangs, we know the thread does not abort
			networkingThreadTcp = null;
			tcpClient = null;
		}
		
		CloseExtOscConnection();

		REMOTE_IP = "";
		tcpConnected = false;
		Debug.Log("Network destruction completed");
	}

	void CloseExtOscConnection()
	{
		if (oscTransmitter != null)
		{
			oscTransmitter.Close();
		}

		if (oscReceiver != null)
		{
			oscReceiver.Close();
		}
	}

	void OnDestroy()
	{
		Destruct();
	}


	// close tcp client and tcp stream
	private void ResetConnection()
	{
		Debug.Log("Disconnected from server ");
		tcpConnected = false;
		if (tcpClient != null)
			tcpClient.Close ();

		if (tcpStream != null)
			tcpStream.Close();
	}

	private void StopSession ()
	{
		DataHolder.instance.Init();
	}

	public Boolean SendSwitchStageEvent (int index)
	{
		string jsonString = "{ \"doSwitchStage\": " + index + " }";
		return SendJsonEvent (jsonString);
	}

	// send json message to server via tcp
	public Boolean SendJsonEvent (String jsonString)
	{
		return SendMessage (jsonString, NEUROCORE_MESSAGE_TYPE_JSON_EVENT);
	}


	// send json message of specified type to server via tcp
	private Boolean SendMessage (String jsonString, UInt32 messageID)
	{
		// get json data, create message
		int size = Encoding.UTF8.GetByteCount (jsonString);
		byte[] jsonData = Encoding.UTF8.GetBytes (jsonString);
		// write header 	
		UInt32 headerLength = sizeof(UInt32) + sizeof(UInt32);;
		UInt32 messageLength = (UInt32)(size + headerLength);
		try 
		{
			tcpStream.Write( BitConverter.GetBytes(messageLength), 0, sizeof(UInt32));
			tcpStream.Write( BitConverter.GetBytes(messageID), 0, sizeof(UInt32));
		 
			// write data
			tcpStream.Write (jsonData,0,size);
			tcpStream.Flush ();
		}
		catch (Exception err)
		{
			Debug.LogError ("Network Error: " + err.ToString());
			return false;
		}
		return true;
	}



	
	// process incoming TCP data
	private void TcpThread()
	{
		const int bufferSize = 1024 * 1024 * 8;
		byte[] data = new byte[bufferSize];

		const int headerSize = 2 * sizeof(UInt32);
		byte[] header = new byte[headerSize];
		ConnectSocket(tcpServerEndPoint);

		// run forever
		while (true){
			Thread.Sleep(100);
			try
			{
				//check if server ist still connected
				// close socket and retry connect
				if(tcpClient == null){
					tcpConnected = false;
					Debug.Log("Try to connect....");
					ConnectSocket(tcpServerEndPoint);
					continue;
				}
				if(tcpClient != null)
				{
					if(!tcpClient.Connected)
					{
						tcpClient.Close();
						ConnectSocket(tcpServerEndPoint);
						continue;
					}
				}

				// send json config message to server (loop)
				if(tcpClientLoopTime > 1 && tcpStream.CanWrite){
					SendMessage (jsonConfig, NEUROCORE_MESSAGE_TYPE_CLIENT_CONFIG);
					tcpClientLoopTime = 0;
				}

				// Is there anything to read? 
				int numBytesToRead = 0;
				// wait until we can read data
				if (!tcpStream.CanRead || !tcpStream.DataAvailable)
				{
					Thread.Sleep (10);
					continue;
				}

				int bytesRead = 0;
				// read header into header[]
				while (bytesRead < headerSize)
				{
					bytesRead += tcpStream.Read (header, bytesRead, headerSize-bytesRead);
					Thread.Sleep (10);
				}
								
				// read the network message header
				int messageLength = (int)System.BitConverter.ToUInt32( header, 0 );
				int messageTypeID = (int)System.BitConverter.ToUInt32( header, 4 );

				numBytesToRead = messageLength - headerSize;
		
				// read message body into data[]
				bytesRead = 0;
				while (bytesRead < numBytesToRead)
				{
					bytesRead += tcpStream.Read (data, bytesRead, numBytesToRead);
					Thread.Sleep (10);
				}

				if (messageTypeID != NEUROCORE_MESSAGE_TYPE_JSON_EVENT) 
				{
					Debug.LogWarning("Warning: Unknown TCP network message (TCP)! Fix me!");
					break;
				}
				
					
				// copy message data from buffer
				byte[] jsonData = new byte[numBytesToRead];
				Array.Copy(data, 0, jsonData, 0, jsonData.Length);

				Debug.Log ("Received Message of length " + jsonData.Length);

				// convert the data to a string
				String jsonString = Encoding.UTF8.GetString(jsonData);
				var json = Json.Deserialize(jsonString) as Dictionary<string, object>;

				if (json == null)
				{
					Debug.LogError ("Error parsing json string");
					continue;
				}

				//-------------------------------
				// switch stage event
				string command = "doSwitchStage";
				if (json.ContainsKey(command))
				{
					int stageIndex = int.Parse(json[command].ToString());
					Debug.Log (command + "(" + (stageIndex+1) + ")");
					StageController.instance.GoToStage (stageIndex+1);
				}
				//-------------------------------
				// switch stage event
				command = "doShowText";
				if (json.ContainsKey(command) && DataHolder.gIsRunning)
				{
					Color color = Color.white;
					string text = "";
					float size = 1f;
					Dictionary<string, object> textDict = json[command] as Dictionary<string, object>;
					if(textDict.ContainsKey("color-r"))
						color.r = float.Parse(textDict["color-r"].ToString()) / 255f;
					if(textDict.ContainsKey("color-g"))
						color.g = float.Parse(textDict["color-g"].ToString()) / 255f;
					if(textDict.ContainsKey("color-b"))
						color.b = float.Parse(textDict["color-b"].ToString()) / 255f;
					
					if(textDict.ContainsKey("text"))
						text = textDict["text"].ToString();
					if(textDict.ContainsKey("size"))
						size = float.Parse(textDict["size"].ToString());
						
					GUIText guiText = new GUIText(color, size, text);
					GuiTextController.instance.SetText (guiText);	
				}

				//-------------------------------
				// more commands here 
				
			}
			catch (Exception err) 
			{
				Debug.LogWarning( "Network Error: Close Connection");// + err.ToString() );
				tcpClient = null;
				tcpConnected = false;
			}
		}
	}
}
