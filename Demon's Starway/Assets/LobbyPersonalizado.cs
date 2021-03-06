﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class LobbyPersonalizado : NetworkLobbyManager {

	SpriteRenderer r;
	Collider2D c;
	public InputField texto;
	//public GameObject texto;

	private string partida = "default";

	void Awake(){
		if (GameObject.FindGameObjectsWithTag("lobby").Length > 1){
			Destroy(this.gameObject);
		}

		r = GetComponent<SpriteRenderer>();
		c= GetComponent<Collider2D>();
	}

	// Use this for initialization
	void Start () {
		//Buscar();
	}

	void Buscar(){
		MMStart();
		MMListMatches();
	}

	void MMStart(){
		print("start");

		this.StartMatchMaker();
	}

	void MMListMatches(){
		print("list");
		this.matchMaker.ListMatches(0,20,partida,true,0,0,OnMatchList);
	}

	public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		print("matchlist");
		
		base.OnMatchList(success,extendedInfo,matchList);

		if (!success){
			Debug.Log("List failed: "+extendedInfo);
		}
		else
		{
			if (matchList.Count > 0){
				Debug.Log("Sucessfully listed matches. 1st match: "+matchList[0]);
				MMJoinMatch(matchList[0]);
			}else{
				MMCreateMatch();
			}
			
		}
	}

	void MMJoinMatch(MatchInfoSnapshot firstMatch){
		print("join");
		this.matchMaker.JoinMatch(firstMatch.networkId,"",Network.player.externalIP,Network.player.ipAddress,0,0,OnMatchJoined);
	}

	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo){
		print("onmatchjoined");
		base.OnMatchJoined(success,extendedInfo,matchInfo);

		if (!success){
			print("failed to join match: "+extendedInfo);
		}else{
			//success
			print("joined match: "+matchInfo.networkId);
		}
	}

	void MMCreateMatch(){
		print("create");
		this.matchMaker.CreateMatch(partida,3,true,"","","",0,0,OnMatchCreate);
	}

	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo){
		print("onmatchcreate");
		base.OnMatchCreate(success,extendedInfo,matchInfo);

		if (!success){
			print("failed to create match: "+extendedInfo);
		}else{
			//success
			print("created match: "+matchInfo.networkId);
		}
	}

	void OnMouseDown(){
		r.enabled = false;
		c.enabled = false;

		partida = texto.text;

		Buscar();
		
	}
}
