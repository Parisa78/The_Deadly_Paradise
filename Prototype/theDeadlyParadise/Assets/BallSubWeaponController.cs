using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSubWeaponController : EnemySubWeaponController
{
    void FixedUpdate()
    {
        transform.position += new Vector3(0, -0.05f, 0);
        if(transform.position.y < 0)
        {
            DestroySelf();
        }
    }

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Im in ontrigger enter override");
    //}
}
