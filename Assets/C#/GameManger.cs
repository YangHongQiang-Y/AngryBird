using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManger : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManger _instance;
    private Vector3 originPos;
    public GameObject win;
    public GameObject loose;
    public GameObject[] stars;
    private int totalNum = 10;

    private int starsNum = 0;
    // Use this for initialization

    private void Awake()
    {
        _instance = this;
        if(birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
        
    }

    private void Start () {
        Initialized();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //第一只小鸟
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }
    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只飞吧
                Initialized();
            }
            else
            {
                //输了
                loose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }

    }
    public void AShowStars()
    {
        StartCoroutine("show");
    }
    IEnumerator show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.4f);
            stars[starsNum].SetActive(true);
        }
    }
    public void RePlay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }
    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }
    public void SaveData()
    {
        if(starsNum>PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        int sum = 0;
        for(int i=0;i<=totalNum;i++)
        {
            sum += PlayerPrefs.GetInt("Level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
