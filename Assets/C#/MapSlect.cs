using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlect : MonoBehaviour {

    public int starsNum = 0;
    private bool isSelect = false;
    public GameObject locks;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;
    private void Start()
    {
        PlayerPrefs.GetInt("totalNum", 0);
        if(PlayerPrefs.GetInt("totalNum", 0)>=starsNum)
        {
            isSelect = true;
        }
        if(isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);
        }
    }
    public void Selected ()
    {
        if(isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
}
