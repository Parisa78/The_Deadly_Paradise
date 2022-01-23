using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellOfMirrorsEnemyController : MonoBehaviour
{
    public RegularEnemyConfig config;
    public float distanceFromPlayer;
    protected bool isAttacking;
    PlayerController player;
    private float moveAmount;
    private bool moveTowardsPlayer;
    //private Animation anim;

    public void HitPlayer()
    {
        isAttacking = true;
        moveTowardsPlayer = true;
    }

    private void Awake()
    {
        var renderer = GetComponent<SpriteRenderer>();
        float height = renderer.bounds.size.y;
        player = FindObjectOfType<PlayerController>();
        moveTowardsPlayer = false;
        switch (player.direction)
        {
            case PlayerController.Direction.Right:
                transform.position = player.transform.position + new Vector3(-1 * distanceFromPlayer, 0, 0);
                break;
            case PlayerController.Direction.Left:
                transform.position = player.transform.position + new Vector3(distanceFromPlayer, 0, 0);
                renderer.flipX = true;
                break;
        }
        moveAmount = player.moveAmount * 1.4f;
        //anim = gameObject.GetComponent<Animation>();
        //anim.Play();
    }

    private void FixedUpdate()
    {
        if(moveTowardsPlayer)
            //move towards player
            transform.position += (player.transform.position - transform.position).normalized * moveAmount;
    }


    void ReachedPlayer()
    {
        
        if (player.swords[player.swordIdx].GetComponent<SwordMovements>().isInDefenceMode && player.swordIdx == 3)
        {
            GetHurt();
        }
        else
        {
            player.ChangeHealth(-1 * config.attackAmount);
            GetHurt();
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player.ToString()))
        {
            ReachedPlayer();
        }
    }


    public virtual void GetHurt()
    {
        //toggle dying animation
        Destroy(this.gameObject);
    }
}
