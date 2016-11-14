using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public string IP = "127.30.0.19";
	public int port = 51202;

	public GameObject cubeChange;

	public bool hideFlag = false;

	public float connectTimer = 0.0f;
	public int attempts = 0;

	// Use this for initialization
	void Start () 
	{
	
		connectTimer = 2.0f;
		attempts = 0;
		IP = "172.30.0.19";
		port = 51202;	
		hideFlag = false;
		if(PlayerPrefs.GetString("isServer") == "true")
			Network.InitializeServer(32, port, !Network.HavePublicAddress());
		else
			Network.Connect(PlayerPrefs.GetString("IP"),port);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(connectTimer > 0)
			connectTimer -=Time.deltaTime;
		else
		{
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				if(PlayerPrefs.GetString("isServer") == "true")
					Network.InitializeServer(32, port, !Network.HavePublicAddress());
				else
					Network.Connect(PlayerPrefs.GetString("IP"),port);
			} 
			connectTimer = 2.0f;
			attempts++;
		}

		if (Input.GetKey (KeyCode.A))
		{
			GetComponent<NetworkView>().RPC("AskColor", RPCMode.Server);
		}

		if (Input.GetKey (KeyCode.B))
		{
			GetComponent<NetworkView>().RPC("AskSound", RPCMode.Server);
		}

		if (Input.GetKey (KeyCode.C))
		{
			GetComponent<NetworkView>().RPC("PlaySound", RPCMode.Server, "test");
		}



	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
	}


	[RPC]
	void AskColor()
	{
		if(Network.isServer)
		{
			GetComponent<NetworkView>().RPC("ChangeColor",RPCMode.All);
		}
	}

	[RPC]
	void ChangeColor()
	{
		cubeChange.GetComponent<Renderer>().material.color = Color.green;
	}

	[RPC]
	void AskSound()
	{
		if(Network.isServer)
		{
			GetComponent<AudioSource>().Play();
		}
	}
	[RPC]
	void PlaySound(string info)
	{
		if(Network.isServer)
		{
			if(info == "test")
				GetComponent<AudioSource>().Play();
		}

	}
}
