using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonEnemyController : RegularEnemyController
{
    private Animator anim;
    public float timeToAttack;
    public Vector3 playerPosition;
    public Vector3 prevPosition;
    public Transform attackPoint;
    public float attackRange;
    private float moveAmount;
    public LayerMask playerLayer;


    //for pathfinding
    public float nextWaypointDistance = 0.1f;
    Path path;
    int currentWayPoint = 0;
    Seeker seeker;
    Rigidbody2D rb;
    SpriteRenderer sp;
    bool goNearPlayer = false;
    bool imDead;
    private PlayerController player;
    private Renderer renderer;
    protected override void Hit()
    {
        base.Hit();
        anim.Play("Base Layer.SkeletonAttack", 0, 0);
        Collider2D[] playerHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        if(playerHit.Length > 0) // player is hit
        {
            FindObjectOfType<PlayerController>().ChangeHealth(-1 * config.attackAmount);
        }
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        effectiveSwords = new string[] { Tags.Sword.ToString()};
        timeToAttack = config.attackMaxTime;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        sp = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        player = FindObjectOfType<PlayerController>();
        moveAmount = player.moveAmount * 0.8f;
        renderer = GetComponent<Renderer>();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, playerPosition, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!renderer.isVisible)
        {
            goNearPlayer = false;
            return;
        }
        playerPosition = player.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) < 1 && !imDead)
        {
            if (timeToAttack > 0)
            {
                timeToAttack -= Time.deltaTime;
            }
            else
            {
                Hit();
                timeToAttack = Random.Range(config.attackMaxTime - 1, config.attackMaxTime + 1);
            }
            anim.SetBool("Walking", false);
            goNearPlayer = false;
        }
        else if(!isAttacking && !imDead)
        {
            goNearPlayer = true;
            anim.SetBool("Walking", true);
        }
    }

    private void FixedUpdate()
    {
        if (goNearPlayer && !imDead)
        {
            if (path == null)
                return;
            if (currentWayPoint >= path.vectorPath.Count)
            {
                return;
            }
            Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position).normalized;
            transform.position += direction * moveAmount;

            sp.flipX = direction.x < 0;

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWaypointDistance)
            {
                currentWayPoint++;
            }

        }
    }


    public void Die()
    {
        Destroy(this.gameObject);
    }

    public override void GetHurt()
    {
        if (imDead) return;
        HP -= playerHitAmount;
        if (HP <= 0)
        {
            //toggle dying animation
            imDead = true;
            anim.Play("Base Layer.SkeletonDead", 0, 0);
        }
        else
        {
            //toggles hurt animation
            anim.Play("Base Layer.SkeletonGotHit", 0, 0);
        }
    }
}