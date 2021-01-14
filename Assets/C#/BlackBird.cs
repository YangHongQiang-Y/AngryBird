using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    public List<Pig> blocks = new List<Pig>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "Emeny")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Emeny")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }
    public override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count>0&&blocks!=null)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                Pig block = blocks[i];
                block.Dead();
            }
        }
        OnClear();
    }
    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }
    protected override void Next()
    {
        GameManger._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManger._instance.NextBird();
    }
}
