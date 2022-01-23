using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Controller : RegularEnemyController
{
    private Animator anim;
    public GameObject seed;
    public float timeToAttack;
    protected override void Hit()
    {
        base.Hit();
        var go = GameObject.Instantiate(seed);
        go.GetComponent<BallSubWeaponController>().isGoingLeft = GetComponent<SpriteRenderer>().flipX;
        go.transform.position = this.transform.position + new Vector3(0, 0.25f, 0);
        anim.Play("Base Layer.PlantAttack", 0, 0.25f);
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString(), Tags.FireSword.ToString() };
        timeToAttack = config.attackMaxTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if(timeToAttack > 0)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                Hit();
                timeToAttack = Random.Range(config.attackMaxTime - 1f, config.attackMaxTime + 1f);
            }
        }
    }
}
