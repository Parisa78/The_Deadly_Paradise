using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullEnemyController : RegularEnemyController
{
    private Animator anim;
    public GameObject seed;
    public float timeToAttack;
    public Vector3 playerPosition;
    public Vector3 prevPosition;
    private float moveAmount;
    protected override void Hit()
    {
        base.Hit();
        anim.Play("Base Layer.BullAttack", 0, 0.25f);
        playerPosition = FindObjectOfType<PlayerController>().transform.position;
        prevPosition = transform.position;
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
            if (timeToAttack > 0)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                Hit();
                timeToAttack = config.attackMaxTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            transform.position += (playerPosition - transform.position).normalized * moveAmount;
            if (Vector3.Distance(playerPosition, transform.position) < 0.2)
                isAttacking = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            FindObjectOfType<PlayerController>().ChangeHealth(-1 * config.attackAmount);
        }
    }
}
