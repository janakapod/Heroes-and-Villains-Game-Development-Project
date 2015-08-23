using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	string registeredGameName="heroes_and_villains_server";
	//bool isRefreshing=false;
	float refreshRequestLength =5.0f;
	HostData[] hostData;
	
	HostData connectedTo;

	private void StartServer(){
		Network.InitializeServer (20, 25002, false);
		MasterServer.RegisterHost (registeredGameName, "Heroes and Villains Server", "Comment");

	}

	void OnServerInitialized(){
		Debug.Log ("Server has been initialized");

	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent){
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Server Registration Successful");
		}
	}
	public IEnumerator RefreshHostList(){
		Debug.Log ("Refreshing..");
		MasterServer.RequestHostList (registeredGameName);
		float timeStarted = Time.time;
		float timeEnd = timeStarted + refreshRequestLength;
		while(Time.time<timeEnd){
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}

		if (hostData == null || hostData.Length == 0) {
			Debug.Log ("No Active Servers");

		} else {
			Debug.Log("Number of Servers : "+hostData.Length);
		}
	}

	public void OnGUI()
	{
		//if (Network.isClient || Network.isServer)
		//	return;

		if(GUI.Button(new Rect(25f,25f,150f,30f),"Start New Server")){
			//start server content
			StartServer();
		}
		if(GUI.Button(new Rect(25f,65f,150f,30f),"Refresh Server List")){
			//server stopping content
			StartCoroutine("RefreshHostList");
		}

		if (hostData != null) {
			if (GUI.Button (new Rect (25f, 105f, 150f, 30f), "Show Server List")) {
				float level=145f;
				float plus=0f;
				foreach(HostData hostD in hostData){
					if (GUI.Button (new Rect (25f, level+plus, 150f, 30f), hostD.gameName)){
						Network.Connect(hostD);
						connectedTo=hostD;
					}
					plus+=40f;
				}
			}
		}

		if (Network.isServer) {
			GUI.TextField((new Rect(25f,30f,30f,20f)),"Hosting"+registeredGameName);

			if(GUI.Button((new Rect(25f,65f,150f,30f)),"Stop Server")){
				MasterServer.UnregisterHost();
			}
		}
		if(Network.isClient){
			GUI.TextField((new Rect(25f,30f,30f,20f)),"Client is connected to"+connectedTo.gameName);
			
			if(GUI.Button((new Rect(25f,65f,150f,30f)),"Stop Server")){
				MasterServer.UnregisterHost();
			}
		}
	}

	

}
