﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            ChangeScene scene = new ChangeScene();
            scene.GoNextScene(gameObject);
        }
	}
}
