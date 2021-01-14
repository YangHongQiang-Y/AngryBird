using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBG;
    private Image image;
    public GameObject[] stars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
		if(transform.parent.GetChild(0).name==gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            int beforeNum = int.Parse(gameObject.name) - 1;
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString())>0)
            {
                isSelect = true;
            }
        }


        if(isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("Text").gameObject.SetActive(true);

            int cout = PlayerPrefs.GetInt("level" + gameObject.name);
            if(cout>0)
            {
                for(int i=0;i<cout;i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Selected()
    {
        if(isSelect)
        {
            PlayerPrefs.SetString("nowLevel","level"+gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
