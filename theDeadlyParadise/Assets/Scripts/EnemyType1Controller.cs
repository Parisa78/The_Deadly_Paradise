using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Controller : RegularEnemyController
{
    protected override void Hit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString() };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
