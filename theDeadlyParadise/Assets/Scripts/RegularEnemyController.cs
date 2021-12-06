using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class RegularEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    protected string[] effectiveSwords;
    public RegularEnemyConfig config;
    int HP;

    protected abstract void Hit();
    

    void Start()
    {
        HP = config.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Array.IndexOf(effectiveSwords, collision.gameObject.tag.ToString()) > -1)  //collision.gameObject. CompareTag(Tags.Sword.ToString()))
        {
            GetHurt();
        }
    }


    public void GetHurt()
    {
        //toggles hurt animation
        HP -= config.hitAmount;
        if(HP <= 0)
        {
            //toggle dying animation
            Destroy(this.gameObject);
        }
    }
}
