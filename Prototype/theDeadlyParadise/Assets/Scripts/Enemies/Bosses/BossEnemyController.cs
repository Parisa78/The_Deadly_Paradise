using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : RegularEnemyController
{

    public HealthBar healthBar;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthBar.SetMaxHealth(config.maxHP);
    }

    public override void GetHurt()
    {
        base.GetHurt();
        healthBar.SetHealth(HP);
    }

}
