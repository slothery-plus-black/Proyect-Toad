﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChoquesJugadorMulti : NetworkBehaviour {

	//PuntasEstrella puntas = new PuntasEstrella();
	//VidasMulti vidas;

	PuntasEstrella puntas;
	ReproductorSonidos sonidos;

	/*[SyncVar]
	int vidas = 3;*/
	Vector3 posInicial;
	Vector3 posInicialCamara;
	Quaternion posInicialRotation;

	public GameObject salida;

	NetworkLobbyManager manager;

	//ReproductorSonidos sonidos;
	
	//public GameObject salida;

	GameObject pivot;

	// Use this for initialization
	void Awake () {
		manager = GameObject.Find("LobbyManager").GetComponent<NetworkLobbyManager>();

		puntas = new PuntasEstrella();
		posInicial = transform.position;
		
		pivot = GameObject.Find("pivot camara");

		posInicialCamara = pivot.transform.position;
		posInicialRotation = pivot.transform.rotation;

		sonidos = GameObject.FindGameObjectWithTag("reproductor").GetComponent<ReproductorSonidos>();
	}

	void Start(){
		posInicial = transform.position;
		
		pivot = GameObject.Find("pivot camara");

		posInicialCamara = pivot.transform.position;
		posInicialRotation = pivot.transform.rotation;

		//vidas = GameObject.Find("VidasMulti").GetComponent<VidasMulti>();

		//vidas.BuscarSalida();

		salida = GameObject.FindGameObjectWithTag("salida");
		for (int i=0; i < salida.transform.childCount;i++){
			salida.transform.GetChild(i).gameObject.SetActive(false);
		}
		
	}
	

	// Update is called once per frame
	void Update () {
		//print(vidas.GetPuntas());
		//print(posInicialCamara.position);
	}

	void ComprobarEnemigo(GameObject other){
		if (isLocalPlayer && other.tag.ToLower().Equals("enemigo")){
			
			//if (!vidas.Restar()){
				pivot.transform.position = posInicialCamara;
				pivot.transform.rotation = posInicialRotation;
				transform.position = posInicial;
			//}else{
				//CargadorEscenas.CargaEscenaAsync("Menu");
			//}
			//vidas --;

			/*if (vidas <= 0){
				sonidos.ReproducirSonidoMuerte();
				//CargadorEscenas.CargaEscenaAsync("Menu");
				
			}else{
				sonidos.ReproducirSonidoDanio();
				pivot.transform.position = posInicialCamara;
				pivot.transform.rotation = posInicialRotation;

				RpcRespawn();
				
				sonidos.ReproducirSonidoSpawn();
			}*/
		}
	}

	void OnTriggerEnter(Collider other){
		if (isLocalPlayer){
				//print(vidas.GetPuntas());
			if (other.gameObject.tag.ToLower().Equals("salida")){
				if (puntas.GetPuntas() >= 5){
					
					//manager
					
					//manager.StopMatchMaker();
					//NetworkManager.Shutdown();
					//Destroy(manager.gameObject);
					//print(manager.gameObject);
					
					Network.Disconnect ();
					Destroy (manager.gameObject);
					Destroy(GameObject.Find("LobbyPlayer(Clone)"));
					//GameManager.gameManager.MainLoadScene ("scene00"); // SCENE SUIVANTE

					CargadorEscenas.CargaEscenaAsync("multiFinal");
				}
			}

			if (other.gameObject.tag.ToLower().Equals("punta")){

				//Si se tienen las 5
				
				//Debug.Log(vidas);
				//vidas.CogerPunta(other.gameObject);
				sonidos.ReproducirPuntaEstrella(puntas.GetPuntas());
				puntas.SumarPunta();
				Destroy(other.gameObject);

				if (puntas.GetPuntas()>=5){
					sonidos.ReproducirSonidoSalida();
					for (int i=0; i < salida.transform.childCount;i++){
						salida.transform.GetChild(i).gameObject.SetActive(true);
					}
				}

			}

			ComprobarEnemigo(other.gameObject);
		}
		
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
        if (Network.isServer)
            Debug.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                Debug.Log("Lost connection to the server");
            else
                Debug.Log("Successfully diconnected from the server");

		CargadorEscenas.CargaEscenaAsync("multiFinal");
    }

	void OnCollisionEnter(Collision other){
		ComprobarEnemigo(other.gameObject);
	}
}
