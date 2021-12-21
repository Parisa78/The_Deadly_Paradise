using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRootController : EnemySubWeaponController
{
    private bool isGoingRight;
    public float moveAmount;
    private float firstx;
    protected override void Start()
    {
        if (gameObject.transform.position.x > 0)
            isGoingRight = false;
        else
            isGoingRight = true;
        Debug.Log(gameObject.transform.position);
        Debug.Log(isGoingRight);
    }
    protected void FixedUpdate()
    {
        transform.position += new Vector3(moveAmount * (isGoingRight ? 1 : -1), 0, 0);
    }
    protected override void DestroySelf()
    {
    }
}
