using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jinxStartThundering : EnemySubWeaponController
{
    public GameObject[] enemies;
    public void Die()
    {
        int go_idx = UnityEngine.Random.Range(0, enemies.Length - 1);
        var go = GameObject.Instantiate(enemies[go_idx]);
        go.transform.position = transform.parent.transform.position;
        if(go_idx == 2)
        {
            go.GetComponent<SpriteRenderer>().flipX = transform.position.x - FindObjectOfType<PlayerController>().transform.position.x > 0;
        }
        Destroy(transform.parent.gameObject);
    }
}
