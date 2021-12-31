using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyController : RegularEnemyController
{
    PlayerController player;
    public float heightToJump;
    public bool isJumping;
    protected override void Hit()
    {
        base.Hit();
        //StartCoroutine(Jump());
        throw new System.NotImplementedException();
    }

    //private IEnumerator Jump()
    //{
        
    //}

    // Start is called before the first frame update
    void Awake()
    {
        isJumping = false;
        player = FindObjectOfType<PlayerController>();
        effectiveSwords = new string[] { Tags.Sword.ToString() };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isJumping)
        {
            transform.position += new Vector3(player.transform.position.x - transform.position.x, 0, 0) * player.moveAmount * 0.3f;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && isAttacking)
        {
            collision.gameObject.GetComponent<PlayerController>().ChangeHealth(-1 * config.attackAmount);
        }
    }
}
