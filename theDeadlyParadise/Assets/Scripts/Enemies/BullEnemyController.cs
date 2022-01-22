using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BullEnemyController : RegularEnemyController
{
    private Animator anim;
    public float timeToAttack;
    public Vector3 playerPosition;
    public Vector3 prevPosition;
    public float moveAmount;

    //for pathfinding
    public float nextWaypointDistance = 10f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    SpriteRenderer sp;
    bool canHitPlayer = false;
    bool imDead;
    protected override void Hit()
    {
        base.Hit();
        playerPosition = FindObjectOfType<PlayerController>().transform.position;
        prevPosition = transform.position;
        InvokeRepeating("UpdatePath", 0f, 2f);
        canHitPlayer = true;
        //isAttacking = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString(), Tags.FireSword.ToString() };
        timeToAttack = config.attackMaxTime;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        sp = GetComponent<SpriteRenderer>();
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, playerPosition, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isAttacking)
        //{
            if (timeToAttack > 0)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                Hit();
                timeToAttack = Random.Range(config.attackMaxTime - 1f, config.attackMaxTime + 1f);
        }
        //}
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            //Debug.Log(Vector3.Distance(playerPosition, transform.position));
            if (Vector3.Distance(playerPosition, transform.position) < 0.2)
            {
                anim.Play("Base Layer.BullEnemyAttack", 0, 0);
                isAttacking = false;
            }
            if (path == null)
                return;
            if(currentWayPoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Debug.Log(path.vectorPath[currentWayPoint]);
            Debug.Log("and distance is:");
            Debug.Log((path.vectorPath[currentWayPoint] - transform.position).normalized);
            Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position).normalized;
            transform.position += direction * moveAmount;
            
            sp.flipX = direction.x < 0;
            //Vector2 force = direction * moveAmount * Time.deltaTime;
            //rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if(distance < nextWaypointDistance)
            {
                currentWayPoint++;
            }

        }
    }

    //public void CanHitPlayerEnable() => canHitPlayer = true;

    public void CanHitPlayerDisable() => canHitPlayer = false;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Tags.Player.ToString()) && canHitPlayer)
        {
            FindObjectOfType<PlayerController>().ChangeHealth(-1 * config.attackAmount);
            canHitPlayer = false;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public override void GetHurt()
    {
        //toggles hurt animation
        if (imDead) return;
        Debug.Log("what");
        HP -= playerHitAmount;
        if (HP <= 0)
        {
            //toggle dying animation
            imDead = true;
            anim.Play("Base Layer.BullEnemyDie", 0, 0);
        }
    }
}
