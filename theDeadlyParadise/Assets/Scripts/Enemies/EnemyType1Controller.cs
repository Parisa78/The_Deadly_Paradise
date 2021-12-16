using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Controller : RegularEnemyController
{
    protected override void Hit()
    {
        base.Hit();
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString(), Tags.FireSword.ToString() };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
