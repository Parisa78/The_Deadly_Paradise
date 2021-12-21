using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakSproutingRoot : EnemySubWeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
    }

    public IEnumerator Sprout()
    {
        var renderer = GetComponent<SpriteRenderer>();
        float height = renderer.bounds.size.y;
        float moveAmount = height / 20;
        for(int i = 0; i< 10; i++)
        {
            this.transform.position += new Vector3(0, moveAmount, 0);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    protected override void DestroySelf()
    {
    }
}
