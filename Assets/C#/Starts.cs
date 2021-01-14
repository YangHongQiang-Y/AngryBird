using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starts : MonoBehaviour {



    // Use this for initialization
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}
