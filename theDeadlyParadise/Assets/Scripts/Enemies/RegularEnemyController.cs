using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class RegularEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    protected string[] effectiveSwords;
    public RegularEnemyConfig config;
    protected int HP;
    protected int playerHitAmount;
    protected bool isAttacking;

    protected virtual void Hit()
    {
        isAttacking = true;
    }
    

    protected virtual void Start()
    {
        HP = config.maxHP;
        playerHitAmount = FindObjectOfType<PlayerController>().hitAmount;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(Array.IndexOf(effectiveSwords, collision.gameObject.tag.ToString()) > -1)  //collision.gameObject. CompareTag(Tags.Sword.ToString()))
        {
            GetHurt();
        }
    }


    public virtual void GetHurt()
    {
        //toggles hurt animation
        HP -= playerHitAmount;
        if(HP <= 0)
        {
            //toggle dying animation
            Destroy(this.gameObject);
        }
    }
}
