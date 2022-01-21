using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class andriomaliusSubWeaponController : EnemySubWeaponController
{
    public bool isGoingLeft;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-1 * config.hitAmount);
        }
    }
}

