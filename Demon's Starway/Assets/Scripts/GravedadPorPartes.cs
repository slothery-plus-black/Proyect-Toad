﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GravedadPorPartes : NetworkBehaviour {
	
	List<GameObject> objects;
	public Vector3 direccion;
	public float fuerzaGravitatoria = 1f;

	// Use this for initialization
	void Start () {
		objects = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if (isServer){
			if (objects.Count != 0)
			foreach (GameObject o in objects){
				Rigidbody r = o.GetComponent<Rigidbody> ();
				r.AddForce(direccion * fuerzaGravitatoria, ForceMode.Acceleration);
			}
		}
		
	}

	void OnTriggerEnter (Collider col){
		if (isServer && col.gameObject.tag.Equals("gravedad")){
			//if (isLocalPlayer){
				objects.Add (col.gameObject);
			//}
			
		}
	}

	void OnTriggerExit (Collider col){
		if (isServer && col.gameObject.tag.Equals("gravedad")){
			objects.Remove (col.gameObject);
		}
	}

}
