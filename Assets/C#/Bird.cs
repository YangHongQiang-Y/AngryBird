using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public Transform rightPos;
    public Transform leftPos;
    public float max = 1.5f;
    private bool iscilk = false;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;
    public LineRenderer right;
    public LineRenderer left;
    public GameObject boom;
    protected TestMyTrail myTrail;
    public bool canMove = true;
    public float smooth = 3;
    public AudioClip select;
    public AudioClip fly;
    public Sprite hurt;
    private bool isFly = false;
    protected SpriteRenderer render;
    public void OnMouseDown()
    {
        if(canMove)
        {
            Music(select);
            iscilk = true;
            rg.isKinematic = true;
        }
        
    }

    public void OnMouseUp()
    {
        if (canMove)
        {
            iscilk = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
    }
    // Use this for initialization
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if(iscilk)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
            if(Vector3.Distance(transform.position,rightPos.position) > max)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= max;
                transform.position = pos + rightPos.position;
            }
            Line();
        }
        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 16),
            Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);
        if(isFly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }
    void Fly()
    {
        isFly = true;
        Music(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5);
    }
    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }
    protected virtual void Next()
    {
        GameManger._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManger._instance.NextBird();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();
    }
    public void Music(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }
    public virtual void ShowSkill()//技能
    {
        isFly = false;
    }
    public void Hurt()
    {
        render.sprite = hurt;
    }
}
