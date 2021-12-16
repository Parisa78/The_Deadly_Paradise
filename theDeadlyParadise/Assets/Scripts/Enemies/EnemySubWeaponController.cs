using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySubWeaponController : MonoBehaviour
{
    public EnemySubWeaponConfig config;

    void Awake()
    {
        
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
        
    //}


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-1 * config.hitAmount);
            DestroySelf();
        }
    }

    protected void DestroySelf()
    {
        Destroy(this.gameObject);
    }

}
