using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRootController : EnemySubWeaponController
{
    private bool isGoingRight;
    public float moveAmount;
    private float firstx;
    float halfCameraPositionx;
    protected override void Start()
    {
        halfCameraPositionx = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f)/2;
        if (gameObject.transform.position.x > 0)
            isGoingRight = false;
        else
            isGoingRight = true;
    }
    protected void FixedUpdate()
    {
        transform.position += new Vector3(moveAmount * (isGoingRight ? 1 : -1), 0, 0);
        if((isGoingRight && transform.position.x > halfCameraPositionx)
            || (!isGoingRight && transform.position.x < -halfCameraPositionx))
            Destroy(this.gameObject);
    }
    protected override void DestroySelf()
    {
    }
}
