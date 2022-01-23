using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : RegularEnemyController
{

    public HealthBar healthBar;
    public GameObject shard;
    public GameObject sword;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthBar.SetMaxHealth(config.maxHP);
        healthBar.SetHealth(config.maxHP);
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
            shard.SetActive(true);
            sword.SetActive(true);
            foreach(var regularEnemy in FindObjectsOfType<RegularEnemyController>())
            {
                Destroy(regularEnemy.gameObject);
            }
        }
    }

}
