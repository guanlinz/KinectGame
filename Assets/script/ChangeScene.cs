using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.Find("Btn_StartGame");
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(delegate ()
        {
            GoNextScene(obj);
        });
	}

    public void GoNextScene(GameObject obj)
    {
        SceneManager.LoadScene(obj.scene.buildIndex + 1);
        Debug.Log("GoNextScene is called");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
