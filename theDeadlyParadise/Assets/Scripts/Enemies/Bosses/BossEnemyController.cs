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
        //toggles hurt animation
        HP -= playerHitAmount;
        healthBar.SetHealth(HP);
        if (HP <= 0)
        {
            //toggle dying animation
            Destroy(this.gameObject);
            FindObjectOfType<ShardController>().gameObject.SetActive(true);
            FindObjectOfType<lockedSwordController>().gameObject.SetActive(true);
        }
    }

}
