using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float max=10;
    public float min=5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;
    public bool ispig = false;
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.GetComponent<Bird>().Hurt();
        if (collision.gameObject.tag == "Player")
        {
            Music(birdCollision);
            
        }
        if (collision.relativeVelocity.magnitude>max)
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude>min&& collision.relativeVelocity.magnitude<max)
        {
            render.sprite = hurt;
            Music(hurtClip);
        }
    }

    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Dead()
    {
        if(ispig)
        {
            GameManger._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go =Instantiate(score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);
        Music(dead);
    }
    public void Music(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
