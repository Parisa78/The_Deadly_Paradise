using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSubWeaponController : EnemySubWeaponController
{
    float cameraPositionx;
    public bool isGoingLeft;
    protected override void Awake()
    {
        base.Awake();
        cameraPositionx = 1f / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - 0.5f);
    }
    void FixedUpdate()
    {
        if(isGoingLeft)
            transform.position -= new Vector3(0.1f, 0, 0);
        else
            transform.position += new Vector3(0.1f, 0, 0);
        if (transform.position.x > cameraPositionx/2 || transform.position.x < -1*cameraPositionx/2)
        {
            DestroySelf();
        }
    }

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Im in ontrigger enter override");
    //}
}
