﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intruction_B : MonoBehaviour {

	//cambiar a 1 para ingles
	private int idioma =0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){
		transform.GetChild(0).gameObject.SetActive(true);
	}

	void OnMouseUp(){
		transform.GetChild(0).gameObject.SetActive(false);
		//cambiar imagenes de instrucciones
	}
}